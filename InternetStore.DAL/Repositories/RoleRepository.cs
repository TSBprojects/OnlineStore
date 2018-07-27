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
    public class RoleRepository : IRepository<Role>
    {
        private dbContext db;

        public RoleRepository(dbContext context)
        {
            this.db = context;
        }

        public IEnumerable<Role> GetAll()
        {
            return db.Roles;
        }

        public Role Get(int id)
        {
            return db.Roles.Find(id);
        }

        public void Create(Role role)
        {
            db.Roles.Add(role);
        }

        public void Update(Role role)
        {
            db.Entry(role).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Role role = db.Roles.Find(id);
            if (role != null)
                db.Roles.Remove(role);
        }

        public IEnumerable<Role> Find(Func<Role, bool> predicate)
        {
            return db.Roles.Where(predicate).ToList();
        }

        public Role FirstOrDefault(Func<Role, bool> predicate)
        {
            return db.Roles.FirstOrDefault(predicate);
        }

        public bool Any(Func<Role, bool> predicate)
        {
            return db.Roles.Any(predicate);
        }
    }
}
