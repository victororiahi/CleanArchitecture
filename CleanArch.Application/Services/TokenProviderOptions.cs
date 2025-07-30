using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Application.Services
{
    public class TokenProviderOptions
    {
        public SymmetricSecurityKey SecurityKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public SigningCredentials SigningCredentials { get; set; }
        public TimeSpan Expiration { get; set; }
    }
}
