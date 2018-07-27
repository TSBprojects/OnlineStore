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
    public class ProductVoteRepository : IRepository<ProductVote>
    {
        private dbContext db;

        public ProductVoteRepository(dbContext context)
        {
            this.db = context;
        }

        public IEnumerable<ProductVote> GetAll()
        {
            return db.ProductVotes;
        }

        public ProductVote Get(int id)
        {
            return db.ProductVotes.Find(id);
        }

        public void Create(ProductVote ProductVote)
        {
            db.ProductVotes.Add(ProductVote);
        }

        public void Update(ProductVote ProductVote)
        {
            db.Entry(ProductVote).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            ProductVote ProductVote = db.ProductVotes.Find(id);
            if (ProductVote != null)
                db.ProductVotes.Remove(ProductVote);
        }

        public IEnumerable<ProductVote> Find(Func<ProductVote, bool> predicate)
        {
            return db.ProductVotes.Where(predicate).ToList();
        }

        public ProductVote FirstOrDefault(Func<ProductVote, bool> predicate)
        {
            return db.ProductVotes.FirstOrDefault(predicate);
        }

        public bool Any(Func<ProductVote, bool> predicate)
        {
            return db.ProductVotes.Any(predicate);
        }
    }
}
