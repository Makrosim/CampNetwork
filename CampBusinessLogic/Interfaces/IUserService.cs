using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using CampBusinessLogic.DTO;

namespace CampBusinessLogic.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task Create(UserDTO userDTO);
        Task<ClaimsIdentity> Authenticate(UserDTO userDTO);
        Task Delete(string name);
    }
}
