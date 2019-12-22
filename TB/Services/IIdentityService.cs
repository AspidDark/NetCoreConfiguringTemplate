using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TB.Domain;

namespace TB.Services
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(string email, string password);
    }
}
