using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetStore.BLL.DataTransferObjects
{
    public class TagItemDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public ProductDTO Product { get; set; }
        public int TagId { get; set; }
        public TagDTO Tag { get; set; }
    }
}
