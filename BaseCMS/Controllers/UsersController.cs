using System;
using System.Web.Mvc;
using System.Web.Security;
using BaseCMS.Providers;
using BaseCMS.Models;
using BaseCMS.Models.Domain;
using BaseCMS.Models.Repositories;

namespace BaseCMS.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        public IMembershipService MembershipService { get; set; }
        private IUserInfoRepo _userInfoRepo { get; set; }

        public UsersController(IUserInfoRepo userInfoRepo)
        {
            _userInfoRepo = userInfoRepo;
            if (MembershipService == null) { MembershipService = new AccountMembershipService(); }
        }

        [AllowAnonymous]
        public ActionResult Index(string returnUrl)
        {
            var language = System.Web.HttpContext.Current.Request.Cookies["Custom"] == null ? "en" : System.Web.HttpContext.Current.Request.Cookies["Custom"]["Lang"];
            Session["CurrentLang"] = language;

            if (MembershipService.IsLoggedIn)
               // return RedirectToUrl(returnUrl);
                RedirectToAction("Index", "Home");

            return View();
        }

        //[RequireHttps]
        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public ActionResult Index(Login model, string returnUrl)
        {
            var language = System.Web.HttpContext.Current.Request.Cookies["Custom"] == null ? "en" : System.Web.HttpContext.Current.Request.Cookies["Custom"]["Lang"];
            Session["CurrentLang"] = language;

            if (ModelState.IsValid)
            {
                if (MembershipService.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "The user name or password provided is incorrect.");
            }
            return View("Index");
        }

        [Authorize(Roles = "user")]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
        [Authorize(Roles = "admin")]
        public ActionResult LogoutAdmin()
        {
            FormsAuthentication.SignOut();

            return Redirect("~/Admin/login.aspx");
        }

        [Authorize(Roles = "admin, user")]
        public ActionResult MyAccount()
        {
            var language = System.Web.HttpContext.Current.Request.Cookies["Custom"] == null ? "en" : System.Web.HttpContext.Current.Request.Cookies["Custom"]["Lang"];
            Session["CurrentLang"] = language;

            var u = _userInfoRepo.SingleOrDefault(User.Identity.Name);
            var account = new Account
            {
                ID = u.ID,
                Active = u.Active,
                Email = u.Email,
                Role = u.Role,
                UserName = u.Username
            };
            return View(account);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult MyAccount(Account model)
        {
            var language = System.Web.HttpContext.Current.Request.Cookies["Custom"] == null ? "en" : System.Web.HttpContext.Current.Request.Cookies["Custom"]["Lang"];
            Session["CurrentLang"] = language;

            return View();
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            var language = System.Web.HttpContext.Current.Request.Cookies["Custom"] == null ? "en" : System.Web.HttpContext.Current.Request.Cookies["Custom"]["Lang"];
            Session["CurrentLang"] = language;

            return View();
        }
        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public ActionResult Register(Register model)
        {
            MembershipService.CreateUser(new UserInfo
            {
                Active = true,
                Email = "",
                Password = model.Password,
                Role = "user",
                Username = model.UserName
            });
            return Index(new Login
            {
                Password = model.Password,
                UserName = model.UserName
            }, "");
        }

        [AllowAnonymous]
        public ActionResult CheckUsername(string name)
        {
            if(_userInfoRepo.SingleOrDefault(name) == null)
                return Json("new", JsonRequestBehavior.AllowGet);
            return Json("error", JsonRequestBehavior.AllowGet);
        }
    }
}