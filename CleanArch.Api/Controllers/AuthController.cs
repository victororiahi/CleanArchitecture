using CleanArch.Application.DTOs;
using CleanArch.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CleanArch.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public AuthController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }


        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromBody] UserLoginDTO userLoginDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            //Check if user with email exists
            var user = await _userService.AuthenticateUser(userLoginDTO);
            
            if (user == null)
            {
                return BadRequest("Username or password is incorrect!");
            }


            //Get roles the user belongs to
            var roles = await _userService.GetRolesByUser(user);


            //Generate Jwt Token
            var token = await _tokenService.GenerateToken(user, roles);
            return Ok(token);
        }

    }
}
