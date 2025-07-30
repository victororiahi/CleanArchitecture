using CleanArch.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO?> AuthenticateUser(UserLoginDTO userLoginDTO);

        Task<List<string>> GetRolesByUser(UserDTO userDTO);
    }
}
