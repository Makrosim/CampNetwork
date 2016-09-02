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

        public async void CreatePost(int campPlaceID, string postText)
        {
            var post = new Post
            {
                Text = postText,
                CreationDate = DateTime.Now
            };

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

            InitializeMapper();

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

            InitializeMapper();

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

            InitializeMapper();

            foreach (var post in campPlace.Posts)
            {
                var postDTO = Mapper.Map<Post, PostDTO>(post);
                postList.Add(postDTO);
            }

            return postList;
        }

        public async void DeletePost(string userName, int postId)
        {
            var post = Database.PostManager.Get(postId);

            if (userName != post.CampPlace.UserProfile.User.UserName)
                throw new UnauthorizedAccessException("You are not owner of this recource");

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

        private void InitializeMapper()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<Post, PostDTO>()
                .ForMember(dest => dest.CampPlaceName, opts => opts.MapFrom(src => src.CampPlace.Name))
                .ForMember(dest => dest.Author, opts => opts.MapFrom(src => src.CampPlace.UserProfile.User.UserName))
                .ForMember(dest => dest.CampPlaceId, opts => opts.MapFrom(src => src.CampPlace.Id));
            });
        }

    }
}
