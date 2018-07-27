using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internet_store.DAL.Entities
{
    public class ProductVote
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int Rating { get; set; }

        [Required]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
