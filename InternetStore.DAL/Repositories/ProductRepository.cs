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
    public class ProductRepository : IRepository<Product>
    {
        private dbContext db;

        public ProductRepository(dbContext context)
        {
            this.db = context;
        }

        public IEnumerable<Product> GetAll()
        {
            return db.Products;
        }

        public Product Get(int id)
        {
            return db.Products.Find(id);
        }

        public void Create(Product Product)
        {
            db.Products.Add(Product);
        }

        public void Update(Product Product)
        {
            db.Entry(Product).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Product Product = db.Products.Find(id);
            if (Product != null)
                db.Products.Remove(Product);
        }

        public IEnumerable<Product> Find(Func<Product, bool> predicate)
        {
            return db.Products.Where(predicate).ToList();
        }

        public Product FirstOrDefault(Func<Product, bool> predicate)
        {
            return db.Products.FirstOrDefault(predicate);
        }

        public bool Any(Func<Product, bool> predicate)
        {
            return db.Products.Any(predicate);
        }
    }
}
