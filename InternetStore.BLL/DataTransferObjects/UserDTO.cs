using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetStore.BLL.DataTransferObjects
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int HashPassword { get; set; }
        public string PhoneNumber { get; set; }
        public string ZipCode { get; set; }
        public string IP { get; set; }
        public int RoleId { get; set; }
        public RoleDTO Role { get; set; }
        public List<OrderDTO> Orders { get; set; }
    }
}
