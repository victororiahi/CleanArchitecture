using CleanArch.Application.DTOs;
using CleanArch.Application.Interfaces;
using CleanArch.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<UserService> _logger;

        public UserService(UserManager<User> userManager, RoleManager<Role> roleManager, SignInManager<User> signInManager, ILogger<UserService> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _logger = logger;
        }


        public async Task<UserDTO?> AuthenticateUser(UserLoginDTO userLoginDTO)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(userLoginDTO.Email);
                if (user != null)
                {
                    //Check if password is correct
                    var result = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, userLoginDTO.Password);
                    if (result == PasswordVerificationResult.Failed)
                    {
                        return null;
                    }
                    await _signInManager.PasswordSignInAsync(user.Email, userLoginDTO.Password, false, false);

                    return new UserDTO
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        PhoneNumber = user.PhoneNumber,
                        Email = user.Email

                    };

                }

                return null;                            

            }

            catch (Exception ex)
            {
                _logger.LogError($"ERROR: {ex.Message} STACKTRACE: {ex.StackTrace}");
                throw new Exception();

                
            }
        }

        public async Task<List<string>> GetRolesByUser(UserDTO userDTO)
        {
            var user = await _userManager.FindByIdAsync(userDTO.Id.ToString());
            if(user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                return roles.ToList();
            }

            return Enumerable.Empty<string>().ToList();
        }
    }
}
