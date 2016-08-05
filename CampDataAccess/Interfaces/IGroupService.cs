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
        List<int> GetAllGroupsId();
        Task<GroupDTO> GetGroupData(string name, int groupId);
        Task<OperationDetails> SetGroupData(string name, GroupDTO groupDTO);
        Task<OperationDetails> AddPostToGroup(int groupId, int postId);
    }
}
