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
            modelBuilder.Entity<User>().HasMany(u => u.Comments);
            modelBuilder.Entity<User>().HasMany(u => u.Topics);

            modelBuilder.Entity<Car>().ToTable("Cars");
            modelBuilder.Entity<Car>().Property(i => i.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Book>().ToTable("Books");
            modelBuilder.Entity<Book>().HasOne(u => u.Car);
            modelBuilder.Entity<Book>().Property(i => i.Id).ValueGeneratedOnAdd();
            

            modelBuilder.Entity<FeedBack>().ToTable("FeedBack");
            modelBuilder.Entity<FeedBack>().Property(i => i.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<FeedBack>().HasOne(u => u.Car);
            modelBuilder.Entity<FeedBack>().HasMany(c => c.Comments);
            modelBuilder.Entity<FeedBack>().HasMany(c => c.Likes);

            modelBuilder.Entity<ReportComment>().ToTable("Comments");
            modelBuilder.Entity<ReportComment>().Property(i => i.Id).ValueGeneratedOnAdd();
            //modelBuilder.Entity<ReportComment>().HasMany(c => c.Likes);

            modelBuilder.Entity<Like>().ToTable("Likes");
            modelBuilder.Entity<Like>().Property(i => i.Id).ValueGeneratedOnAdd();
            //modelBuilder.Entity<Like>().HasOne(u => u.Comment).WithMany(u => u.Likes);
            // modelBuilder.Entity<Like>().HasOne(u => u.Report).WithMany(u => u.Likes);

            modelBuilder.Entity<Message>().ToTable("Messages");
            modelBuilder.Entity<Message>().Property(i => i.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Photo>().ToTable("Photos");
            modelBuilder.Entity<Photo>().Property(i => i.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Topic>().ToTable("Topics");
            modelBuilder.Entity<Topic>().HasMany(u => u.Messages);
            modelBuilder.Entity<Topic>().Property(i => i.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Sale>().ToTable("Sales");
            modelBuilder.Entity<Sale>().HasOne(u => u.Car);
            modelBuilder.Entity<Sale>().Property(i => i.Id).ValueGeneratedOnAdd();


        }
        #endregion
        #region Properties
        public DbSet<User> Users { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<FeedBack> Reports { get; set; }
        public DbSet<ReportComment> Comments { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Sale> Sales { get; set; }
        #endregion Properties
    }
}
