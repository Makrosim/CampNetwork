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

        public List<PostDTO> GetAllUsersPosts(string name)
        {
            var postList = new List<PostDTO>();
            var user = Database.UserProfileManager.Get(Convert.ToInt32(name));

            foreach (var cpId in user.CampPlacesId)
            {
                if (cpId != null)
                {
                    var tmpPost = Database.PostManager.Get((int)cpId);
                    var messages = new List<MessageDTO>();
                    foreach(var mesId in tmpPost.Messages)
                    {
                        var tmpMes = Database.MessageManager.Get(mesId);
                        var tmpMesDTO = new MessageDTO
                        {
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Text = tmpMes.Text,
                            Date = tmpMes.Date
                        };
                        messages.Add(tmpMesDTO);
                    }
                    var tmpPostDTO = new PostDTO
                    {
                        Text = tmpPost.Text,
                        Messages = messages,
                        CampPlace = tmpPost.CampPlace.ToString() //Возможна проблема
                    };
                    postList.Add(tmpPostDTO);
                }
            }

            return postList;
        }

    }
}
