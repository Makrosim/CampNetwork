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
    public class MessageService : IMessageService
    {
        IUnitOfWork Database { get; set; }

        public MessageService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task<OperationDetails> CreateUsersMessage(string name, MessageDTO messageDTO)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException(name);

            messageDTO.Date = DateTime.Now;

            var user = await Database.UserManager.FindByNameAsync(name);
            var prof = Database.UserProfileManager.Get(user.Id);

            Mapper.Initialize(cfg => { cfg.CreateMap<MessageDTO, Message>()
                .ForMember(dest => dest.Author, opts => opts.MapFrom(p => prof));
            });

            var message = Mapper.Map<MessageDTO, Message>(messageDTO);

            Database.MessageManager.Create(message);

            await Database.SaveAsync();

            var post = Database.PostManager.Get(messageDTO.PostId);
            post.Messages.Add(message.Id);

            await Database.SaveAsync();

            return new OperationDetails(true, "Успех", "");
        }

        public List<MessageDTO> GetAllPostMessages(int postId)
        {
            var post = Database.PostManager.Get(postId);
            var messages = new List<MessageDTO>();

            Mapper.Initialize(cfg => { cfg.CreateMap<Message, MessageDTO>()
                .ForMember(dest => dest.FirstName, opts => opts.MapFrom(src => src.Author.FirstName))
                .ForMember(dest => dest.LastName, opts => opts.MapFrom(src => src.Author.LastName)); });

            foreach (var messageId in post.Messages)
            {
                var message = Database.MessageManager.Get(messageId);
                var messageDTO = Mapper.Map<Message, MessageDTO>(message);

                messages.Add(messageDTO);
            }

            return messages;
        }

        public async Task<OperationDetails> DeleteUsersMessage(int messageId, int postId)
        {
            var message = Database.MessageManager.Get(messageId);
            Database.MessageManager.Delete(message.Id);

            var post = Database.PostManager.Get(postId);
            post.Messages.Remove(messageId);

            Database.PostManager.Update(post);
            await Database.SaveAsync();

            return new OperationDetails(true, "Успех", "");
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
