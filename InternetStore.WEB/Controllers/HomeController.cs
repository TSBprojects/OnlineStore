using AutoMapper;
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
    public class HomeController : Controller
    {
        IMapper Mapper;
        IAccountService userService;
        public HomeController(IAccountService serv)
        {
            userService = serv;
            Mapper = AutoMapperWEBConfig.Mapper;
        }
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            HttpCookie auth = HttpContext.Request.Cookies.Get(".AUTH");
            if (!User.Identity.IsAuthenticated && auth == null)
            {
                userService.LoginGuest(Request.UserHostAddress, HttpContext);
            }
            else if (Roles.IsUserInRole(User.Identity.Name, "Guest"))
            {
                userService.RefreshGuest(Convert.ToInt32(User.Identity.Name));
            }
        }

        public ActionResult Index()
        {
            return View();
        }

        [Route("info/{str}/{userid}")]
        public ActionResult Info(string str, int userid)
        {
            int userId = 0;
            if (User.Identity.IsAuthenticated)
            {
                userId = userService.GetUser(User.Identity.Name).Id;
            }

            if(str.Equals("email") && userid == userId)
            {
                ViewBag.subject = "Почти всё!";
                ViewBag.body = "Мы отправили вам письмо с описанием вашего заказа! А также для подтверждении " +
                    "регистрации пройдите по ссылке во втором письме, отправленном на указаный вами email! "+
                    "Ссылка действительна в течении 1 часа.";
                return View("Info");
            }
            else if (str.Equals("confirm") && userid == userId)
            {
                ViewBag.subject = "Вы успешно зарегистрировались!";
                ViewBag.body = "";
                return View("Info");
            }
            else if (str.Equals("order") && userid == userId) 
            {
                ViewBag.subject = "Заказ успешно оформлен!";
                ViewBag.body = "На указанный почтовый адрес отправлено письмо с описанием вашего заказа!";
                return View("Info");
            }
            else
            {
                return HttpNotFound();
            }
        }
    }
}