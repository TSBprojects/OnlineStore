using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internet_store.DAL.Entities
{
    public class TagItem
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        [Required]
        public int TagId { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
