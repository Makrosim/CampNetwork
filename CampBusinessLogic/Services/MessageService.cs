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
    public class MessageService : IMessageService
    {
        IUnitOfWork Database { get; set; }

        public MessageService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task CreateUsersMessage(string name, MessageDTO messageDTO)
        {
            messageDTO.Date = DateTime.Now;

            Mapper.Initialize(cfg => { cfg.CreateMap<MessageDTO, Message>()
                .ForMember("Id", c => c.Ignore());
            });

            var message = Mapper.Map<MessageDTO, Message>(messageDTO);
            message.UserProfile = (await Database.UserManager.FindByNameAsync(messageDTO.Author)).UserProfile;

            Database.MessageManager.Create(message);

            await Database.SaveAsync();

            var post = Database.PostManager.Get(messageDTO.PostId);
            post.Messages.Add(message);

            Database.PostManager.Update(post);

            await Database.SaveAsync(); 
        }

        public List<MessageDTO> GetAllPostMessages(int postId)
        {
            var post = Database.PostManager.Get(postId);
            var messages = new List<MessageDTO>();

            Mapper.Initialize(cfg => { cfg.CreateMap<Message, MessageDTO>()
                .ForMember(dest => dest.FirstName, opts => opts.MapFrom(src => src.UserProfile.FirstName))
                .ForMember(dest => dest.LastName, opts => opts.MapFrom(src => src.UserProfile.LastName))
                .ForMember(dest => dest.Author, opts => opts.MapFrom(src => src.UserProfile.User.UserName));
            });

            foreach (var message in post.Messages)
            {
                var messageDTO = Mapper.Map<Message, MessageDTO>(message);

                messages.Add(messageDTO);
            }

            return messages;
        }

        public async Task DeleteUsersMessage(int messageId, int postId)
        {
            var message = Database.MessageManager.Get(messageId);
            Database.MessageManager.Delete(message.Id);

            var post = Database.PostManager.Get(postId);
            post.Messages.Remove(message);

            Database.PostManager.Update(post);
            await Database.SaveAsync();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
