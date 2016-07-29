using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CampBusinessLogic.Interfaces;
using CampBusinessLogic.DTO;
using System.Threading.Tasks;

namespace CampAuth.Controllers
{
    public class PostController : Controller
    {
        private IPostService postService;

        public PostController(IPostService postService)
        {
            this.postService = postService;
        }
        
        [HttpGet]
        public ActionResult Index(int id)
        {
            ViewBag.CampPlaceID = id;

            return View();
        }

        [HttpGet]
        public RedirectResult Delete(int Id)
        {
            postService.DeletePost(Id);

            return Redirect("/Home/Index");
        }

        [HttpPost]
        public async Task<RedirectResult> Index(int campPlaceId, PostDTO postDTO)
        {
            await postService.CreatePost(campPlaceId, postDTO);

            return Redirect("/Home/Index");
        }
    }
}