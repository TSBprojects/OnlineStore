using AutoMapper;
using Internet_store.DAL.Entities;
using InternetStore.BLL.DataTransferObjects;
using InternetStore.BLL.Infrastructure;
using InternetStore.BLL.Interfaces;
using InternetStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InternetStore.BLL.Services
{
    public class ProductService : IProductService
    {
        IMapper Mapper;
        IUnitOfWork db;
        const int _PROD_COUNT_IN_PART = 6;
        public ProductService(IUnitOfWork uow)
        {
            db = uow;
            Mapper = AutoMapperBLLConfig.Mapper;
        }

        public ProductDTO GetProduct(int id)
        {
            return Mapper.Map<Product,ProductDTO>(db.Products.Get(id));
        }

        public IEnumerable<ProductDTO> GetProducts()
        {
            return Mapper.Map<IEnumerable<Product>, List<ProductDTO>>(db.Products.GetAll());
        }

        public IEnumerable<ProductDTO> GetRandomProducts(int count)
        {
            List<ProductDTO> prodList = new List<ProductDTO>();
            Random r = new Random(DateTime.Now.Millisecond);

            for (int i = 0; i < count; i++)
            {
                prodList.Add(Mapper.Map<Product, ProductDTO>(db.Products.Get(r.Next(0, db.Products.GetAll().Count()))));
            }
            return prodList;
        }

        public IEnumerable<ProductDTO> GetPartOfProducts(int part)
        {
            List<ProductDTO> resultList = new List<ProductDTO>();
            List<ProductDTO> prodList = Mapper.Map<IEnumerable<Product>, List<ProductDTO>>(db.Products.GetAll());
            int startIndex = (part - 1) * _PROD_COUNT_IN_PART;
            int endIndex = startIndex + _PROD_COUNT_IN_PART;
            if(endIndex > prodList.Count)
            {
                endIndex = prodList.Count;
            }
            for (int i = startIndex; i < endIndex; i++)
            {
                resultList.Add(prodList[i]);
            }
            return resultList;
        }

        public IEnumerable<ProductReviewDTO> GetProductReviews(int productId)
        {
            return Mapper.Map<IEnumerable<ProductReview>, IEnumerable<ProductReviewDTO>>(db.ProductReviews.Find(r => r.ProductId == productId));
        }

        public IEnumerable<CategoryDTO> GetCategories()
        {
            return Mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(db.Categories.GetAll());
        }

        public CategoryDTO GetCategory(int id)
        {
            return Mapper.Map<Category, CategoryDTO>(db.Categories.Get(id));
        }

        public CategoryDTO GetCategory(string catName)
        {
            return Mapper.Map<Category, CategoryDTO>(db.Categories.FirstOrDefault(c => c.Name == catName));
        }

        public IEnumerable<TagDTO> GetTags()
        {
            return Mapper.Map<IEnumerable<Tag>, IEnumerable<TagDTO>>(db.Tags.GetAll());
        }

        public TagDTO GetTag(int tagId)
        {
            return Mapper.Map<Tag, TagDTO>(db.Tags.Get(tagId));
        }

        public TagDTO GetTag(string tagName)
        {
            return Mapper.Map<Tag, TagDTO>(db.Tags.FirstOrDefault(c => c.Name == tagName));
        }

        public IEnumerable<ProductDTO> SearchProducts(string query)
        {
            List<ProductDTO> resultList = new List<ProductDTO>();
            List<ProductDTO> prodList = Mapper.Map<IEnumerable<Product>, List<ProductDTO>>(db.Products.GetAll());

            foreach(ProductDTO pr in prodList)
            {
                if (Regex.IsMatch(pr.Name.ToLower(), query.ToLower()) ||
                    Regex.IsMatch(pr.FullDescription.ToLower(), query.ToLower()) ||
                    Regex.IsMatch(pr.PartialDescription.ToLower(), query.ToLower()))
                {
                    resultList.Add(pr);
                }
            }
            return resultList;
        }

        public IEnumerable<ProductDTO> SiftProductsByCategory(int categoryId)
        {
            return Mapper.Map<IEnumerable<Product>, List<ProductDTO>>(db.Products.Find(p => p.CategoryId == categoryId));
        }

        public IEnumerable<ProductDTO> SiftProductsByTag(int tagId)
        {
            List<ProductDTO> resultList = new List<ProductDTO>();
            List<ProductDTO> prodList = Mapper.Map<IEnumerable<Product>, List<ProductDTO>>(db.Products.GetAll());

            foreach (ProductDTO pr in prodList)
            {
                foreach (TagItemDTO tag in pr.Tags)
                {
                    if (tag.TagId == tagId)
                    {
                        resultList.Add(pr);
                        break;
                    }
                }
            }
            return resultList;
        }

        public int GetProductPageCount()
        {
            return (int)Math.Ceiling((decimal)db.Products.GetAll().Count() / _PROD_COUNT_IN_PART);
        }

        public void AddReviewToProduct(int productId, string review, string name, string email)
        {
            db.ProductReviews.Create(new ProductReview()
            {
                Name = name,
                Email = email,
                ProductId = productId,
                Review = review
            });
            db.Save();
        }

        public void AddVoteForProduct(int productId, int rating)
        {
            int votersCount = 1;
            int ratingSum = rating;
            foreach (ProductVote prv in db.ProductVotes.Find(pv => pv.ProductId == productId))
            {
                ratingSum += prv.Rating;
                votersCount++;
            }
            db.Products.Get(productId).Rating = ratingSum * votersCount;
            db.ProductVotes.Create(new ProductVote() { ProductId = productId, Rating = rating });
            db.Save();
        }

        public int AddProduct(string name, string partialDes, string fullDes, int price, int rating, int productCount, int categoryId)
        {
            Product newProd = new Product()
            {
                Name = name,
                PartialDescription = partialDes,
                FullDescription = fullDes,
                Price = price,
                Rating = rating,
                ProductCount = productCount,
                CategoryId = categoryId,
            };
            db.Products.Create(newProd);
            db.Save();
            return newProd.Id;
        }

        public int AddProduct(string name, string partialDes, string fullDes, int price, int rating, int productCount, string categoryName)
        {
            Product newProd = new Product()
            {
                Name = name,
                PartialDescription = partialDes,
                FullDescription = fullDes,
                Price = price,
                Rating = rating,
                ProductCount = productCount,
                CategoryId = GetCategory(categoryName).Id,
            };
            db.Products.Create(newProd);
            db.Save();
            return newProd.Id;
        }

        public void ChangeProduct(int productId, string name, string partialDes, string fullDes, int? price, int? rating, int? productCount, int? categoryId)
        {
            Product prod = db.Products.Get(productId);
            if(name != null && !name.Equals(""))
            {
                prod.Name = name;
            }
            if(partialDes != null && !partialDes.Equals(""))
            {
                prod.PartialDescription = partialDes;
            }
            if (fullDes != null && !fullDes.Equals(""))
            {
                prod.FullDescription = fullDes;
            }
            if (price != null && price != 0)
            {
                prod.Price = (int)price;
            }
            if (rating != null)
            {
                prod.Rating = (int)rating;
            }
            if (productCount != null)
            {
                prod.ProductCount = (int)productCount;
            }
            if (categoryId != null && categoryId > 0 && categoryId <= GetCategories().Count())
            {
                prod.CategoryId = (int)categoryId;
            }
            db.Save();
        }

        public void ChangeProduct(int productId, string name, string partialDes, string fullDes, int? price, int? rating, int? productCount, string categoryName)
        {
            Product prod = db.Products.Get(productId);
            if (name != null && !name.Equals(""))
            {
                prod.Name = name;
            }
            if (partialDes != null && !partialDes.Equals(""))
            {
                prod.PartialDescription = partialDes;
            }
            if (fullDes != null && !fullDes.Equals(""))
            {
                prod.FullDescription = fullDes;
            }
            if (price != null && price != 0)
            {
                prod.Price = (int)price;
            }
            if (rating != null)
            {
                prod.Rating = (int)rating;
            }
            if (productCount != null)
            {
                prod.ProductCount = (int)productCount;
            }
            if (categoryName != null && !categoryName.Equals(""))
            {
                prod.CategoryId = GetCategory(categoryName).Id;
            }
            db.Save();
        }

        public void AddImageToProduct(int productId, string imagePath)
        {
            db.ProductImages.Create(new ProductImage()
            {
                ProductId = productId,
                Path = imagePath
            });
            db.Save();
        }

        public void AddImagesToProduct(int productId, string[] imagesPath, bool replace)
        {
            if (replace)
            {
                foreach (ProductImage img in db.ProductImages.Find(ti => ti.ProductId == productId))
                {
                    db.ProductImages.Delete(img.Id);
                }
            }
            for (int i = 0; i < imagesPath.Length; i++)
            {
                AddImageToProduct(productId, imagesPath[i]);
            }
        }

        public void AddTagToProduct(int productId, int tagId)
        {
            db.TagItems.Create(new TagItem()
            {
                TagId = tagId,
                ProductId = productId
            });
            db.Save();
        }

        public void AddTagToProduct(int productId, string tagName)
        {
            db.TagItems.Create(new TagItem()
            {
                TagId = GetTag(tagName).Id,
                ProductId = productId
            });
            db.Save();
        }

        public void AddTagsToProduct(int productId, string[] tagsName)
        {
            foreach(TagItem ti in db.TagItems.Find(ti => ti.ProductId == productId))
            {
                db.TagItems.Delete(ti.Id);
            }
            for (int i = 0; i < tagsName.Length; i++)
            {
                db.TagItems.Create(new TagItem()
                {
                    TagId = GetTag(tagsName[i]).Id,
                    ProductId = productId
                });
            }
            db.Save();
        }

        public void RemoveTagFromProduct(int tagId, int productId)
        {
            db.TagItems.Delete(db.TagItems.FirstOrDefault(ti => ti.TagId == tagId && ti.ProductId == productId).Id);
            db.Save();
        }

        public int AddCategory(string name, string description)
        {
            Category category = new Category()
            {
                Name = name,
                Description = description
            };
            db.Categories.Create(category);
            db.Save();
            return category.Id;
        }

        public void ChangeCategory(int categoryId, string name, string description)
        {
            Category category = db.Categories.Get(categoryId);
            if (name != null)
            {
                category.Name = name;
            }
            if (description != null)
            {
                category.Description = description;
            }
            db.Save();
        }

        public void ChangeTag(int tagId, string name)
        {
            db.Tags.Get(tagId).Name = name;
            db.Save();
        }

        public int AddTag(string name)
        {
            Tag tag = new Tag()
            {
                Name = name
            };
            db.Tags.Create(tag);
            db.Save();
            return tag.Id;
        }

        public void RemoveProduct(int productId)
        {
            Order order;
            IEnumerable<OrderItem> orderItems = db.OrderItems.Find(o => o.ProductId == productId);
            foreach (var ori in orderItems)
            {
                order = db.Orders.Get(ori.OrderId);
                order.OrderPrice -= ori.Product.Price * ori.ItemCount;
                order.ProductsCount -= ori.ItemCount;
            }
            db.Products.Delete(productId);
            db.Save();
        }

        public void RemoveCategory(int categoryId)
        {
            db.Categories.Delete(categoryId);
        }

        public void RemoveTag(int tagId)
        {
            db.Tags.Delete(tagId);
        }
    }
}
