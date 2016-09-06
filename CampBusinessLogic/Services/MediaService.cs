using System;
using System.Threading.Tasks;
using CampDataAccess.Entities;
using CampBusinessLogic.Interfaces;
using CampDataAccess.Interfaces;
using System.IO;

namespace CampBusinessLogic.Services
{
    public class MediaService : IMediaService
    {
        IUnitOfWork Database { get; set; }

        public MediaService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task<int> SaveMedia(string path)
        {
            if (String.IsNullOrEmpty(path))
                throw new ArgumentNullException(path);

            var media = new Media { Type = "Image", Path = path };

            Database.MediaManager.Create(media);

            await Database.SaveAsync();

            return media.Id;
        }

        public string GetMediaBase64(int mediaId)
        {
            if(mediaId == -1)
                return "";

            var media = Database.MediaManager.Get(mediaId);

            byte[] imageArray = File.ReadAllBytes(media.Path);

            string imageBase64 = Convert.ToBase64String(imageArray);

            return imageBase64;
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public void Delete(int mediaId)
        {
            Database.MediaManager.Delete(mediaId);
        }
    }
}