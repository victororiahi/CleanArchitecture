using CleanArch.Application.DTOs;
using CleanArch.Application.Interfaces;
using CleanArch.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CleanArch.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<TokenService> _logger;

        public TokenService(UserManager<User> userManager, IConfiguration configuration, ILogger<TokenService> logger)
        {
            _userManager = userManager;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<string> GenerateToken(UserDTO userDTO, List<string> roles)
        {
            try
            {
                TimeSpan? expiration = null;
                var user = await _userManager.FindByIdAsync(userDTO.Id.ToString());
                if (user != null)
                {
                    var claim = CreateClaims(user, roles);
                    var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Authentication:JwtBearer:SecretKey").Value));
                    var options = new TokenProviderOptions
                    {
                        Audience = _configuration.GetSection("Authentication:JwtBearer:Audience").Value!,
                        Issuer = _configuration.GetSection("Authentication:JwtBearer:Issuer").Value!,
                        Expiration = TimeSpan.FromMinutes(Convert.ToInt32(_configuration.GetSection("Authentication:JwtBearer:AccessExpiration").Value!)),
                        SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha512)
                    };

                    var token = new JwtSecurityToken
                        (issuer: options.Issuer,
                        audience: options.Audience,
                        claims: claim,
                        notBefore: DateTime.UtcNow,
                        expires: DateTime.UtcNow.Add(options.Expiration),
                        signingCredentials: options.SigningCredentials);

                    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                    return tokenString;
                }
                return string.Empty;

            }

            catch (Exception ex)
            {
                _logger.LogError($"ERROR: {ex.Message} STACKTRACE: {ex.StackTrace}");
                throw new Exception();
            }

        }

        private List<Claim> CreateClaims(User user, List<string> roles)
        {
            var claims = new List<Claim>();
            claims.AddRange(new[]
            {
                new Claim(JwtRegisteredClaimNames.NameId, "029261DF-0E82-4E5E-9AA8-036606F70ECF"),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.Now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                new Claim("email", user.Email),
                new Claim("phoneNumber", user.PhoneNumber),
                new Claim("firstName", user.FirstName),
                new Claim("lastName", user.LastName),

            });
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims; 
        }


    }
}
