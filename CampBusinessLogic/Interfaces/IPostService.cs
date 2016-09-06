using System;
using System.Collections.Generic;
using CampBusinessLogic.DTO;
using System.Threading.Tasks;

namespace CampBusinessLogic.Interfaces
{
    public interface IPostService
    {
        Task CreatePost(int campPlaceId, PostDTO postDTO);
        Task<List<PostDTO>> GetAllUsersPosts(string userName);
        List<PostDTO> GetAllGroupPosts(int groupId);
        List<PostDTO> GetAllCampPlacePosts(int campPlaceId);
        Task DeletePost(string userName, int postId);
    }
}
