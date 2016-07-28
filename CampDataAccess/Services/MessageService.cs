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

        public List<MessageDTO> GetAllPostMessages(int postId)
        {
            var post = Database.PostManager.Get(postId);
            var messages = new List<MessageDTO>();

            foreach (var messageId in post.Messages)
            {
                var message = Database.MessageManager.Get(messageId);
                var messageDTO = Mapper.Map<Message, MessageDTO>(message); //Сконфигурировать маппинг
                messageDTO.FirstName = message.Author.FirstName;
                messageDTO.LastName = message.Author.LastName;
                messages.Add(messageDTO);
            }

            return messages;
        }

        public async Task<OperationDetails> CreateUsersMessage(MessageDTO messageDTO)
        {
            var user = await Database.UserManager.FindByEmailAsync(messageDTO.Email);
            var prof = Database.UserProfileManager.Get(user.Id);

            var mes = new Message
            {
                Id = messageDTO.Id,
                Author = prof,
                Text = messageDTO.Text,
                Date = DateTime.Now // Плохо? Время, когда обрабатывается сервером?
            };

            Database.MessageManager.Create(mes);
            await Database.SaveAsync();

            var post = Database.PostManager.Get(messageDTO.PostId);
            post.Messages.Add(mes.Id);

            await Database.SaveAsync();

            return new OperationDetails(true, "Успех", "");
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
