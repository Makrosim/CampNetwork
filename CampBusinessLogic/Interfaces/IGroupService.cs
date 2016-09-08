using System;
using System.Threading.Tasks;
using CampBusinessLogic.DTO;
using System.Collections.Generic;

namespace CampBusinessLogic.Interfaces
{
    public interface IGroupService : IDisposable
    {
        Task CreateGroup(string userName, GroupDTO groupDTO);
        Task<List<GroupDTO>> GetUsersGroups(string userName);
        GroupDTO GetGroupData(int groupId);
        Task SetGroupData(string userName, GroupDTO groupDTO);
        Task Delete(string userName, int groupId);
    }
}
