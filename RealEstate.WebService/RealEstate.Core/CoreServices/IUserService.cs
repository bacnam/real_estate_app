using System;
using System.Collections.Generic;
using System.Text;
using RealEstate.Core.Requests;
using RealEstate.Core.Responses;
using System.Threading.Tasks;

namespace RealEstate.Core.CoreServices
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
}
