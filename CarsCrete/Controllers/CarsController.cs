using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarsCrete.Data;
using Microsoft.AspNetCore.Mvc;
using Mapster;
using CarsCrete.Data.Models;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarsCrete.Controllers
{
    
    [Route("[controller]")]
    public class CarsController : Controller
    {
        #region Private Fields
        private CarsDbContext DbContext;
        #endregion
        #region Constructor
        public CarsController(CarsDbContext context)
        {
            // Instantiate the ApplicationDbContext through DI
            DbContext = context;
        }
        #endregion Constructor
        // GET: /<controller>/
        [HttpGet("get-user/{email}")]
        public IActionResult GetUser(string email)
        {
            
            var user = DbContext.Users.Where(x => x.Email == email).Include(x => x.Reports).Include(x => x.Books).FirstOrDefault();
            
            return new JsonResult(
                user.Adapt<UserDTO>(),
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }
        [HttpPut("add-user")]
        public IActionResult AddUser([FromBody]UserDTO model)
        {

            var user = model.Adapt<User>();
            DbContext.Users.Add(user);
            DbContext.SaveChanges();

            return new JsonResult(
                user.Adapt<UserDTO>(),
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }
        
        public class UserPassword
        {
            public long Id { get; set; }
            public string Password { get; set; }
        }
        [HttpPost("change-password")]
        public IActionResult ChangePassword([FromBody]UserPassword model)
        {

            var user = DbContext.Users.Where(x => x.Id == model.Id).FirstOrDefault();
            user.Password = model.Password;
            user.ModifiedDate = DateTime.Now;
            DbContext.SaveChanges();

            return new JsonResult(
                user.Adapt<UserDTO>(),
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }

        [HttpPut("add-booking")]
        public IActionResult AddBooking([FromBody]BookDTO model)
        {

            var book = model.Adapt<Book>();
            DbContext.Books.Add(book);
            DbContext.SaveChanges();

            return new JsonResult(
                book.Adapt<BookDTO>(),
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }
        [HttpGet("get-book/{id}")]
        public IActionResult GetBook(long Id)
        {

            var book = DbContext.Books.Where(x => x.Id == Id).FirstOrDefault();

            return new JsonResult(
                book.Adapt<BookDTO>(),
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }
        [HttpGet("get-car/{id}")]
        public IActionResult GetCar(long Id)
        {

            var car = DbContext.Cars.Where(x => x.Id == Id).FirstOrDefault();

            return new JsonResult(
                car.Adapt<CarDTO>(),
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }
        [HttpPut("add-car")]
        public IActionResult AddCar([FromBody]CarDTO model)
        {

            var car = model.Adapt<Car>();
            DbContext.Cars.Add(car);
            DbContext.SaveChanges();

            return new JsonResult(
                car.Adapt<CarDTO>(),
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }

        [HttpGet("get-cars")]
        public IActionResult GetCars()
        {

            var car = DbContext.Cars.Include(x => x.Reports).ToArray();

            return new JsonResult(
                car.Adapt<CarDTO[]>(),
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }

        [HttpGet("get-reports")]
        public IActionResult GetReports()
        {

            var reports = DbContext.Reports.ToArray();

            return new JsonResult(
                reports.Adapt<FeedBackDTO[]>(),
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }
        [HttpPut("add-report")]
        public IActionResult AddReport([FromBody]FeedBackDTO model)
        {

            var report = model.Adapt<FeedBack>();
            DbContext.Reports.Add(report);
            DbContext.SaveChanges();

            return new JsonResult(
                report.Adapt<FeedBackDTO>(),
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }
    }
}
