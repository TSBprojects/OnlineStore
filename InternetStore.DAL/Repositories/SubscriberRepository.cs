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
    public class SubscriberRepository : IRepository<Subscriber>
    {
        private dbContext db;

        public SubscriberRepository(dbContext context)
        {
            this.db = context;
        }

        public IEnumerable<Subscriber> GetAll()
        {
            return db.Subscribers;
        }

        public Subscriber Get(int id)
        {
            return db.Subscribers.Find(id);
        }

        public void Create(Subscriber Subscriber)
        {
            db.Subscribers.Add(Subscriber);
        }

        public void Update(Subscriber Subscriber)
        {
            db.Entry(Subscriber).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Subscriber Subscriber = db.Subscribers.Find(id);
            if (Subscriber != null)
                db.Subscribers.Remove(Subscriber);
        }

        public IEnumerable<Subscriber> Find(Func<Subscriber, bool> predicate)
        {
            return db.Subscribers.Where(predicate).ToList();
        }

        public Subscriber FirstOrDefault(Func<Subscriber, bool> predicate)
        {
            return db.Subscribers.FirstOrDefault(predicate);
        }

        public bool Any(Func<Subscriber, bool> predicate)
        {
            return db.Subscribers.Any(predicate);
        }
    }
}
