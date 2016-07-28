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
        Task<OperationDetails> CreatePost(int Id, PostDTO DTO);
        Task<OperationDetails> DeletePost(int Id);
    }
}
