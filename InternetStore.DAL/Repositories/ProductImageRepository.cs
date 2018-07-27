using Internet_store.DAL.Entities;
using InternetStore.DAL.EntityFramework;
using InternetStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internet_store.DAL.Repositories
{
    public class ProductImageRepository : IRepository<ProductImage>
    {
        private dbContext db;

        public ProductImageRepository(dbContext context)
        {
            this.db = context;
        }

        public IEnumerable<ProductImage> GetAll()
        {
            return db.ProductImages;
        }

        public ProductImage Get(int id)
        {
            return db.ProductImages.Find(id);
        }

        public void Create(ProductImage ProductImage)
        {
            db.ProductImages.Add(ProductImage);
        }

        public void Update(ProductImage ProductImage)
        {
            db.Entry(ProductImage).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            ProductImage ProductImage = db.ProductImages.Find(id);
            if (ProductImage != null)
                db.ProductImages.Remove(ProductImage);
        }

        public IEnumerable<ProductImage> Find(Func<ProductImage, bool> predicate)
        {
            return db.ProductImages.Where(predicate).ToList();
        }

        public ProductImage FirstOrDefault(Func<ProductImage, bool> predicate)
        {
            return db.ProductImages.FirstOrDefault(predicate);
        }

        public bool Any(Func<ProductImage, bool> predicate)
        {
            return db.ProductImages.Any(predicate);
        }
    }
}
