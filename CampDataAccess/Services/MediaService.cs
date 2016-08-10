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
    public class MediaService : IMediaService
    {
        IUnitOfWork Database { get; set; }

        public MediaService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task<OperationDetails> SaveMedia(string path)
        {
            if (String.IsNullOrEmpty(path))
                throw new ArgumentNullException(path);

            var media = new Media { Type = "Image", Path = "path" };

            Database.MediaManager.Create(media);

            await Database.SaveAsync();

            return new OperationDetails(true, "Операция успешно завершена", "");
        }

        public string GetMediaPath(int mediaId)
        {
            var media = Database.MediaManager.Get(mediaId);

            return media.Path;
        }


        public void Dispose()
        {
            Database.Dispose();
        }
    }
}