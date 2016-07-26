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

        public async Task<OperationDetails> CreateUsersMessage(MessageDTO messageDTO)
        {
            var user = await Database.UserManager.FindByEmailAsync(messageDTO.Email);
            var prof = Database.UserProfileManager.Get(Convert.ToInt32(user.Id));

            var mes = new Message
            {
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

        public async Task<OperationDetails> DeleteUsersMessage(int Id)
        {
            var mes = Database.MessageManager.Get(Id);
            Database.MessageManager.Delete(mes.Id);
            await Database.SaveAsync();

            return new OperationDetails(true, "Успех", "");
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
