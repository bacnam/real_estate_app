using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RealEstate.Core.Requests;
using RealEstate.Core.Responses;

namespace RealEstate.Services
{
    public interface IUserService
    {
        Task<LoginResponse> LoginAsync(LoginRequest loginRequest);
        Task<LoginResponse> DoLoginAsync(string email, string password);
        Task<LoginResponse> DoLoginFacebookAsync(string token);
        Task<LoginResponse> DoLoginGoogleAsync(string token);
        Task<RegisterResponse> DoRegisterAsync(string email, string password, string fullname, string phoneNumber);
        Task<GetAccountResponse> GetAccountAsync();
        Task LogoutAsync();
        GetAccountResponse Account { get; }
    }

    internal class UserService : IUserService
    {
        public Task<LoginResponse> LoginAsync(LoginRequest loginRequest)
        {
            throw new NotImplementedException();
        }
        GetAccountResponse _account = new GetAccountResponse();
        public GetAccountResponse Account { get { return _account; } }

        public Task<LoginResponse> DoLoginAsync(string email, string password)
        {
            var apiService = Xamarin.Forms.DependencyService.Get<IAPIService>();
            LoginRequest request = new LoginRequest(email, password);
            return apiService.PostAsync<LoginResponse>("accounts/login", request.ToJson());
        }

        public Task<LoginResponse> DoLoginFacebookAsync(string token)
        {
            var apiService = Xamarin.Forms.DependencyService.Get<IAPIService>();
            var request = new FacebookLoginRequest(token);
            return apiService.PostAsync<LoginResponse>("accounts/facebook/login", request.ToJson());
        }

        public Task<LoginResponse> DoLoginGoogleAsync(string token)
        {
            var apiService = Xamarin.Forms.DependencyService.Get<IAPIService>();
            var request = new FacebookLoginRequest(token);
            return apiService.PostAsync<LoginResponse>("accounts/google/login", request.ToJson());
        }

        public Task<RegisterResponse> DoRegisterAsync(string email, string password, string fullname, string phoneNumber)
        {
            var apiService = Xamarin.Forms.DependencyService.Get<IAPIService>();
            RegisterRequest request = new RegisterRequest(email, password, fullname, phoneNumber);
            return apiService.PostAsync<RegisterResponse>("accounts/register", request.ToJson());
        }

        public async Task<GetAccountResponse> GetAccountAsync()
        {
            if (Account.Success)
            {
                return Account;
            }
            string token = await Xamarin.Essentials.SecureStorage.GetAsync("token");
            var apiService = Xamarin.Forms.DependencyService.Get<IAPIService>();
            GetAccountRequest request = new GetAccountRequest(token);
            return _account = await apiService.PostAsync<GetAccountResponse>("accounts", request.ToJson());
        }

        public async Task LogoutAsync()
        {
            var apiService = Xamarin.Forms.DependencyService.Get<IAPIService>();
            await apiService.PostAsync<dynamic>("accounts/logout", "");
        }
    }
}
