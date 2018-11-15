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
            modelBuilder.Entity<User>().HasMany(u => u.Reports).WithOne(i => i.User);
            modelBuilder.Entity<User>().HasMany(u => u.Books).WithOne(i => i.User);
            modelBuilder.Entity<User>().HasMany(u => u.Comments).WithOne(i => i.User);
            modelBuilder.Entity<User>().HasMany(u => u.Messages);

            modelBuilder.Entity<Car>().ToTable("Cars");
            modelBuilder.Entity<Car>().Property(i => i.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Car>().HasMany(u => u.Reports).WithOne(i => i.Car);
            modelBuilder.Entity<Car>().HasMany(u => u.Books).WithOne(i => i.Car);

            modelBuilder.Entity<Book>().ToTable("Books");
            modelBuilder.Entity<Book>().HasOne(u => u.Car).WithMany(u => u.Books);
            modelBuilder.Entity<Book>().HasOne(u => u.User).WithMany(u => u.Books);
            modelBuilder.Entity<Book>().Property(i => i.Id).ValueGeneratedOnAdd();
            

            modelBuilder.Entity<FeedBack>().ToTable("FeedBack");
            modelBuilder.Entity<FeedBack>().Property(i => i.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<FeedBack>().HasOne(u => u.Car).WithMany(u => u.Reports);
            modelBuilder.Entity<FeedBack>().HasOne(u => u.User).WithMany(u => u.Reports);
            modelBuilder.Entity<FeedBack>().HasMany(c => c.Comments);
            modelBuilder.Entity<FeedBack>().HasMany(c => c.Likes);

            modelBuilder.Entity<ReportComment>().ToTable("Comments");
            modelBuilder.Entity<ReportComment>().Property(i => i.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<ReportComment>().HasOne(u => u.User).WithMany(u => u.Comments);
            //modelBuilder.Entity<ReportComment>().HasMany(c => c.Likes);

            modelBuilder.Entity<Like>().ToTable("Likes");
            modelBuilder.Entity<Like>().Property(i => i.Id).ValueGeneratedOnAdd();
            //modelBuilder.Entity<Like>().HasOne(u => u.Comment).WithMany(u => u.Likes);
            // modelBuilder.Entity<Like>().HasOne(u => u.Report).WithMany(u => u.Likes);

            modelBuilder.Entity<Message>().ToTable("Messages");
            modelBuilder.Entity<Message>().Property(i => i.Id).ValueGeneratedOnAdd();


        }
        #endregion
        #region Properties
        public DbSet<User> Users { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<FeedBack> Reports { get; set; }
        public DbSet<ReportComment> Comments { get; set; }
        public DbSet<Message> Messages { get; set; }
        #endregion Properties
    }
}
