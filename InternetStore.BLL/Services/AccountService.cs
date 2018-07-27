using AutoMapper;
using Internet_store.DAL.Entities;
using InternetStore.BLL.DataTransferObjects;
using InternetStore.BLL.Infrastructure;
using InternetStore.BLL.Interfaces;
using InternetStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;

namespace InternetStore.BLL.Services
{
    public class AccountService : IAccountService
    {
        IMapper Mapper;
        IUnitOfWork db;
        ICartService cartService;
        const int _GUEST_LOGOUT_TIME_SEC = 3600;
        static Dictionary<int, Timer> timerPool;
        public AccountService(IUnitOfWork uow, ICartService cartServ)
        {
            db = uow;
            cartService = cartServ;
            Mapper = AutoMapperBLLConfig.Mapper;
            if (timerPool == null)
            {
                timerPool = new Dictionary<int, Timer>();
            }
        }

        public UserDTO GetUser(string email)
        {
            int id;
            if (int.TryParse(email, out id))
            {
                return GetUser(id);
            }
            else
            {
                return Mapper.Map<User, UserDTO>(db.Users.FirstOrDefault(u => u.Email == email));
            }
        }

        public UserDTO GetUser(int id)
        {
            return Mapper.Map<User, UserDTO>(db.Users.Get(id));
        }

        public bool AddSubscriber(string email)
        {
            if (!db.Subscribers.Any(s => s.Email.Equals(email)))
            {
                db.Subscribers.Create(new Subscriber() { Email = email });
                db.Save();
                return true;
            }
            else
            {
                return false;
            }
        }

        public void RefreshGuest(int guestId)
        {
            timerPool[guestId].Change(_GUEST_LOGOUT_TIME_SEC * 1000, Timeout.Infinite);
        }

        public int LoginGuest(string ip, HttpContextBase context)
        {
            //slidingExpiration="false"
            const int _MILI = _GUEST_LOGOUT_TIME_SEC * 1000;
            User user = new User() { RoleId = 3, IP = ip };
            db.Users.Create(user);
            db.Save();
            user.Email = user.Id.ToString();
            db.Save();

            cartService.CreateCart(user.Id);

            var authTicket = new FormsAuthenticationTicket(1, user.Id.ToString(), DateTime.Now, DateTime.Now.AddSeconds(_GUEST_LOGOUT_TIME_SEC), true, FormsAuthentication.FormsCookieName);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket));
            cookie.Expires = DateTime.Now.AddSeconds(_GUEST_LOGOUT_TIME_SEC);
            context.Response.Cookies.Add(cookie);

            timerPool.Add(user.Id, new Timer(new TimerCallback(LogoutGuest), user.Id, _MILI, Timeout.Infinite));
            return user.Id;
        }

        public void LogoutGuest(object guestId)
        {
            if (db.Users.Any(u => u.Id == (int)guestId && u.RoleId == 3))
            {
                db.Users.Delete((int)guestId);
                db.Save();
            }
        }

        public ServiceResult<LoginDTO> LoginUser(LoginDTO logDTO, int? guestId)
        {
            User guestUser;
            User loginUser;
            Order loginUserOrder;
            Order guestUserOrder;
            int pass = logDTO.Password.GetHashCode();

            loginUser = db.Users.FirstOrDefault(u => u.Email == logDTO.Login && u.HashPassword == pass && u.RoleId != 3);
            if (loginUser != null)
            {
                if (guestId != null)
                {
                    //перенос корзины гостя юзеру, который авторизируется
                    guestUser = db.Users.Get((int)guestId);
                    loginUserOrder = db.Orders.FirstOrDefault(o => o.UserId == loginUser.Id && o.Status.Equals("Not Confirmed"));
                    guestUserOrder = db.Orders.FirstOrDefault(o => o.UserId == guestUser.Id && o.Status.Equals("Not Confirmed"));
                    loginUserOrder.OrderPrice = guestUserOrder.OrderPrice;
                    loginUserOrder.ProductsCount = guestUserOrder.ProductsCount;
                    if (guestUserOrder.OrderItems.Count != 0)
                    {
                        loginUserOrder.OrderItems = new List<OrderItem>(guestUserOrder.OrderItems);
                        foreach (OrderItem oi in loginUserOrder.OrderItems)
                        {
                            oi.OrderId = loginUserOrder.Id;
                        }
                    }
                    db.Save();
                    timerPool.Remove((int)guestId);
                    LogoutGuest((int)guestId);
                }
                if (logDTO.RememberMe)
                {
                    FormsAuthentication.SetAuthCookie(logDTO.Login, createPersistentCookie: true);
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(logDTO.Login, createPersistentCookie: false);
                }
                return new ServiceResult<LoginDTO>(null, null);
            }
            else
            {
                return new ServiceResult<LoginDTO>(null, string.Empty, "Неправильный логин или пароль");
            }
        }

        public void LogoutUser()
        {
            FormsAuthentication.SignOut();
        }

        public void ConfirmEmail(int userId)
        {
            Order newUserOrder;
            User guestUser = db.Users.Get(userId);
            User newUser = new User()
            {
                FirstName = guestUser.FirstName,
                LastName = guestUser.LastName,
                RoleId = 2,
                Email = guestUser.Email,
                HashPassword = guestUser.HashPassword,
                PhoneNumber = guestUser.PhoneNumber,
                Address = guestUser.Address,
                ZipCode = guestUser.ZipCode,
                IP = guestUser.IP
            };
            db.Users.Create(newUser);
            db.Save();
            cartService.CreateCart(newUser.Id);

            foreach (Order o in db.Orders.Find(o => o.UserId == guestUser.Id && !o.Status.Equals("Not Confirmed")))
            {
                newUserOrder = new Order()
                {
                    UserId = newUser.Id,
                    Comment = o.Comment,
                    DeliveryMethod = o.DeliveryMethod,
                    PaymentMethod = o.PaymentMethod,
                    OrderPrice = o.OrderPrice,
                    ProductsCount = o.ProductsCount,
                    Status = o.Status
                };
                db.Orders.Create(newUserOrder);
                db.Save();
                foreach (OrderItem oi in o.OrderItems)
                {
                    oi.OrderId = newUserOrder.Id;
                }
            }
            db.Save();
        }

        public ServiceResult<RegistrationDTO> RegisterUser(RegistrationDTO regDTO, string confirmUrl)
        {
            if (!db.Users.Any(u => u.Email == regDTO.Email))
            {
                User user = db.Users.Get(regDTO.userId);

                user.FirstName = regDTO.FirstName;
                user.LastName = regDTO.LastName;
                user.Email = regDTO.Email;
                user.HashPassword = regDTO.HashPassword;
                user.PhoneNumber = regDTO.PhoneNumber;
                user.Address = regDTO.Address;
                user.ZipCode = regDTO.ZipCode;

                db.Save();

                EmailSender.Send(user.Email, "Подтверждение регистрации на BeGreen!",
                         string.Format("Для завершения регистрации перейдите по ссылке:" +
                         "<a href='{0}' title='Подтвердить регистрацию'>подтвердить</a>", confirmUrl));
                return new ServiceResult<RegistrationDTO>(null, null);
            }
            else
            {
                return new ServiceResult<RegistrationDTO>(null, "Email", "Пользователь с таким адресом электронной почты уже зарегистрирован");
            }
        }
    }
}
