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
    public class CartController : Controller
    {
        IMapper Mapper;
        ICartService cartService;
        IAccountService userService;
        public CartController(ICartService cartServ, IAccountService userServ)
        {
            cartService = cartServ;
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

        [Route("cart")]
        public ActionResult Cart()
        {
            return View();
        }

        public ActionResult OrderItemsList()
        {
            int userId = userService.GetUser(User.Identity.Name).Id;
            return PartialView("_OrderItemsList",cartService.GetCurrentOrder(userId).OrderItems.ToList());
        }

        public int AddProduct(int prodId, int count)
        {
            int userId = userService.GetUser(User.Identity.Name).Id;
            cartService.AddProductToCart(userId, prodId, count);
            return count;
        }

        public void RemoveProductFromCart(int orderItemId)
        {
            cartService.RemoveProductFromOrder(orderItemId);
        }

        public void UpdateCart(List<OrderItem> model)
        {
            cartService.UpdateProductCountInOrder(Mapper.Map<List<OrderItem>, List<UpdateOrderItemDTO>>(model));
        }

        public string ProductCount()
        {
            if (User.Identity.IsAuthenticated)
            {
                int userId = userService.GetUser(User.Identity.Name).Id;
                return cartService.GetProductCountInCart(userId).ToString();
            }
            else
            {
                return "0";
            }
        }

        public ActionResult CheckOutOrder()
        {
            int userId = userService.GetUser(User.Identity.Name).Id;
            return PartialView("_CheckoutOrderList", cartService.GetCurrentOrder(userId).OrderItems.ToList());
        }

        [HttpGet]
        [Route("check-out")]
        public ActionResult CheckOut()
        {
            int userId = userService.GetUser(User.Identity.Name).Id;
            if (cartService.GetCurrentOrder(userId).OrderItems.Count != 0)
            {
                CheckOutUserModel chum = new CheckOutUserModel();
                chum.GuestModel = new CheckOutGuestModel();
                chum.GuestModel.RegModel = new RegistrationModel();
                return View(chum);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        [Route("check-out")]
        [ValidateAntiForgeryToken]
        public ActionResult CheckOut(CheckOutUserModel model)
        {
            int userId;
            RegistrationDTO regDTO;
            ServiceResult<RegistrationDTO> r;
            if (ModelState.IsValid)
            {
                if (model.GuestModel != null)
                {
                    if(!model.GuestModel.CreateAccount)
                    {
                        cartService.AcceptGuestOrder(MapGuestOrder(model));
                        userId = userService.GetUser(User.Identity.Name).Id.GetHashCode();
                        return RedirectToAction("info", "Home", new { str = "order", userid = userId });
                    }
                    else
                    {
                        regDTO = MapReg(model);
                        r = userService.RegisterUser(regDTO, Url.Action("ConfirmEmail", "Account", new { userid = regDTO.userId }, Request.Url.Scheme));
                        if (r.Exception == null)
                        {
                            cartService.AcceptGuestOrder(MapGuestOrder(model));
                            return RedirectToAction("info", "Home", new { str = "email", userid = regDTO.userId });
                        }
                        else
                        {
                            ModelState.AddModelError("GuestModel."+r.Exception.Property, r.Exception.Message);
                            return View(model);
                        }
                    }
                }
                else
                {
                    cartService.AcceptUserOrder(MapUserOrder(model));
                    userId = userService.GetUser(User.Identity.Name).Id.GetHashCode();
                    return RedirectToAction("info", "Home", new { str = "order", userid = userId });
                }
            }
            else return View(model);
        }

        private OrderDTO MapGuestOrder(CheckOutUserModel model)
        {
            return new OrderDTO()
            {
                Comment = model.Comment,
                DeliveryMethod = model.DeliveryMethod,
                PaymentMethod = model.PaymentMethod,
                UserId = userService.GetUser(User.Identity.Name).Id,
                User = new UserDTO()
                {
                    FirstName = model.GuestModel.FirstName,
                    LastName = model.GuestModel.LastName,
                    Email = model.GuestModel.Email,
                    PhoneNumber = model.GuestModel.PhoneNumber,
                    Address = model.GuestModel.Address,
                    ZipCode = model.GuestModel.ZipCode
                }
            };
        }

        private OrderDTO MapUserOrder(CheckOutUserModel model)
        {
            return new OrderDTO()
            {
                Comment = model.Comment,
                DeliveryMethod = model.DeliveryMethod,
                PaymentMethod = model.PaymentMethod,
                UserId = userService.GetUser(User.Identity.Name).Id
            };
        }

        private RegistrationDTO MapReg(CheckOutUserModel model)
        {
            return new RegistrationDTO()
            {
                userId = userService.GetUser(User.Identity.Name).Id,
                Address = model.GuestModel.Address,
                Email = model.GuestModel.Email,
                FirstName = model.GuestModel.FirstName,
                LastName = model.GuestModel.LastName,
                HashPassword = model.GuestModel.RegModel.Password.GetHashCode(),
                PhoneNumber = model.GuestModel.PhoneNumber,
                ZipCode = model.GuestModel.ZipCode,
                IP = Request.UserHostAddress
            };
        }
    }
}