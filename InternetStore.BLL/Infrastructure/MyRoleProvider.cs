using InternetStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;

namespace InternetStore.BLL.Infrastructure
{
    public class MyRoleProvider : RoleProvider
    {
        IUnitOfWork db;
        public MyRoleProvider()
        {
            db = DependencyResolver.Current.GetService<IUnitOfWork>();
        }

        public override bool IsUserInRole(string userlogin, string roleName)
        {
            if (db.Users.Any(u => u.Email == userlogin))
            {
                return db.Roles.Get(db.Users.FirstOrDefault(u => u.Email == userlogin).RoleId).Name == roleName;
            }
            return false;
        }

        public override string[] GetRolesForUser(string userlogin)
        {
            if (db.Users.Any(u => u.Email == userlogin))
            {
                return new string[] { db.Roles.Get(db.Users.FirstOrDefault(u => u.Email == userlogin).RoleId).Name };
            }
            return new string[0];
        }

        #region Not implemented

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }


        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }


        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        #endregion Not implemented
    }
}
