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
    public class TagItemRepository : IRepository<TagItem>
    {
        private dbContext db;

        public TagItemRepository(dbContext context)
        {
            this.db = context;
        }

        public IEnumerable<TagItem> GetAll()
        {
            return db.TagItems;
        }

        public TagItem Get(int id)
        {
            return db.TagItems.Find(id);
        }

        public void Create(TagItem TagItem)
        {
            db.TagItems.Add(TagItem);
        }

        public void Update(TagItem TagItem)
        {
            db.Entry(TagItem).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            TagItem TagItem = db.TagItems.Find(id);
            if (TagItem != null)
                db.TagItems.Remove(TagItem);
        }

        public IEnumerable<TagItem> Find(Func<TagItem, bool> predicate)
        {
            return db.TagItems.Where(predicate).ToList();
        }

        public TagItem FirstOrDefault(Func<TagItem, bool> predicate)
        {
            return db.TagItems.FirstOrDefault(predicate);
        }

        public bool Any(Func<TagItem, bool> predicate)
        {
            return db.TagItems.Any(predicate);
        }
    }
}
