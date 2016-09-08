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

        public async Task<MessageDTO> CreateUsersMessage(string userName, MessageDTO messageDTO)
        {
            var message = Mapper.Map<MessageDTO, Message>(messageDTO);
            message.UserProfile = (await Database.UserManager.FindByNameAsync(userName)).UserProfile;

            Database.MessageManager.Create(message);

            await Database.SaveAsync();

            var post = Database.PostManager.Get(messageDTO.PostId);
            post.Messages.Add(message);

            Database.PostManager.Update(post);

            await Database.SaveAsync();

            return Mapper.Map<Message, MessageDTO>(message);
        }

        public List<MessageDTO> GetAllPostMessages(int postId)
        {
            var post = Database.PostManager.Get(postId);
            var messages = new List<MessageDTO>();

            foreach (var message in post.Messages)
            {
                var messageDTO = Mapper.Map<Message, MessageDTO>(message);

                messages.Add(messageDTO);
            }

            return messages;
        }

        public async Task DeleteUsersMessage(string userName, int postId, int messageId)
        {
            var message = Database.MessageManager.Get(messageId);
            var post = Database.PostManager.Get(postId);

            if (!userName.Equals(message.UserProfile.User.UserName) || userName.Equals(message.Post.CampPlace.UserProfile.User.UserName))
                throw new UnauthorizedAccessException("У вас нет полномочий совершать это действие");

            post.Messages.Remove(message);
            Database.MessageManager.Delete(message.Id);

            await Database.SaveAsync();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
