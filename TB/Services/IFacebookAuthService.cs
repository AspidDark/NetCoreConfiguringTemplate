using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TB.External.Contracts;

namespace TB.Services
{
    public interface IFacebookAuthService
    {
        Task<FacebookTokenValidationResult> ValidateAccessTokenAsync(string accessToken);

        Task<FacebookUserInfoResult> GetUserInfoAsync(string accessToken);
    }
}
