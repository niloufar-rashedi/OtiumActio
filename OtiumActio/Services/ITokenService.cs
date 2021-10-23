using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OtiumActio
{
    public interface ITokenService
    {
        Task<TokenResponse> GetToken(string scope);
    }
}
