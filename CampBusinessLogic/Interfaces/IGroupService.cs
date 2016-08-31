using System;
using System.Threading.Tasks;
using CampBusinessLogic.DTO;
using System.Collections.Generic;

namespace CampBusinessLogic.Interfaces
{
    public interface IGroupService : IDisposable
    {
        void CreateGroup(string name, GroupDTO groupDTO);
        Task<List<GroupDTO>> GetAllGroups(string userName);
        Task<GroupDTO> GetGroupData(string userName, int groupId);
        void SetGroupData(string userName, GroupDTO groupDTO);
        void AddPostToGroup(int groupId, int postId);
    }
}
