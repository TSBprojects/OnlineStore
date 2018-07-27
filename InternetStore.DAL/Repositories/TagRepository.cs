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
    public class TagRepository : IRepository<Tag>
    {
        private dbContext db;

        public TagRepository(dbContext context)
        {
            this.db = context;
        }

        public IEnumerable<Tag> GetAll()
        {
            return db.Tags;
        }

        public Tag Get(int id)
        {
            return db.Tags.Find(id);
        }

        public void Create(Tag Tag)
        {
            db.Tags.Add(Tag);
        }

        public void Update(Tag Tag)
        {
            db.Entry(Tag).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Tag Tag = db.Tags.Find(id);
            if (Tag != null)
                db.Tags.Remove(Tag);
        }

        public IEnumerable<Tag> Find(Func<Tag, bool> predicate)
        {
            return db.Tags.Where(predicate).ToList();
        }

        public Tag FirstOrDefault(Func<Tag, bool> predicate)
        {
            return db.Tags.FirstOrDefault(predicate);
        }

        public bool Any(Func<Tag, bool> predicate)
        {
            return db.Tags.Any(predicate);
        }
    }
}
