using System.Collections.Generic;
using System.Web.Mvc;
using BaseCMS.Models.Repositories;

namespace BaseCMS.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private readonly IUserInfoRepo _userInfoRepo;


        public HomeController( IUserInfoRepo userInfoRepo)
        {
            _userInfoRepo = userInfoRepo;
        }

        public ActionResult Index()
        {
            var language = System.Web.HttpContext.Current.Request.Cookies["Custom"] == null ? "en" : System.Web.HttpContext.Current.Request.Cookies["Custom"]["Lang"];
            Session["CurrentLang"] = language;

            return View();
        }

        public class FaqModel
        {
            public IList<KeyValuePair<string, string>> Faqs;
        }

    }

}
