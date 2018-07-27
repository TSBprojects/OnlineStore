using Internet_store.DAL.Entities;
using Internet_store.DAL.Repositories;
using InternetStore.DAL.EntityFramework;
using InternetStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetStore.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private dbContext db;
        private ProductVoteRepository prodVoteRep;
        private SubscriberRepository SubscrRep;
        private ProductReviewRepository prodRevRep;
        private ProductRepository productRep;
        private ProductImageRepository prodImageRep;
        private CategoryRepository categoryRep;
        private OrderRepository orderRep;
        private OrderItemRepository orderItemRep;
        private UserRepository userRep;
        private RoleRepository roleRep;
        private TagRepository tagRep;
        private TagItemRepository tagItemRep;

        public UnitOfWork(string connectionString)
        {
            db = new dbContext(connectionString);
        }

        public IRepository<Product> Products
        {
            get
            {
                if (productRep == null)
                    productRep = new ProductRepository(db);
                return productRep;
            }
        }

        public IRepository<ProductVote> ProductVotes
        {
            get
            {
                if (prodVoteRep == null)
                    prodVoteRep = new ProductVoteRepository(db);
                return prodVoteRep;
            }
        }

        public IRepository<ProductReview> ProductReviews
        {
            get
            {
                if (prodRevRep == null)
                    prodRevRep = new ProductReviewRepository(db);
                return prodRevRep;
            }
        }

        public IRepository<ProductImage> ProductImages
        {
            get
            {
                if (prodImageRep == null)
                    prodImageRep = new ProductImageRepository(db);
                return prodImageRep;
            }
        }

        public IRepository<Subscriber> Subscribers
        {
            get
            {
                if (SubscrRep == null)
                    SubscrRep = new SubscriberRepository(db);
                return SubscrRep;
            }
        }

        public IRepository<Category> Categories
        {
            get
            {
                if (categoryRep == null)
                    categoryRep = new CategoryRepository(db);
                return categoryRep;
            }
        }

        public IRepository<Order> Orders
        {
            get
            {
                if (orderRep == null)
                    orderRep = new OrderRepository(db);
                return orderRep;
            }
        }

        public IRepository<OrderItem> OrderItems
        {
            get
            {
                if (orderItemRep == null)
                    orderItemRep = new OrderItemRepository(db);
                return orderItemRep;
            }
        }

        public IRepository<User> Users
        {
            get
            {
                if (userRep == null)
                    userRep = new UserRepository(db);
                return userRep;
            }
        }

        public IRepository<Role> Roles
        {
            get
            {
                if (roleRep == null)
                    roleRep = new RoleRepository(db);
                return roleRep;
            }
        }

        public IRepository<Tag> Tags
        {
            get
            {
                if (tagRep == null)
                    tagRep = new TagRepository(db);
                return tagRep;
            }
        }

        public IRepository<TagItem> TagItems
        {
            get
            {
                if (tagItemRep == null)
                    tagItemRep = new TagItemRepository(db);
                return tagItemRep;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
