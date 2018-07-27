using AutoMapper;
using InternetStore.BLL.DataTransferObjects;
using InternetStore.BLL.Infrastructure;
using InternetStore.BLL.Interfaces;
using InternetStore.WEB.App_Start;
using InternetStore.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace InternetStore.WEB.Controllers
{
    public class AccountController : Controller
    {
        IMapper Mapper;
        IAccountService userService;
        public AccountController(IAccountService serv)
        {
            userService = serv;
            Mapper = AutoMapperWEBConfig.Mapper;
        }
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            HttpCookie auth = HttpContext.Request.Cookies[".AUTH"];
            if (!User.Identity.IsAuthenticated && auth == null)
            {
                userService.LoginGuest(Request.UserHostAddress, HttpContext);
            }
            else if(Roles.IsUserInRole(User.Identity.Name, "Guest"))
            {
                userService.RefreshGuest(Convert.ToInt32(User.Identity.Name));
            }
        }

        public string AddSubcribe(string email)
        {
            if(!email.Equals(""))
            {
                return userService.AddSubscriber(email).ToString();
            }
            return "empty";
        }

        [Authorize]
        [Route("logout")]
        public ActionResult Logout()
        {
            userService.LogoutUser();
            return RedirectToAction("Index", "Home");
        }

        public string UserName()
        {
            return userService.GetUser(User.Identity.Name).FirstName;
            //return "";
        }

        public ActionResult CheckOutLogin()
        {
            return PartialView("_CheckoutLogin");
        }

        [Route("login")]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated && !Roles.IsUserInRole(User.Identity.Name, "Guest"))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [Route("login")]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl, bool ischeckout)
        {
            ServiceResult<LoginDTO> r;
            if (ModelState.IsValid)
            {
                if(User.Identity.IsAuthenticated && Roles.IsUserInRole(User.Identity.Name,"Guest"))
                {
                    r = userService.LoginUser(Mapper.Map<LoginModel, LoginDTO>(model), Convert.ToInt32(User.Identity.Name));
                }
                else
                {
                    r = userService.LoginUser(Mapper.Map<LoginModel, LoginDTO>(model), null);
                }
                if (r.Exception == null)
                {
                    if(!ischeckout)
                    {
                        if (string.IsNullOrEmpty(returnUrl)) return RedirectToAction("Index", "Home");
                        else return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("CheckOut", "Cart");
                    }
                }
                else
                {
                    ModelState.AddModelError(r.Exception.Property, r.Exception.Message);
                    return View(model);
                }
            }
            else return View(model);
        }

        [Route("confirm/{userid}")]
        public ActionResult ConfirmEmail(int userid)
        {
            UserDTO u = userService.GetUser(User.Identity.Name);
            if(userid == u.Id)
            {
                userService.ConfirmEmail(u.Id);
                return RedirectToAction("info", "Home", new { str = "confirm", userid = u.Id });
            }
            else
            {
                return HttpNotFound();
            }
        }
    }
}