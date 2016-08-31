﻿using System;
using System.Collections.Generic;
using CampBusinessLogic.DTO;
using System.Threading.Tasks;

namespace CampBusinessLogic.Interfaces
{
    public interface IPostService
    {
        void CreatePost(int campPlaceId, string postText);
        Task<List<PostDTO>> GetAllUsersPosts(string userName);
        List<PostDTO> GetAllGroupPosts(int groupId);
        List<PostDTO> GetAllCampPlacePosts(int campPlaceId);
        void DeletePost(string userName, int postId);
    }
}
