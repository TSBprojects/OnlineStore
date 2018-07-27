using InternetStore.BLL.DataTransferObjects;
using InternetStore.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetStore.BLL.Interfaces
{
    public interface IProductService
    {
        CategoryDTO GetCategory(string catName);
        CategoryDTO GetCategory(int id);
        ProductDTO GetProduct(int id);
        IEnumerable<ProductDTO> GetProducts();
        IEnumerable<ProductDTO> GetRandomProducts(int count);
        IEnumerable<ProductDTO> GetPartOfProducts(int part); 
        IEnumerable<ProductReviewDTO> GetProductReviews(int productId);
        IEnumerable<CategoryDTO> GetCategories();
        IEnumerable<TagDTO> GetTags();
        TagDTO GetTag(int tagId);
        TagDTO GetTag(string tagName);
        IEnumerable<ProductDTO> SearchProducts(string query);
        IEnumerable<ProductDTO> SiftProductsByCategory(int categoryId);
        IEnumerable<ProductDTO> SiftProductsByTag(int tagId);
        int GetProductPageCount();
        void AddReviewToProduct(int productId, string review, string name, string email);
        void AddVoteForProduct(int productId, int rating);
        int AddProduct(string name, string partialDes, string fullDes, int price, int rating, int productCount, int categoryId);
        int AddProduct(string name, string partialDes, string fullDes, int price, int rating, int productCount, string categoryName);
        void ChangeProduct(int productId, string name, string partialDes, string fullDes, int? price, int? rating, int? productCount, int? categoryId);
        void ChangeProduct(int productId, string name, string partialDes, string fullDes, int? price, int? rating, int? productCount, string categoryName);
        void AddImageToProduct(int productId, string imagePath);
        void AddImagesToProduct(int productId, string[] imagesPath, bool replace);
        void AddTagToProduct(int productId, int tagId);
        void AddTagToProduct(int productId, string tagName);
        void AddTagsToProduct(int productId, string[] tagsName);
        void RemoveTagFromProduct(int tagId, int productId);
        int AddCategory(string name, string description);
        void ChangeCategory(int categoryId, string name, string description);
        void ChangeTag(int tagId, string name);
        int AddTag(string name);
        void RemoveProduct(int productId);
        void RemoveCategory(int categoryId);
        void RemoveTag(int tagId);
    }
}
