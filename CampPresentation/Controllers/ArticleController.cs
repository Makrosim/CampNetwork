using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CampBusinessLogic.Interfaces;
using CampBusinessLogic.DTO;
using System.Threading.Tasks;

namespace CampAuth.Controllers
{
    public class ArticleController : Controller
    {
        private IPostService postService;

        public ArticleController(IPostService postService)
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
        public async Task<RedirectResult> Index(int Id, PostDTO postDTO)
        {
            await postService.CreatePost(Id, postDTO);

            return Redirect("/Home/Index");
        }
    }
}