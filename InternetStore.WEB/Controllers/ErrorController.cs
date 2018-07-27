using AutoMapper;
using InternetStore.BLL.Infrastructure;
using InternetStore.BLL.Interfaces;
using InternetStore.WEB.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace InternetStore.WEB.Controllers
{
    public class ErrorController : Controller
    {
        IMapper Mapper;
        IAccountService userService;
        public ErrorController(IAccountService userServ)
        {
            userService = userServ;
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
            else if (Roles.IsUserInRole(User.Identity.Name, "Guest"))
            {
                userService.RefreshGuest(Convert.ToInt32(User.Identity.Name));
            }
        }

        public ActionResult NotFound()
        {
            Response.StatusCode = 404;
            return View();
        }
    }
}