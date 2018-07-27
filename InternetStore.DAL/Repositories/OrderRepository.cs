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
    public class OrderRepository : IRepository<Order>
    {
        private dbContext db;

        public OrderRepository(dbContext context)
        {
            this.db = context;
        }

        public IEnumerable<Order> GetAll()
        {
            return db.Orders;
        }

        public Order Get(int id)
        {
            return db.Orders.Find(id);
        }

        public void Create(Order Order)
        {
            db.Orders.Add(Order);
        }

        public void Update(Order Order)
        {
            db.Entry(Order).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Order Order = db.Orders.Find(id);
            if (Order != null)
                db.Orders.Remove(Order);
        }

        public IEnumerable<Order> Find(Func<Order, bool> predicate)
        {
            return db.Orders.Where(predicate).ToList();
        }

        public Order FirstOrDefault(Func<Order, bool> predicate)
        {
            return db.Orders.FirstOrDefault(predicate);
        }

        public bool Any(Func<Order, bool> predicate)
        {
            return db.Orders.Any(predicate);
        }
    }
}
