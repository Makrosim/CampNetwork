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

        public async Task AttachAvatar(string userName, string path)
        {
            if (String.IsNullOrEmpty(path))
                throw new ArgumentNullException(path);

            var user = await Database.UserManager.FindByNameAsync(userName);
            var profile = user.UserProfile;

            if(profile.Avatar == null)
            {
                var media = new Media { Type = "Image", Path = path, UserProfile = profile };

                Database.MediaManager.Create(media);
                await Database.SaveAsync();

                profile.Avatar = media;
            }
            else
            {
                profile.Avatar.Path = path;
                Database.MediaManager.Update(profile.Avatar);

                await Database.SaveAsync();
            }
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