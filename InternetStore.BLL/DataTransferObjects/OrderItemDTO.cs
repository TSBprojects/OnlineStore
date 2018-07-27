using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetStore.BLL.DataTransferObjects
{
    public class OrderItemDTO
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public OrderDTO Order { get; set; }
        public int ProductId { get; set; }
        public ProductDTO Product { get; set; }
        public int ItemCount { get; set; }
    }
}
