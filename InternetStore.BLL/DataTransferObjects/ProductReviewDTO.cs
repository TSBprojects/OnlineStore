using Internet_store.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetStore.BLL.DataTransferObjects
{
    public class ProductReviewDTO
    { 
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string Review { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
