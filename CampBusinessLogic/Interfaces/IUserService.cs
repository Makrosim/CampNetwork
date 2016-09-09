using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using CampBusinessLogic.DTO;
using Microsoft.AspNet.Identity;

namespace CampBusinessLogic.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task Create(RegisterDTO userDTO);
        Task<ClaimsIdentity> Authenticate(RegisterDTO userDTO);
        Task Delete(string userName);
    }
}
