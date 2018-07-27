using InternetStore.BLL.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetStore.BLL.Interfaces
{
    public interface ICartService
    {
        IEnumerable<OrderDTO> GetAllOrders();
        IEnumerable<OrderDTO> GetUserOrders(int userId);
        IEnumerable<OrderItemDTO> GetOrderItems(int orderId);
        OrderDTO GetCurrentOrder(int userId);
        int GetProductCountInCart(int userId);
        void OrderCompleted(int orderId);
        void AcceptGuestOrder(OrderDTO ordto);
        void AcceptUserOrder(OrderDTO ordto);
        int CreateCart(int userId);
        void AddProductToCart(int userId, int productId, int count = 1);
        void UpdateProductCountInOrder(List<UpdateOrderItemDTO> oicList);
        void RemoveProductFromOrder(int orderItemId);
    }
}
