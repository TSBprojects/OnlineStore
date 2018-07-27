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
    public class UserRepository : IRepository<User>
    {
        private dbContext db;

        public UserRepository(dbContext context)
        {
            this.db = context;
        }

        public IEnumerable<User> GetAll()
        {
            return db.Users;
        }

        public User Get(int id)
        {
            return db.Users.Find(id);
        }

        public void Create(User User)
        {
            db.Users.Add(User);
        }

        public void Update(User User)
        {
            db.Entry(User).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            User User = db.Users.Find(id);
            if (User != null)
                db.Users.Remove(User);
        }

        public IEnumerable<User> Find(Func<User, bool> predicate)
        {
            return db.Users.Where(predicate).ToList();
        }

        public User FirstOrDefault(Func<User, bool> predicate)
        {
            return db.Users.FirstOrDefault(predicate);
        }

        public bool Any(Func<User, bool> predicate)
        {
            return db.Users.Any(predicate);
        }
    }
}
