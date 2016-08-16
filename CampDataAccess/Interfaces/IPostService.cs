using System;
using System.Collections.Generic;
using CampBusinessLogic.DTO;
using CampBusinessLogic.Infrastructure;
using System.Threading.Tasks;

namespace CampBusinessLogic.Interfaces
{
    public interface IPostService
    {
        Task<OperationDetails> CreatePost(int campPlaceId, string postText);
        Task<List<PostDTO>> GetAllUsersPosts(string name);
        List<PostDTO> GetAllGroupPosts(int postId);
        Task<OperationDetails> DeletePost(int postId);
    }
}
