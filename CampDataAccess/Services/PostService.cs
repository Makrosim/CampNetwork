using System;
using System.Threading.Tasks;
using CampBusinessLogic.DTO;
using CampBusinessLogic.Infrastructure;
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

        public async Task<OperationDetails> CreatePost(int campPlaceID, PostDTO postDTO)
        {
            Mapper.Initialize(cfg => { cfg.CreateMap<PostDTO, Post>().ForMember("CampPlace", c => c.Ignore()); });
            var post = Mapper.Map<PostDTO, Post>(postDTO);

            post.CreationDate = DateTime.Now;
            Database.PostManager.Create(post);
            await Database.SaveAsync();
            post.CampPlace = Database.CampPlaceManager.Get(campPlaceID);
            await Database.SaveAsync();

            return new OperationDetails(true, "Операция успешно завершена", "");
        }

        public async Task<List<PostDTO>> GetAllUsersPosts(string name)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException(name);

            var user = await Database.UserManager.FindByNameAsync(name);
            var profile = Database.UserProfileManager.Get(user.Id);

            var postList = new List<PostDTO>();

            Mapper.Initialize(cfg => { cfg.CreateMap<Post, PostDTO>()
                .ForMember("Messages", c => c.Ignore())
                .ForMember(dest => dest.CampPlaceName, opts => opts.MapFrom(src => src.CampPlace.Name));
            });

            foreach (var cp in profile.CampPlaces)
            {
                if (cp != null)
                {
                    foreach(var post in cp.Posts)
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

            Mapper.Initialize(cfg => { cfg.CreateMap<Post, PostDTO>()
                .ForMember("Messages", c => c.Ignore())
                .ForMember(dest => dest.CampPlaceName, opts => opts.MapFrom(src => src.CampPlace.Name));
            });

            foreach (var post in group.Posts)
            {
                var postDTO = Mapper.Map<Post, PostDTO>(post);
                postList.Add(postDTO);
            }

            return postList;
        }

        public async Task<OperationDetails> DeletePost(int postId)
        {
            var post = Database.PostManager.Get(postId);
            var list = new List<int>();

            foreach (var a in post.Messages)
            {
                list.Add(a);
            }

            foreach (var a in list)
            {
                Database.MessageManager.Delete(a);
            }

            Database.PostManager.Delete(post.Id);
            await Database.SaveAsync();

            return new OperationDetails(true, "Операция успешно завершена", "");
        }

    }
}
