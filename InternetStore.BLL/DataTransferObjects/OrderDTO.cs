using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetStore.BLL.DataTransferObjects
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public int OrderPrice { get; set; }
        public int ProductsCount { get; set; }
        public string DeliveryMethod { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; }
        public string Comment { get; set; }
        public int UserId { get; set; }
        public UserDTO User { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }
    }
}
