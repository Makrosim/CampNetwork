using System;
using System.Collections.Generic;
using CampBusinessLogic.DTO;
using CampBusinessLogic.Infrastructure;
using System.Threading.Tasks;

namespace CampBusinessLogic.Interfaces
{
    public interface IPostService
    {
        Task<List<PostDTO>> GetAllUsersPosts(string name);
        List<PostDTO> GetAllGroupPosts(int postId);
        Task<OperationDetails> CreatePost(int campPlaceId, PostDTO postDTO);
        Task<OperationDetails> DeletePost(int postId);
    }
}
