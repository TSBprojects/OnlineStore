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
    public class CategoryRepository : IRepository<Category>
    {
        private dbContext db;

        public CategoryRepository(dbContext context)
        {
            this.db = context;
        }

        public IEnumerable<Category> GetAll()
        {
            return db.Categories;
        }

        public Category Get(int id)
        {
            return db.Categories.Find(id);
        }

        public void Create(Category Category)
        {
            db.Categories.Add(Category);
        }

        public void Update(Category Category)
        {
            db.Entry(Category).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Category Category = db.Categories.Find(id);
            if (Category != null)
                db.Categories.Remove(Category);
        }

        public IEnumerable<Category> Find(Func<Category, bool> predicate)
        {
            return db.Categories.Where(predicate).ToList();
        }

        public Category FirstOrDefault(Func<Category, bool> predicate)
        {
            return db.Categories.FirstOrDefault(predicate);
        }

        public bool Any(Func<Category, bool> predicate)
        {
            return db.Categories.Any(predicate);
        }
    }
}
