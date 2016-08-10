using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CampBusinessLogic.DTO;
using CampBusinessLogic.Infrastructure;

namespace CampBusinessLogic.Interfaces
{
    interface IMediaService
    {
        Task<OperationDetails> SaveMedia(string mediaPath);
        string GetMediaPath(int mediaId);
    }
}
