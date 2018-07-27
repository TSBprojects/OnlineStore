using AutoMapper;
using InternetStore.BLL.DataTransferObjects;
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
    public class ProductController : Controller
    {
        IMapper Mapper;
        IProductService prodService;
        IAccountService userService;
        public ProductController(IProductService prodServ, IAccountService userServ)
        {
            userService = userServ;
            Mapper = AutoMapperWEBConfig.Mapper;
            prodService = prodServ;
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

        [Route("plants/page{id}")]
        public ActionResult Plants(int id)
        {
            int pageCount;
            ViewBag.siftCategory = false;
            ViewBag.siftTag = false;
            ViewBag.search = false;
            pageCount = prodService.GetProductPageCount();
            if (id > 0 && id <= pageCount || pageCount == 0)
            {
                return View(id);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [Route("plants/id{id}")]
        public ActionResult CurrentPlant(int id)
        {
            ProductDTO prod = prodService.GetProduct(id);
            if (prod == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(prod);
            }
        }

        [Route("plants/q={query}")]
        public ActionResult SearchPlants(string query)
        {
            ViewBag.siftCategory = false;
            ViewBag.siftTag = false;
            ViewBag.search = true;
            ViewBag.query = query;
            if (!query.Equals(""))
            {
                return View("Plants", 0);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [Route("plants/siftbytag{tagid}")]
        public ActionResult SiftTagPlants(int tagid)
        {
            ViewBag.siftCategory = false;
            ViewBag.siftTag = true;
            ViewBag.search = false;
            if (tagid > 0 && tagid <= prodService.GetTags().Count())
            {
                return View("Plants", tagid);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [Route("plants/siftbycat{catid}")]
        public ActionResult SiftCatPlants(int catid)
        {
            ViewBag.siftCategory = true;
            ViewBag.siftTag = false;
            ViewBag.search = false;
            if (catid > 0 && catid <= prodService.GetCategories().Count())
            {
                return View("Plants", catid);
            }
            else
            {
                return HttpNotFound();
            }
        }

        public ActionResult Search(string query, bool isAjax)
        {
            ViewBag.full = false;
            ViewBag.isAjax = isAjax;
            return PartialView("_ProductList", prodService.SearchProducts(query));
        }

        public ActionResult SiftByCategory(int categoryId, bool isAjax)
        {
            ViewBag.full = false;
            ViewBag.isAjax = isAjax;
            return PartialView("_ProductList", prodService.SiftProductsByCategory(categoryId));
        }

        public ActionResult SiftByTag(int tagId)
        {
            ViewBag.full = false;
            ViewBag.isAjax = false;
            return PartialView("_ProductList", prodService.SiftProductsByTag(tagId));
        }

        public ActionResult ProductList(int pageId, bool full, bool isAjax)
        {
            if(full)
            {
                ViewBag.isAjax = isAjax;
                ViewBag.full = full;
                ViewBag.currentPage = pageId;
                ViewBag.pageCount = prodService.GetProductPageCount();
            }
            return PartialView("_ProductList", prodService.GetPartOfProducts(pageId));
        }

        public ActionResult CategoryList(bool active, int activeCat)
        {
            ViewBag.active = active;
            ViewBag.activeCategory = activeCat;
            return PartialView("_CategoryList", prodService.GetCategories());
        }
    }
}