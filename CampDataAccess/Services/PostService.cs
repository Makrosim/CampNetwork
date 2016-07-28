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

        public async Task<OperationDetails> CreatePost(int Id, PostDTO postDTO)
        {
            Mapper.Initialize(cfg => { cfg.CreateMap<PostDTO, Post>().ForMember("CampPlace", c => c.Ignore()); });
            var post = Mapper.Map<PostDTO, Post>(postDTO);

            post.CreationDate = DateTime.Now;
            Database.PostManager.Create(post);
            await Database.SaveAsync();
            post.CampPlace = Database.CampPlaceManager.Get(Id);
            await Database.SaveAsync();

            return new OperationDetails(true, "Операция успешно завершена", "");
        }

        public async Task<OperationDetails> DeletePost(int Id)
        {
            var post = Database.PostManager.Get(Id);
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

        public async Task<List<PostDTO>> GetAllUsersPosts(string name)
        {
            var postList = new List<PostDTO>();
            var user = await Database.UserManager.FindByEmailAsync(name);
            var profile = Database.UserProfileManager.Get(user.Id);

            Mapper.Initialize(cfg => { cfg.CreateMap<Message, MessageDTO>(); });
            
            foreach (var cp in profile.CampPlaces)
            {
                if (cp != null)
                {
                    foreach(var post in cp.Posts)
                    { 
                        var postDTO = new PostDTO
                        {
                            Id = post.Id,
                            CreationDate = DateTime.Now,
                            Text = post.Text,
                            CampPlace = cp.Name
                        };
                        postList.Add(postDTO);
                    }
                }
            }

            return postList;
        }

    }
}
