using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetStore.BLL.DataTransferObjects
{
    public class UpdateOrderItemDTO
    {
        public int Id { get; set; }
        public int ItemCount { get; set; }
    }
}
