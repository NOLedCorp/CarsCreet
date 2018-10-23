using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarsCrete.Data.Models;

namespace CarsCrete.Data
{
    public class CarsDbContext : DbContext
    {
        #region Constructor
        public CarsDbContext(DbContextOptions options) : base(options)
        {

        }
        #endregion

        #region Methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<User>().HasMany(u => u.Reports);
            modelBuilder.Entity<User>().HasMany(u => u.Books);

            modelBuilder.Entity<Car>().ToTable("Cars");
            modelBuilder.Entity<Car>().Property(i => i.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Car>().HasMany(u => u.Reports);
            modelBuilder.Entity<Car>().HasMany(u => u.Books);

            modelBuilder.Entity<Book>().ToTable("Books");
            modelBuilder.Entity<Book>().Property(i => i.Id).ValueGeneratedOnAdd();
            

            modelBuilder.Entity<FeedBack>().ToTable("FeedBack");
            modelBuilder.Entity<FeedBack>().Property(i => i.Id).ValueGeneratedOnAdd();
           

        }
        #endregion
        #region Properties
        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<FeedBack> Reports { get; set; }
        #endregion Properties
    }
}
