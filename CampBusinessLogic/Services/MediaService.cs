using System;
using System.Threading.Tasks;
using CampDataAccess.Entities;
using CampBusinessLogic.Interfaces;
using CampDataAccess.Interfaces;

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

        public async Task<string> GetMediaPath(int mediaId)
        {
            if(mediaId == -1)
            {
                throw new ArgumentException("It’s ok, media Id = -1, no media should be returned");
            }
            else
            {
                var media = await Database.MediaManager.GetAsync(mediaId);
                return media.Path;
            }
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