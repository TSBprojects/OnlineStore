using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internet_store.DAL.Entities
{
    public class Order
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int OrderPrice { get; set; }

        [Required]
        public int ProductsCount { get; set; }

        public string DeliveryMethod { get; set; }

        public string PaymentMethod { get; set; }

        [Required]
        public string Status { get; set; }

        public string Comment { get; set; }  

        [Required]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public virtual List<OrderItem> OrderItems { get; set; }
    }
}
