using System;
using System.Threading.Tasks;
using CampBusinessLogic.DTO;
using CampBusinessLogic.Infrastructure;
using System.Collections.Generic;

namespace CampBusinessLogic.Interfaces
{
    public interface IGroupService : IDisposable
    {
        Task<OperationDetails> CreateGroup(string name, GroupDTO groupDTO);
        Task<List<GroupDTO>> GetAllGroups(string userName);
        Task<GroupDTO> GetGroupData(string userName, int groupId);
        Task<OperationDetails> SetGroupData(string userName, GroupDTO groupDTO);
        Task<OperationDetails> AddPostToGroup(int groupId, int postId);
    }
}
