using System;
using System.Threading.Tasks;
using CampBusinessLogic.DTO;
using System.Collections.Generic;

namespace CampBusinessLogic.Interfaces
{
    public interface IGroupService : IDisposable
    {
        Task CreateGroup(string name, GroupDTO groupDTO);
        Task<List<GroupDTO>> GetAllGroups(string userName);
        Task<GroupDTO> GetGroupData(string userName, int groupId);
        Task SetGroupData(string userName, GroupDTO groupDTO);
        Task AddPost(int groupId, int postId);
    }
}
