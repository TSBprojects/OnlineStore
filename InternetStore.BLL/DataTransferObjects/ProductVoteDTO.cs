using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetStore.BLL.DataTransferObjects
{
    public class ProductVoteDTO
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public int ProductId { get; set; }
        public virtual ProductDTO Product { get; set; }
    }
}
