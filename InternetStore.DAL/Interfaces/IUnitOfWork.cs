using Internet_store.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetStore.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<ProductVote> ProductVotes { get; }
        IRepository<Subscriber> Subscribers { get; }
        IRepository<ProductReview> ProductReviews { get; }
        IRepository<Product> Products { get; }
        IRepository<ProductImage> ProductImages { get; }
        IRepository<Category> Categories { get; }
        IRepository<Order> Orders { get; }
        IRepository<OrderItem> OrderItems { get; }
        IRepository<User> Users { get; }
        IRepository<Role> Roles { get; }
        IRepository<Tag> Tags { get; }
        IRepository<TagItem> TagItems { get; }
        void Save();
    }
}
