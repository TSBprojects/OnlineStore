using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetStore.BLL.DataTransferObjects
{
    public class ProductImageDTO
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public int ProductId { get; set; }
        public ProductDTO Product { get; set; }
    }
}
