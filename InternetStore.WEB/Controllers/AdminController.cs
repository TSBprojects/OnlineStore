using AutoMapper;
using InternetStore.BLL.DataTransferObjects;
using InternetStore.BLL.Interfaces;
using InternetStore.WEB.App_Start;
using InternetStore.WEB.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace InternetStore.WEB.Controllers
{
    public class AdminController : Controller
    {
        IMapper Mapper;
        ICartService cartService;
        IProductService prodService;
        IAccountService userService;
        public AdminController(IProductService prodServ, IAccountService userServ, ICartService cartServ)
        {
            cartService = cartServ;
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

        public void OrderCompleted(int orderId)
        {
            cartService.OrderCompleted(orderId);
        }

        public ActionResult GetAllOrders()
        {
            return PartialView("_AdminOrderList",cartService.GetAllOrders());
        }

        public ActionResult GetUserOrders()
        {
            return PartialView("_AdminOrderList", cartService.GetAllOrders());
        }

        public ActionResult AddProduct()
        {
            List<string> tags = new List<string>();
            List<string> categories = new List<string>();
            foreach (var tag in prodService.GetTags())
            {
                tags.Add(tag.Name);
            }
            foreach (var cat in prodService.GetCategories())
            {
                categories.Add(cat.Name);
            }
            ViewBag.Tags = tags;
            ViewBag.Categories = categories;
            ViewBag.Change = false;
            return PartialView("_ProductForm");
        }

        public ActionResult AddProductPost(ProductModel model)
        {
            int productId;
            ModelState.Remove("productId");
            ModelState.Remove("ImageSetting");
            if (Request.Files.Count > 0)
            {
                ModelState.Remove("ProductImages");
            }
            if (string.IsNullOrEmpty(model.Tags[0]) || model.Tags[0].Equals("null"))
            {
                ModelState.AddModelError("Tags", "Выберите теги товара");
            }
            if (ModelState.IsValid)
            {
                productId = prodService.AddProduct(model.Name, model.PartialDescription,
                    model.FullDescription, model.Price, model.Rating, model.ProductCount, model.Category);
                prodService.AddTagsToProduct(productId, model.Tags[0].Split(','));

                prodService.AddImagesToProduct(productId, SaveImages(Request.Files), false);

                return Json("success");
            }
            else
            {
                List<string> tags = new List<string>();
                List<string> categories = new List<string>();
                foreach (var tag in prodService.GetTags())
                {
                    tags.Add(tag.Name);
                }
                foreach (var cat in prodService.GetCategories())
                {
                    categories.Add(cat.Name);
                }
                ViewBag.Tags = tags;
                ViewBag.Categories = categories;
                ViewBag.Change = false;
                return PartialView("_ProductForm", model);
            }
        }

        public ActionResult ChangeProduct(int prodId)
        {
            List<string> tags = new List<string>();
            List<string> categories = new List<string>();
            foreach(var tag in prodService.GetTags())
            {
                tags.Add(tag.Name);
            }
            foreach (var cat in prodService.GetCategories())
            {
                categories.Add(cat.Name);
            }
            ViewBag.Tags = tags;
            ViewBag.Categories = categories;
            ViewBag.prodId = prodId;
            ViewBag.Change = true;
            return PartialView("_ProductForm", GetFilledModel(prodId));
        }

        public ActionResult ChangeProductPost(ProductModel model)
        {
            if (Request.Files.Count > 0 || (model.ImageSetting != null && model.ImageSetting.Equals("none")))
            {
                ModelState.Remove("ProductImages");
            }
            if (string.IsNullOrEmpty(model.Tags[0]) || model.Tags[0].Equals("null"))
            {
                ModelState.AddModelError("Tags", "Выберите теги товара");
            }
            if (ModelState.IsValid)
            {
                prodService.ChangeProduct(model.productId,model.Name,model.PartialDescription,
                    model.FullDescription,model.Price,model.Rating, model.ProductCount,model.Category);
                prodService.AddTagsToProduct(model.productId,model.Tags[0].Split(','));

                if(model.ImageSetting.Equals("replace"))
                {
                    prodService.AddImagesToProduct(model.productId, SaveImages(Request.Files), true);
                }
                else if(model.ImageSetting.Equals("add"))
                {
                    prodService.AddImagesToProduct(model.productId, SaveImages(Request.Files), false);
                }
                return Json("success");
            }
            else
            {
                List<string> tags = new List<string>();
                List<string> categories = new List<string>();
                foreach (var tag in prodService.GetTags())
                {
                    tags.Add(tag.Name);
                }
                foreach (var cat in prodService.GetCategories())
                {
                    categories.Add(cat.Name);
                }
                ViewBag.Tags = tags;
                ViewBag.Categories = categories;
                ViewBag.Change = true;
                return PartialView("_ProductForm", model);
            }
        }

        public void RemoveProduct(int prodId)
        {
            prodService.RemoveProduct(prodId);
        }

        public ActionResult GetAllProducts()
        {
            if (Roles.IsUserInRole(User.Identity.Name, "User"))
            {
                return RedirectToAction("GetUserOrders");
            }
            else
            {
                return PartialView("_AdminProductList", prodService.GetProducts());
            }
        }

        private ProductModel GetFilledModel(int prodId)
        {
            ProductDTO prod = prodService.GetProduct(prodId);
            ProductModel Model = new ProductModel();
            Model.productId = prodId;
            Model.Name = prod.Name;
            Model.PartialDescription = prod.PartialDescription;
            Model.FullDescription = prod.FullDescription;
            Model.Price = prod.Price;
            Model.ProductCount = prod.ProductCount;
            Model.Rating = prod.Rating;
            return Model;
        }

        private string[] SaveImages(HttpFileCollectionBase files)
        {
            string[] imagesPath = new string[files.Count];
            for (int i = 0; i < files.Count; i++)
            {
                imagesPath[i] = "/Content/dbImages/" + Path.GetFileName(files[i].FileName);
                files[i].SaveAs(Server.MapPath(imagesPath[i]));
            }
            return imagesPath;
        }
    }
}