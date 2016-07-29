using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using CampBusinessLogic.DTO;
using CampBusinessLogic.Infrastructure;

namespace CampBusinessLogic.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<OperationDetails> Create(UserDTO userDTO);
        Task<ClaimsIdentity> Authenticate(UserDTO userDTO);
    }
}
