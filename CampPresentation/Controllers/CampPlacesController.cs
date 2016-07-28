using System.Web.Mvc;
using System.Threading.Tasks;
using CampBusinessLogic.Interfaces;
using CampBusinessLogic.DTO;

namespace CampAuth.Controllers
{
    public class CampPlacesController : Controller
    {
        private ICampPlaceService campService;

        public CampPlacesController(ICampPlaceService campService)
        {
            this.campService = campService;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {

            ViewBag.CP = await campService.GetCampList(User.Identity.Name);
            ViewBag.Points = campService.GetPointsList();

            return View();
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            ViewBag.CP = campService.GetCampData(id);

            return View();
        }

        [HttpPost]
        public RedirectResult Update(CampPlaceDTO cp)
        {
            campService.Update(cp);

            return Redirect("/CampPlaces/Index");
        }

        [HttpGet]
        public ActionResult CreateCampPlace()
        {
            return View();
        }

        [HttpPost]
        public async Task<RedirectResult> CreateCampPlace(CampPlaceDTO place)
        {
            await campService.Create(User.Identity.Name, place);

            return Redirect("/CampPlaces/Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            campService.Delete(id);

            return View("Index");
        }
    }
}