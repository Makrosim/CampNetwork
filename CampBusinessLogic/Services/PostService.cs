using System;
using System.Threading.Tasks;
using CampBusinessLogic.DTO;
using CampDataAccess.Entities;
using CampBusinessLogic.Interfaces;
using CampDataAccess.Interfaces;
using AutoMapper;
using System.IO;
using System.Collections.Generic;

namespace CampBusinessLogic.Services
{
    public class PostService : IPostService
    {
        IUnitOfWork Database { get; set; }

        public PostService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task CreatePost(int campPlaceID, PostDTO postDTO) // Need fix
        {
            var post = Mapper.Map<PostDTO, Post>(postDTO);

            Database.PostManager.Create(post);
            await Database.SaveAsync();
            post.CampPlace = Database.CampPlaceManager.Get(campPlaceID);
            await Database.SaveAsync();
        }

        public async Task<List<PostDTO>> GetAllUsersPosts(string name)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException(name);

            var user = await Database.UserManager.FindByNameAsync(name);
            var postList = new List<PostDTO>();

            var profile = Database.UserProfileManager.Get(user.Id);

            foreach (var cp in profile.CampPlaces)
            {
                if (cp != null)
                {
                    foreach (var post in cp.Posts)
                    {
                        var postDTO = Mapper.Map<Post, PostDTO>(post);
                        postList.Add(postDTO);
                    }
                }
            }

            return postList;
        }

        public List<PostDTO> GetAllGroupPosts(int groupId)
        {
            var postList = new List<PostDTO>();
            var group = Database.GroupManager.Get(groupId);

            foreach (var post in group.Posts)
            {
                var postDTO = Mapper.Map<Post, PostDTO>(post);
                postList.Add(postDTO);
            }

            return postList;
        }

        public List<PostDTO> GetAllCampPlacePosts(int campPlaceId)
        {
            var postList = new List<PostDTO>();
            var campPlace = Database.CampPlaceManager.Get(campPlaceId);

            foreach (var post in campPlace.Posts)
            {
                var postDTO = Mapper.Map<Post, PostDTO>(post);
                postList.Add(postDTO);
            }

            return postList;
        }

        public async Task DeletePost(string userName, int postId)
        {
            var post = Database.PostManager.Get(postId);

            if (userName != post.CampPlace.UserProfile.User.UserName)
                throw new UnauthorizedAccessException("У вас нет полномочий совершать это действие");

            var messageList = new List<Message>();

            foreach (var message in post.Messages)
            {
                messageList.Add(message);
            }

            foreach (var message in messageList)
            {
                Database.MessageManager.Delete(message.Id);
            }

            Database.PostManager.Delete(post.Id);
            await Database.SaveAsync();
        }

    }
}
