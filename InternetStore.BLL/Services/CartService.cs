using AutoMapper;
using Internet_store.DAL.Entities;
using InternetStore.BLL.DataTransferObjects;
using InternetStore.BLL.Infrastructure;
using InternetStore.BLL.Interfaces;
using InternetStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetStore.BLL.Services
{
    public class CartService : ICartService
    {
        IMapper Mapper;
        IUnitOfWork db;
        public CartService(IUnitOfWork uow)
        {
            db = uow;
            Mapper = AutoMapperBLLConfig.Mapper;
        }

        public IEnumerable<OrderDTO> GetAllOrders()
        {
            return Mapper.Map<IEnumerable<Order>, IEnumerable<OrderDTO>>(db.Orders.Find(o => o.Status != "Not Confirmed"));
        }

        public IEnumerable<OrderDTO> GetUserOrders(int userId)
        {
            return Mapper.Map<IEnumerable<Order>, IEnumerable<OrderDTO>>(db.Orders.Find(o => o.UserId == userId && o.Status != "Not Confirmed"));
        }

        public IEnumerable<OrderItemDTO> GetOrderItems(int orderId)
        {
            return Mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemDTO>>(db.OrderItems.Find(oi => oi.OrderId == orderId));
        }

        public int GetProductCountInCart(int userId)
        {
            Order order = db.Orders.GetAll().FirstOrDefault(o => o.UserId == userId && o.Status.Equals("Not Confirmed"));
            if (order != null)
            {
                return order.ProductsCount;
            }
            else
            {
                return 0;
            }
        }

        public void OrderCompleted(int orderId)
        {
            db.Orders.Get(orderId).Status = "Completed";
            db.Save();
        }

        public OrderDTO GetCurrentOrder(int userId)
        {
            return Mapper.Map<Order, OrderDTO>(db.Orders.FirstOrDefault(o => o.UserId == userId && o.Status.Equals("Not Confirmed")));
        }

        public void AcceptGuestOrder(OrderDTO odto)
        {
            User user = db.Users.Get(odto.UserId);
            Order order = db.Orders.FirstOrDefault(o => o.UserId == user.Id && o.Status.Equals("Not Confirmed"));

            order.Status = "During";
            order.Comment = odto.Comment;
            order.DeliveryMethod = odto.DeliveryMethod;
            order.PaymentMethod = odto.PaymentMethod;

            user.FirstName = odto.User.FirstName;
            user.LastName = odto.User.LastName;
            user.Email = odto.User.Email;
            user.PhoneNumber = odto.User.PhoneNumber;
            user.Address = odto.User.Address;
            user.ZipCode = odto.User.ZipCode;

            db.Save();

            EmailSender.Send(user.Email,"Ваш заказ на BeGreen!", EmailSender.GetHtmlEmail(user, order));

            CreateCart(user.Id);
        }

        public void AcceptUserOrder(OrderDTO odto)
        {
            User user = db.Users.Get(odto.UserId);
            Order order = db.Orders.FirstOrDefault(o => o.UserId == user.Id && o.Status.Equals("Not Confirmed"));

            order.Status = "During";
            order.Comment = odto.Comment;
            order.DeliveryMethod = odto.DeliveryMethod;
            order.PaymentMethod = odto.PaymentMethod;

            db.Save();

            EmailSender.Send(user.Email, "Ваш заказ на BeGreen!", EmailSender.GetHtmlEmail(user, order));

            CreateCart(user.Id);
        }

        public int CreateCart(int userId)
        {
            Order order = new Order()
            {
                UserId = userId,
                Status = "Not Confirmed",
                ProductsCount = 0,
                OrderPrice = 0
            };
            db.Orders.Create(order);
            db.Save();
            return order.Id;
        }

        public void AddProductToCart(int userId, int productId, int count = 1)
        {
            OrderItem oItem;
            Order order = db.Orders.FirstOrDefault(o => o.UserId == userId && o.Status.Equals("Not Confirmed"));
            if (order == null)
            {
                order = db.Orders.Get(CreateCart(userId));
            }
            order.OrderPrice += db.Products.Get(productId).Price * count;
            order.ProductsCount += count;
            oItem = db.OrderItems.FirstOrDefault(oi => oi.OrderId == order.Id && oi.ProductId == productId);
            if (oItem == null)
            {
                db.OrderItems.Create(new OrderItem() { OrderId = order.Id, ItemCount = count, ProductId = productId });
            }
            else
            {
                oItem.ItemCount += count;
            }
            db.Save();
        }

        public void UpdateProductCountInOrder(List<UpdateOrderItemDTO> oicList)
        {
            Order order;
            OrderItem ordItem;
            foreach (UpdateOrderItemDTO oic in oicList)
            {
                if(oic.Id > 0)
                {
                    order = db.Orders.FirstOrDefault(o => o.Id == db.OrderItems.Get(oic.Id).OrderId);
                    ordItem = db.OrderItems.Get(oic.Id);
                    order.OrderPrice -= ordItem.ItemCount * ordItem.Product.Price;
                    order.OrderPrice += oic.ItemCount * ordItem.Product.Price;
                    order.ProductsCount -= ordItem.ItemCount;
                    order.ProductsCount += oic.ItemCount;
                    ordItem.ItemCount = oic.ItemCount;
                    db.Save();
                }
            }
        }

        public void RemoveProductFromOrder(int orderItemId)
        {
            OrderItem ordItem = db.OrderItems.Get(orderItemId);
            Order order = db.Orders.FirstOrDefault(o => o.Id == ordItem.OrderId);
            order.OrderPrice -= ordItem.ItemCount * ordItem.Product.Price;
            order.ProductsCount -= ordItem.ItemCount;
            db.OrderItems.Delete(orderItemId);
            db.Save();
        }     
    }
}
