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
    public class OrderItemRepository : IRepository<OrderItem>
    {
        private dbContext db;

        public OrderItemRepository(dbContext context)
        {
            this.db = context;
        }

        public IEnumerable<OrderItem> GetAll()
        {
            return db.OrderItems;
        }

        public OrderItem Get(int id)
        {
            return db.OrderItems.Find(id);
        }

        public void Create(OrderItem OrderItem)
        {
            db.OrderItems.Add(OrderItem);
        }

        public void Update(OrderItem OrderItem)
        {
            db.Entry(OrderItem).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            OrderItem OrderItem = db.OrderItems.Find(id);
            if (OrderItem != null)
                db.OrderItems.Remove(OrderItem);
        }

        public IEnumerable<OrderItem> Find(Func<OrderItem, bool> predicate)
        {
            return db.OrderItems.Where(predicate).ToList();
        }

        public OrderItem FirstOrDefault(Func<OrderItem, bool> predicate)
        {
            return db.OrderItems.FirstOrDefault(predicate);
        }

        public bool Any(Func<OrderItem, bool> predicate)
        {
            return db.OrderItems.Any(predicate);
        }
    }
}
