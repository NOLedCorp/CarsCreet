using CarsCrete.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsCrete.Data
{
    public class DbSeeder
    {
        #region Public Methods
        public static void Seed(CarsDbContext dbContext)
        {
            // Create default Users (if there are none)
            if (!dbContext.Users.Any()) CreateUsers(dbContext);
            // Create default Quizzes (if there are none) together with their set of Q & A
            if (!dbContext.Cars.Any()) CreateCars(dbContext);
            if (!dbContext.Books.Any()) CreateBooks(dbContext);
            if (!dbContext.Reports.Any()) CreateReports(dbContext);
        }
        #endregion
        #region Seed Methods
        private static void CreateUsers(CarsDbContext dbContext)
        {
            // local variables
            DateTime createdDate = new DateTime(2016, 03, 01, 12, 30, 00);
            DateTime lastModifiedDate = DateTime.Now;
            // Create the "Admin" ApplicationUser account (if it doesn't exist already)
            var user_Admin = new User()
            {
                Name = "Admin",
                Email = "admin@testmakerfree.com",
                CreatedDate = createdDate,
                ModifiedDate = lastModifiedDate
            };
            dbContext.SaveChanges();
        }

        private static void CreateCars(CarsDbContext dbContext)
        {
            // local variables
            DateTime createdDate = new DateTime(2016, 03, 01, 12, 30, 00);
            DateTime lastModifiedDate = DateTime.Now;
            // Create the "Admin" ApplicationUser account (if it doesn't exist already)
            var user_Admin = new Car()
            {
                Model = "Admin",
                Photo = "admin@testmakerfree.com",
                Passengers = 5,
                Fuel = "Бензин",
                Transmission  = "Автомат",
                Description = "Круто"
            };
            dbContext.SaveChanges();
        }

        private static void CreateBooks(CarsDbContext dbContext)
        {
            // local variables
            DateTime createdDate = new DateTime(2016, 03, 01, 12, 30, 00);
            DateTime lastModifiedDate = DateTime.Now;
            // Create the "Admin" ApplicationUser account (if it doesn't exist already)
            var user_Admin = new User()
            {
                Name = "Admin",
                Email = "admin@testmakerfree.com",
                CreatedDate = createdDate,
                ModifiedDate = lastModifiedDate
            };
            dbContext.SaveChanges();
        }

        private static void CreateReports(CarsDbContext dbContext)
        {
            // local variables
            DateTime createdDate = new DateTime(2016, 03, 01, 12, 30, 00);
            DateTime lastModifiedDate = DateTime.Now;
            // Create the "Admin" ApplicationUser account (if it doesn't exist already)
            var user_Admin = new User()
            {
                Name = "Admin",
                Email = "admin@testmakerfree.com",
                CreatedDate = createdDate,
                ModifiedDate = lastModifiedDate
            };
            dbContext.SaveChanges();
        }
    }

}
