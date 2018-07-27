using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internet_store.DAL.Entities
{
    public class Product
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string PartialDescription { get; set; }

        [Required]
        public string FullDescription { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public int Rating { get; set; }

        [Required]
        public int ProductCount { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public virtual List<TagItem> Tags { get; set; }

        public virtual List<ProductImage> ProductImages { get; set; }

        public virtual List<ProductReview> ProductReviews { get; set; }
    }
}
