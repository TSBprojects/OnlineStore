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
    public class ProductReviewRepository : IRepository<ProductReview>
    {
        private dbContext db;

        public ProductReviewRepository(dbContext context)
        {
            this.db = context;
        }

        public IEnumerable<ProductReview> GetAll()
        {
            return db.ProductReviews;
        }

        public ProductReview Get(int id)
        {
            return db.ProductReviews.Find(id);
        }

        public void Create(ProductReview ProductReview)
        {
            db.ProductReviews.Add(ProductReview);
        }

        public void Update(ProductReview ProductReview)
        {
            db.Entry(ProductReview).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            ProductReview ProductReview = db.ProductReviews.Find(id);
            if (ProductReview != null)
                db.ProductReviews.Remove(ProductReview);
        }

        public IEnumerable<ProductReview> Find(Func<ProductReview, bool> predicate)
        {
            return db.ProductReviews.Where(predicate).ToList();
        }

        public ProductReview FirstOrDefault(Func<ProductReview, bool> predicate)
        {
            return db.ProductReviews.FirstOrDefault(predicate);
        }

        public bool Any(Func<ProductReview, bool> predicate)
        {
            return db.ProductReviews.Any(predicate);
        }
    }
}
