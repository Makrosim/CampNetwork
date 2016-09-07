using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CampBusinessLogic.DTO;

namespace CampBusinessLogic.Interfaces
{
    public interface IMediaService
    {
        Task AttachAvatar(string userName, string mediaPath);
        string GetMediaBase64(int mediaId);
        void Delete(int mediaId);
    }
}
