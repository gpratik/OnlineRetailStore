using Assignment.OnlineRetailStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.OnlineRetailStore.Repository
{
    public class StoreDbContext : DbContext, IStoreDbContext
    {
        public IProductCategoryRepository ProductCategory { get; set; }

        public IProductRepository Products { get; set; }

        public IOrderRepository Orders { get; set; }
		//Change the connection string below
        public StoreDbContext()
           : base("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\<<urusername>>\\documents\\visual studio 2017\\Projects\\Assignment.OnlineRetailStore\\Assignment.OnlineRetailStore.Repository\\localdata\\StoreDB.mdf;Integrated Security=True")
        {
            this.Configuration.LazyLoadingEnabled = false;
            Products = new ProductRepository(this);
            Orders = new OrderRepository(this);
            ProductCategory = new ProductCategoryRepository(this);
        }

        //public virtual DbSet<Product> Products { get; set; }
        //public virtual DbSet<Order> Orders { get; set; }

        //public virtual DbSet<ProductCategory> ProductCategories { get; set; }



        public int Complete()
        {
            return this.SaveChanges();
            //throw new NotImplementedException();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //This is some simple config included
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Product>().HasKey(c => c.ProductId);

            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.Entity<Order>().HasKey(o => new { o.OrderId, o.ProductId });
            modelBuilder.Entity<Order>().Property(t => t.OrderId).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            modelBuilder.Entity<Order>().Property(t => t.ProductId).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);

            modelBuilder.Entity<ProductCategory>().ToTable("ProductCategory");
            modelBuilder.Entity<ProductCategory>().HasKey(c => c.CategoryId);
        }
    }
}
