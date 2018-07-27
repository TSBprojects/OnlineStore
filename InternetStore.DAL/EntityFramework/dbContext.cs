using Internet_store.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetStore.DAL.EntityFramework
{
    public class dbContext : DbContext
    {
        static dbContext()
        {
            Database.SetInitializer(new MyDbInitializer());
        }
        public dbContext(string connectionString) : base(connectionString) { }

        public DbSet<ProductVote> ProductVotes { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<ProductReview> ProductReviews { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagItem> TagItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder
            //    .Entity<Dialog>()
            //    .HasOptional(u => u.ProfileImage)
            //    .WithOptionalPrincipal();

            modelBuilder.Entity<User>()
                  .HasMany(c => c.Orders)
                  .WithRequired(o => o.User)
                  .WillCascadeOnDelete(true);

            modelBuilder.Entity<Order>()
                  .HasMany(c => c.OrderItems)
                  .WithRequired(o => o.Order)
                  .WillCascadeOnDelete(true);
        }
    }

    public class MyDbInitializer : DropCreateDatabaseAlways<dbContext>
    {
        protected override void Seed(dbContext db)
        {
            db.Roles.Add(new Role() { Id = 1, Name = "Admin"});
            db.Roles.Add(new Role() { Id = 2, Name = "User" });
            db.Roles.Add(new Role() { Id = 3, Name = "Guest" });
            db.Users.Add(new User()
            {
                FirstName = "Антон",
                LastName = "В.", 
                HashPassword = "123456".GetHashCode(),
                PhoneNumber = "+79030230023",
                Email = "contra36@mail.ru",
                ZipCode = "410017",
                Address = "г.Саратов, ул.Шелковичная, д.151, кв.172",
                IP = "82.114.237.211",            
                RoleId = 1,           
            });
            db.SaveChanges();

            db.Orders.Add(new Order()
            {
                UserId = 1,
                Status = "Not Confirmed",
                ProductsCount = 0,
                OrderPrice = 0
            });

            db.SaveChanges();

            db.Categories.Add(new Category() { Name = "Awesome", Description = "Потрясающие" });
            db.Categories.Add(new Category() { Name = "Cactus", Description = "Кактусы" });
            db.Categories.Add(new Category() { Name = "FEATURED", Description = "Лучшее" });
            db.Categories.Add(new Category() { Name = "INDOOR", Description = "В помещении" });
            db.Categories.Add(new Category() { Name = "NEW", Description = "Новые" });
            db.Categories.Add(new Category() { Name = "OUTDOOR", Description = "На открытом воздухе" });
            db.Categories.Add(new Category() { Name = "POST", Description = "?" });
            db.Categories.Add(new Category() { Name = "SEEDS", Description = "Семена" });

            db.Tags.Add(new Tag() { Name = "plant"});
            db.Tags.Add(new Tag() { Name = "tree" });
            db.Tags.Add(new Tag() { Name = "nice" });
            db.Tags.Add(new Tag() { Name = "spines" });

            db.SaveChanges();

            FillDB(db);
        }

        private void FillDB(dbContext db)
        {
            int Tag = 1;
            int prodImage = 0;
            int Rating = 0;
            int CategoryId = 1;
            for (int i = 1; i <=25; i++)
            {
                db.Products.Add(new Product()
                {
                    Name = "The Tall Terrarium Plant (item"+i+")",
                    Price = 40+i,
                    CategoryId = CategoryId,
                    ProductCount = 10,
                    Rating = Rating,
                    PartialDescription = "Sed ut perspiciatis unde omnis iste natus error sit " +
                    "voluptatem accusantium doloremque laudantium, totam rem aperiam, " +
                    "eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo.",
                    FullDescription = "Sed ut perspiciatis unde omnis iste natus error sit" +
                    "voluptatem accusantium doloremque laudantium, totam rem aperiam," +
                    "eaque ipsa quae ab illo inventore veritatis et quasi architecto " +
                    "beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia " +
                    "voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur " +
                    "magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro " +
                    "quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci " +
                    "velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore " +
                    "magnam aliquam quaerat voluptatem. Ut enim ad minima veniam, quis " +
                    "nostrum exercitationem ullam corporis suscipit laboriosam, nisi ut " +
                    "aliquid ex ea commodi consequatur? Quis autem vel eum iure reprehenderit " +
                    "qui in ea voluptate velit esse quam nihil molestiae consequatur, vel illum " +
                    "qui dolorem eum fugiat quo voluptas nulla pariatur."
                });
                CategoryId ++;
                Rating ++;
                if (CategoryId > 8) CategoryId=1;
                if (Rating > 5) Rating = 0;
                db.SaveChanges();
                db.ProductImages.Add(new ProductImage() { Path = string.Format("/Content/dbImages/{0}.jpg", prodImage), ProductId = i }); 
                db.ProductImages.Add(new ProductImage() { Path = string.Format("/Content/dbImages/{0}.jpg", 2), ProductId = i });
                db.ProductImages.Add(new ProductImage() { Path = string.Format("/Content/dbImages/{0}.jpg", 3), ProductId = i });
                db.TagItems.Add(new TagItem() { ProductId = i, TagId = Tag });
                db.TagItems.Add(new TagItem() { ProductId = i, TagId = 4 });
                prodImage++;
                if (prodImage > 17) prodImage = 1;
                Tag++;
                if (Tag > 3) Tag = 1;
            }
            db.SaveChanges();
        }
    }
}