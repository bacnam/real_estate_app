using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RealEstate.Core.Requests;
using RealEstate.Core.Responses;
using RealEstate.WebService.Databases;
using RealEstate.WebService.Models;

namespace RealEstate.WebService.Services
{
    public interface IUserService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
        Task<LoginResponse> DoLoginFacebookAsync(string token);
        Task<LoginResponse> DoLoginGoogleAsync(string token);
        Task LogoutAsync(string token);
        Task<RegisterResponse> RegisterAsync(RegisterRequest request);
        Task<GetAccountResponse> GetAccountAsync(string token);
        UserModel? GetAccountFromToken(string token);
        string GenerateToken(UserModel account);
        Task<GetNotificationResponse> GetNotificationAsync(long accountId);
        Task<ReadNotificationResponse> ReadNotificationAsync(long notificationId);
    }

    public class UserService : BaseService, IUserService
    {
        public UserService(LibraryContext context) : base(context)
        {
        }
        private string SecretKey = "TW9zaGVFcmV6UHJpdmF0ZUtleQ==";

        public async Task<GetAccountResponse> GetAccountAsync(string token)
        {
            GetAccountResponse response = new GetAccountResponse();
            try
            {
                var account = await Context.Users.FirstOrDefaultAsync(a => a.Token == token);
                if (account == null)
                {
                    response.Message = "Token đã hết hạn hoặc không tồn tại.";
                }
                else
                {
                    var notifications = from n in Context.Notifications
                                        where n.AccountId == account.Id && n.IsRead == false
                                        select n;
                    response.Email = account.Email;
                    response.FullName = account.FullName;
                    response.PhoneNumber = account.PhoneNumber;
                    response.Success = true;
                    response.Notification = notifications.Count();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLine(ex.Message);
                Log.WriteLine(ex.StackTrace);
            }
            return response;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            LoginResponse response = new LoginResponse();
            try
            {
                var account = await Context.Users.FirstOrDefaultAsync(a => a.Email == request.Email);

                if (account == null)
                {
                    response.Message = "Địa chỉ Email hoặc mật khẩu không tồn tại.";
                }
                else
                {
                    if (!string.IsNullOrEmpty(account.Password) && BCrypt.Net.BCrypt.Verify(request.Password, account.Password))
                    {
                        string token = GenerateToken(account);
                        account.Token = token;
                        response.Success = true;
                        response.Message = token;
                        Context.SaveChanges();
                    }
                    else
                    {
                        response.Message = "Địa chỉ Email hoặc mật khẩu không tồn tại.";
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLine(ex.Message);
                Log.WriteLine(ex.StackTrace);
            }
            return response;
        }

        public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
        {
            RegisterResponse response = new RegisterResponse();
            try
            {
                int count = await Context.Users.CountAsync(a => a.Email == request.Email);
                if (count > 0)
                {
                    response.Message = "Địa chỉ Email đã tồn tại.";
                }
                else
                {
                    Context.Users.Add(new UserModel()
                    {
                        Created = DateTime.UtcNow,
                        Updated = DateTime.UtcNow,
                        Email = request.Email,
                        FullName = request.FullName,
                        PhoneNumber = request.PhoneNumber,
                        Sort = 0,
                        Password = BCrypt.Net.BCrypt.HashPassword(request.Password)
                    });

                    Context.SaveChanges();
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                Log.WriteLine(ex.Message);
                Log.WriteLine(ex.StackTrace);
            }
            return response;
        }

        public async Task LogoutAsync(string token)
        {
            try
            {
                var account = await Context.Users.FirstOrDefaultAsync(a => a.Token == token);
                if (account != null)
                {
                    account.Token = string.Empty;
                    Context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLine(ex.Message);
                Log.WriteLine(ex.StackTrace);
            }
        }

        public UserModel? GetAccountFromToken(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                Log.WriteLine("Given token is null or empty.");
                return null;
            }

            try
            {
                return Context.Users.FirstOrDefault(b => b.Token == token);
            }
            catch (Exception ex)
            {
                Log.WriteLine(ex.Message);
                return null;
            }
        }

        public string GenerateToken(UserModel account)
        {
            if (account == null)
                throw new ArgumentException("Arguments to create token are not valid.");
            var jobj = new JObject();
            jobj.Add("Email", account.Email);
            jobj.Add("Id", account.Id);
            jobj.Add("FullName", account.FullName);
            Claim claim = new Claim("account", jobj.ToString());
            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] { claim }),
                Expires = DateTime.UtcNow.AddHours(24),
                SigningCredentials = new SigningCredentials(GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            string accessToken = jwtSecurityTokenHandler.WriteToken(securityToken);

            return accessToken;
        }

        private SecurityKey GetSymmetricSecurityKey()
        {
            byte[] symmetricKey = Convert.FromBase64String(SecretKey);
            return new SymmetricSecurityKey(symmetricKey);
        }

        private TokenValidationParameters GetTokenValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = GetSymmetricSecurityKey()
            };
        }

        public async Task<LoginResponse> DoLoginFacebookAsync(string token)
        {
            using (WebClient webClient = new WebClient())
            {
                LoginResponse loginResponse = new LoginResponse();
                string endpoint = "https://graph.facebook.com/v3.2/me";

                try
                {
                    var content = webClient.DownloadString($"{endpoint}?access_token={token}&fields=id,name,email");
                    if (!string.IsNullOrEmpty(content))
                    {
                        var data = JsonConvert.DeserializeObject<dynamic>(content);

                        string email = data["email"];
                        string fullname = data["name"];
                        string fbId = data["id"];

                        var account = await Context.Users.FirstOrDefaultAsync(a => a.FacebookId == fbId);
                        var emailAccount = Context.Users.FirstOrDefault(a => a.Email == email);
                        if (account == null && emailAccount == null)
                        {
                            account = new UserModel()
                            {
                                Email = email,
                                FullName = fullname,
                                FacebookId = fbId
                            };

                            string myToken = GenerateToken(account);
                            account.Token = myToken;
                            loginResponse.Success = true;
                            loginResponse.Message = myToken;
                            Context.Users.Add(account);
                            Context.SaveChanges();
                        }
                        else if (account == null && emailAccount != null)
                        {
                            loginResponse.Message = "Địa chỉ email này đã được đăng ký với tài khoản khác.";
                        }
                        else
                        {
                            account.Email = email;
                            account.FullName = fullname;
                            string myToken = GenerateToken(account);

                            account.Token = myToken;

                            loginResponse.Success = true;
                            loginResponse.Message = myToken;

                            Context.SaveChanges();
                        }
                    }
                }
                catch (Exception ex)
                {
                    loginResponse.Message = ex.Message;
                }

                return loginResponse;
            }
        }

        public async Task<LoginResponse> DoLoginGoogleAsync(string token)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                LoginResponse loginResponse = new LoginResponse();
                string endpoint = "https://www.googleapis.com/oauth2/v1/userinfo?alt=json&access_token=";

                try
                {
                    var response = await httpClient.GetAsync($"{endpoint}{token}");
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        var data = JsonConvert.DeserializeObject<dynamic>(result);

                        string email = data["email"];
                        string fullname = data["name"];
                        string googleId = data["id"];

                        var account = await Context.Users.FirstOrDefaultAsync(a => a.GoogleId == googleId);
                        var emailAccount = Context.Users.FirstOrDefault(a => a.Email == email);
                        if (account == null && emailAccount == null)
                        {
                            account = new UserModel()
                            {
                                Email = email,
                                FullName = fullname,
                                GoogleId = googleId
                            };

                            string myToken = GenerateToken(account);
                            account.Token = myToken;
                            loginResponse.Success = true;
                            loginResponse.Message = myToken;
                            Context.Users.Add(account);
                            Context.SaveChanges();
                        }
                        else if (account == null && emailAccount != null)
                        {
                            loginResponse.Message = "Địa chỉ email này đã được đăng ký với tài khoản khác.";
                        }
                        else
                        {
                            account.Email = email;
                            account.FullName = fullname;
                            string myToken = GenerateToken(account);

                            account.Token = myToken;

                            loginResponse.Success = true;
                            loginResponse.Message = myToken;

                            Context.SaveChanges();
                        }
                    }
                }
                catch (Exception ex)
                {
                    loginResponse.Message = ex.Message;
                }

                return loginResponse;
            }
        }

        public async Task<GetNotificationResponse> GetNotificationAsync(long accountId)
        {
            var data = from n1 in Context.Notifications
                       where n1.AccountId == accountId
                       select n1 into n2
                       orderby n2.IsRead
                       select n2;
            var news = await (from n3 in data
                              from n4 in Context.News
                              where n3.NewsId == n4.Id
                              select new NotificationData()
                              {
                                  Id = n3.Id,
                                  Source = n4.Id,
                                  IsRead = n3.IsRead,
                                  Time = n3.Created,
                                  Title = n4.Title,
                                  NotificationType = "Tin tức"
                              }).ToListAsync();
            var reals = await (from n5 in data
                               from n6 in Context.RealEstates
                               where n5.RealId == n6.Id
                               select new NotificationData()
                               {
                                   Id = n5.Id,
                                   Source = n6.Id,
                                   IsRead = n5.IsRead,
                                   Time = n5.Created,
                                   Title = n6.Title,
                                   NotificationType = "Tin rao"
                               }).ToListAsync();
            news.AddRange(reals);

            GetNotificationResponse response = new GetNotificationResponse()
            {
                Message = "",
                Notifications = news.OrderBy(c => c.IsRead).ToArray(),
                Success = true
            };
            Console.WriteLine(JsonConvert.SerializeObject(response));
            return response;
        }

        public async Task<ReadNotificationResponse> ReadNotificationAsync(long notificationId)
        {
            ReadNotificationResponse response = new ReadNotificationResponse();
            var notification = await Context.Notifications.FirstOrDefaultAsync(n => n.Id == notificationId);
            if (notification != null)
            {
                notification.IsRead = true;
                await Context.SaveChangesAsync();
                response.Success = true;
            }
            return response;
        }
    }
}
