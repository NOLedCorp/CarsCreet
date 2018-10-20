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
            //if (!dbContext.Users.Any()) CreateUsers(dbContext);
            // Create default Quizzes (if there are none) together with their set of Q & A
            //if (!dbContext.Cars.Any()) CreateCars(dbContext);
            //if (!dbContext.Books.Any()) CreateBooks(dbContext);
            //if (!dbContext.Reports.Any()) CreateReports(dbContext);
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
                Password = "123",
                CreatedDate = createdDate,
                ModifiedDate = lastModifiedDate
            };
            dbContext.Users.Add(user_Admin);
            dbContext.SaveChanges();
        }

        private static void CreateCars(CarsDbContext dbContext)
        {
            // local variables
            DateTime createdDate = new DateTime(2016, 03, 01, 12, 30, 00);
            DateTime lastModifiedDate = DateTime.Now;
            // Create the "Admin" ApplicationUser account (if it doesn't exist already)
            var car = new Car()
            {
                
                Model = "VW Up",
                Photo = "admin@testmakerfree.com",
                Passengers = 5,
                Fuel = "Бензин",
                Transmission  = "Автомат",
                Price = 28,
                Consumption = "5 литров на 100км",
                Doors = 5,
                Description = "Автомобиль с АКПП, 1,2 литра. Кондционер, радио-CD, расход топлива 5литров/100 км. В машину свободно входят четверо взрослых пассажира, 1 большая и 1 маленькая дорожные сумки"
            };
            dbContext.Cars.Add(car);
            dbContext.SaveChanges();
        }

        private static void CreateBooks(CarsDbContext dbContext)
        {
            // local variables
            DateTime createdDate = new DateTime(2016, 03, 01, 12, 30, 00);
            DateTime lastModifiedDate = DateTime.Now;
            // Create the "Admin" ApplicationUser account (if it doesn't exist already)
            var book = new Book()
            {
                CarId=4,
                UserId=6,
                Price=360,
                Place="Iraklion",
                DateStart = createdDate,
                DateFinish = lastModifiedDate
            };
            dbContext.Books.Add(book);
            dbContext.SaveChanges();
        }

        private static void CreateReports(CarsDbContext dbContext)
        {
            // local variables
            DateTime createdDate = new DateTime(2016, 03, 01, 12, 30, 00);
            DateTime lastModifiedDate = DateTime.Now;
            // Create the "Admin" ApplicationUser account (if it doesn't exist already)
            var report = new FeedBack()
            {
               
                CarId=4,
                UserId=6,
                Mark = 5,
                Text = "Все очень понравилось!",
                CreatedDate = createdDate
            };
            dbContext.Reports.Add(report);
            dbContext.SaveChanges();
        }
        #endregion
    }

}
