using System;
using System.Collections.Generic;
using CampBusinessLogic.DTO;
using CampBusinessLogic.Infrastructure;

namespace CampBusinessLogic.Interfaces
{
    public interface IPostService
    {
        List<PostDTO> GetAllUsersPosts(string name);
    }
}
