using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetStore.BLL.DataTransferObjects
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PartialDescription { get; set; }
        public string FullDescription { get; set; }
        public int Price { get; set; }
        public int Rating { get; set; }
        public int ProductCount { get; set; }
        public int CategoryId { get; set; }
        public CategoryDTO Category { get; set; }

        public List<TagItemDTO> Tags { get; set; }

        public List<ProductImageDTO> ProductImages { get; set; }

        public List<ProductReviewDTO> ProductReviews { get; set; }
    }
}
