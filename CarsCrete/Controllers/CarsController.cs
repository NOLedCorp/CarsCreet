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
        public class UserEntrance
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
        [HttpGet("get-user-by-id/{id}")]
        public IActionResult GetUserById(long id)
        {

            var user = DbContext.Users.Where(x => x.Id==id).Include(x => x.Books).ProjectToType<UserDTO>().FirstOrDefault();
            if (user == null)
            {
                return new StatusCodeResult(500);
            }
            user.Books=user.Books.OrderByDescending(x => x.DateFinish).ToList();
            return new JsonResult(
                user,
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }
        public class Statistics
        {
            public List<UserDTO> Users { get; set; }
            public List<BookDTO> Books { get; set; }
            public List<FeedBackDTO> Reports { get; set; }
            public List<CarDTO> Cars { get; set; }
        }
        [HttpGet("get-statistics")]
        public IActionResult GetStatistics()
        {
            Statistics result = new Statistics();
            result.Books = DbContext.Books.ProjectToType<BookDTO>().ToList();
            result.Cars = DbContext.Cars.Include(x => x.Books).Include(x => x.Reports).ProjectToType<CarDTO>().ToList();
            result.Reports = DbContext.Reports.Include(x => x.Comments).ProjectToType<FeedBackDTO>().ToList();
            result.Users = DbContext.Books.ProjectToType<UserDTO>().ToList();

            return new JsonResult(
                result,
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }
        [HttpGet("get-user")]
        public IActionResult GetUser(UserEntrance user1)
        {

            var user = DbContext.Users.Where(x => (x.Email == user1.Email && x.Password==user1.Password)).Include(x => x.Reports).Include(x => x.Books).ProjectToType<UserDTO>().FirstOrDefault();
            if (user == null)
            {
                return new StatusCodeResult(500);
            }
            user.Books = user.Books.OrderByDescending(x => x.DateFinish).ToList();
            return new JsonResult(
                user,
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }
        public class UserReg
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string Phone { get; set; }
            public string Lang { get; set; }
        }

        [HttpPut("add-user")]
        public IActionResult AddUser([FromBody]UserReg model)
        {
            if (DbContext.Users.Where(x => x.Email == model.Email).ToArray().Length > 0)
            {
                return new StatusCodeResult(500);
            }
            var user = new User()
            {
                Name = model.Name,
                Email = model.Email,
                Phone = model.Phone,
                Password = model.Password,
                Lang = model.Lang,
                Photo = "../../assets/images/default_user_photo.jpg",
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
            DbContext.Users.Add(user);
            user.Books = new List<Book>();
            user.Reports = new List<FeedBack>();
            DbContext.SaveChanges();

            return new JsonResult(
                user.Adapt<UserDTO>(),
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }

        public class InfoToChange
        {
            public string Type { get; set; }
            public string Value { get; set; }
            public long UserId { get; set; }
        }
        [HttpPost("change-info")]
        public bool ChangeInfo([FromBody]InfoToChange model)
        {
            var user = DbContext.Users.Where(u => u.Id == model.UserId).FirstOrDefault();
            if(user != null)
            {
                switch (model.Type)
                {
                    case "Phone":
                        {
                            user.Phone = model.Value;
                            break;
                        }
                    case "Email":
                        {
                            var users = DbContext.Users.ToList();
                            foreach( User u in users)
                            {
                                if (u.Email == model.Value)
                                {
                                    return false;
                                }
                            }
                            user.Email = model.Value;
                            break;
                        }
                    case "Lang":
                        {
                            user.Lang = model.Value;
                            break;
                        }
                }
                DbContext.SaveChanges();
            }
            

            return true;
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
        public bool CheckDate(long CarId, DateTime DateStart, DateTime DateFinish)
        {
            bool res = true;
            var bs = GetBookTimes(CarId);
            foreach (BookTimes bookTime in GetBookTimes(CarId))
            {
                if (DateStart >= bookTime.DateStart && DateStart <= bookTime.DateFinish || DateFinish >= bookTime.DateStart && DateFinish <= bookTime.DateFinish)
                {
                    return false;
                }
            }
            return res;
        }
        [HttpPut("add-booking")]
        public IActionResult AddBooking([FromBody]BookDTO model)
        {
            
            if (model.DateStart > model.DateFinish)
            {
                return new StatusCodeResult(500);
            }
            if (model.DateStart < DateTime.Now)
            {
                return new StatusCodeResult(503);
            }
            if (!CheckDate(model.CarId, model.DateStart, model.DateFinish))
            {
                return new StatusCodeResult(501);
            }
            var book = new Book()
            {
                DateStart = model.DateStart,
                DateFinish = model.DateFinish,
                CreateDate = DateTime.Now,
                CarId = model.CarId,
                UserId = model.UserId,
                Place = model.Place,
                Price = model.Price
            };
            DbContext.Books.Add(book);
            DbContext.SaveChanges();

            return new JsonResult(
                book.Adapt<BookDTO>(),
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }

        public class BookNew
        {
            public long Id { get; set; }
            public DateTime DateStart { get; set; }
            public DateTime DateFinish { get; set; }
            public long CarId { get; set; }
            public long UserId { get; set; }
            public double Price { get; set; }
            public string Place { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string Phone { get; set; }
            public string Comment { get; set; }
        }
        [HttpPut("add-booking-new")]
        public IActionResult AddBookingNew([FromBody]BookNew model)
        {
            if(model.DateStart < DateTime.Now)
            {
                return new StatusCodeResult(503);
            }
            if (!CheckDate(model.CarId, model.DateStart, model.DateFinish))
            {
                return new StatusCodeResult(501);
            }
            if (model.DateStart > model.DateFinish)
            {
                return new StatusCodeResult(500);
            }
            var user = DbContext.Users.Where(u => u.Email == model.Email).FirstOrDefault();
            if (user == null)
            {
                AddUser(model.Adapt<UserReg>());
                model.UserId = DbContext.Users.Where(u => u.Email == model.Email).FirstOrDefault().Id;
            }
            else
            {
                if (user.Password == model.Password)
                {
                    model.UserId = user.Id;
                }
                else
                {
                    return new StatusCodeResult(502);
                }
            }
            var book = new Book()
            {
                DateStart = model.DateStart,
                DateFinish = model.DateFinish,
                CreateDate = DateTime.Now,
                CarId = model.CarId,
                UserId = model.UserId,
                Place = model.Place,
                Price = model.Price
            };
            try
            {
                DbContext.Books.Add(book);
                DbContext.SaveChanges();
                return new StatusCodeResult(200);
            }
            catch
            {
                return new StatusCodeResult(505);
            }

            

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

            var car = DbContext.Cars.Where(x => x.Id == Id).Include(c => c.Reports).Include(c => c.Books).ProjectToType<CarDTO>().FirstOrDefault();
            foreach (FeedBackDTO f in car.Reports)
            {

                f.Likes.RemoveAll(x => x.CommentId != 0);
                f.Comments.ForEach(x => x.Likes = DbContext.Likes.Where(y => y.CommentId == x.Id).ToList().Adapt<List<LikeDTO>>());
            }
            car.Reports = car.Reports.OrderByDescending(r => r.CreatedDate).ToList();
            return new JsonResult(
                car,
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }
        [HttpPut("add-car")]
        public IActionResult AddCar([FromBody] CarDTO model)
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
            //var carr = new Car()
            //{
            //    Model = "VW Golf 7",
            //    Photo = "../../assets/images/VW_golf_7.jpg",
            //    Passengers = 5,
            //    Doors = 5,
            //    Transmission = "Automatic",
            //    Fuel = "Diesel",
            //    Consumption = 7,
            //    Description = "Автомобиль с АКПП, 1,4 литра, 120 лошадиных сил. Климат-контроль, радио-CD, расход топлива 6 литров/100 км. В машину свободно входят пять взрослых пассажиров, 2 большие и 2 маленькие дорожные сумки.",
            //    Price = 65,
            //    Description_ENG = "Eng description of the car."


            //};
            //DbContext.Cars.Add(carr);
            //DbContext.SaveChanges();
            //sssss
            var cars = DbContext.Cars
                .Include(x => x.Reports)
                    .ThenInclude(c => c.Comments)
                        .ThenInclude(u => u.User)
                .Include(x => x.Reports)
                    .ThenInclude(u => u.User)

                .Include(x => x.Books).ProjectToType<CarDTO>().ToList();

            foreach (CarDTO car in cars)
            {
                car.Reports = car.Reports.OrderByDescending(r => r.CreatedDate).ToList();
            }

            return new JsonResult(
                cars,
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }

        public class ReportCar
        {
            public long Id { get; set; }
            public string Photo { get; set; }
            public string Model { get; set; }
        }

        [HttpGet("get-report-cars")]
        public IActionResult GetReportCars()
        {
            //var carr = new Car()
            //{
            //    Model = "VW Golf 7",
            //    Photo = "../../assets/images/VW_golf_7.jpg",
            //    Passengers = 5,
            //    Doors = 5,
            //    Transmission = "Automatic",
            //    Fuel = "Diesel",
            //    Consumption = 7,
            //    Description = "Автомобиль с АКПП, 1,4 литра, 120 лошадиных сил. Климат-контроль, радио-CD, расход топлива 6 литров/100 км. В машину свободно входят пять взрослых пассажиров, 2 большие и 2 маленькие дорожные сумки.",
            //    Price = 65,
            //    Description_ENG = "Eng description of the car."


            //};
            //DbContext.Cars.Add(carr);
            //DbContext.SaveChanges();
            var cars = DbContext.Cars
                .ProjectToType<ReportCar>().ToList();

            return new JsonResult(
                cars,
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }


        public class BookTimes
        {
            public long CarId { get; set; } 
            public DateTime DateStart { get; set; }
            public DateTime DateFinish { get; set; }
        }

        [HttpGet("get-book-times")]
        public BookTimes[] GetBookTimes(long Carid)
        {

            var books = DbContext.Books.Where(b => b.CarId==Carid).Where(b => b.DateStart>DateTime.Now).ToArray();

            return books.Adapt<BookTimes[]>();
        }

        [HttpGet("get-reports")]
        public IActionResult GetReports()
        {
            var reports = DbContext.Reports.ProjectToType<FeedBackDTO>().OrderByDescending(r1 => r1.CreatedDate).ToList();
            foreach(FeedBackDTO f in reports)
            {
                
                f.Likes.RemoveAll(x => x.CommentId != 0);
                f.Comments.ForEach(x => x.Likes = DbContext.Likes.Where(y => y.CommentId == x.Id).ToList().Adapt<List<LikeDTO>>());
            }
            return new JsonResult(
                reports,
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }
        [HttpPut("add-report")]
        public IActionResult AddReport([FromBody]FeedBackDTO model)
        {

            var report = model.Adapt<FeedBack>();
            report.CreatedDate = DateTime.Now;
            report.Mark = Math.Round(report.Mark, 2);
            DbContext.Reports.Add(report);
            DbContext.SaveChanges();

            return new JsonResult(
                report.Adapt<FeedBackDTO>(),
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }
        public class NewComment
        {
            public long UserId { get; set; }
            public long FeedBackId { get; set; }
            public string Text { get; set; }
        }
        [HttpPut("add-comment")]
        public IActionResult AddComment([FromBody]NewComment model)
        {

            var report = model.Adapt<ReportComment>();
            report.CreatedDate = DateTime.Now;
            DbContext.Comments.Add(report);
            DbContext.SaveChanges();
            var result = report.Adapt<ReportCommentDTO>();
            result.Likes = new List<LikeDTO>();
            result.User = DbContext.Users.Where(u => u.Id == model.UserId).ProjectToType<ReportUser>().FirstOrDefault();

            return new JsonResult(
                result,
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }
        public class LikeIn
        {
            public long CommentId { get; set; }
            public bool IsLike { get; set; }
            public long FeedBackId { get; set; }
            public long UserId { get; set; }
        }
        public class LikeChange
        {
            public long LikeId { get; set; }
            public bool IsLike { get; set; }
        }
        [HttpPost("change-like")]
        public bool ChangeLike([FromBody]LikeChange model)
        {
            var like = DbContext.Likes.Where(x => x.Id == model.LikeId).FirstOrDefault();
            if (like != null)
            {
                like.IsLike = model.IsLike;
            }
            DbContext.SaveChanges();
            return true;
        }
        [HttpDelete("delete-like/{id}")]
        public bool DeleteLike(long id)
        {
            var like = DbContext.Likes.Where(x => x.Id == id).FirstOrDefault();
            if (like!=null)
            {
                DbContext.Likes.Remove(like);
            }
           
            
            DbContext.SaveChanges();
            return true;
        }
        [HttpPost("add-likes")]
        public IActionResult AddLikes([FromBody]LikeIn model)
        {
            var like = new Like
            {
                UserId = model.UserId,
                FeedBackId = model.FeedBackId,
                CommentId =model.CommentId,
                IsLike = model.IsLike
            
            };

            DbContext.Likes.Add(like);
            DbContext.SaveChanges();
            var like1 = DbContext.Likes.Where(x => x.UserId == model.UserId && x.FeedBackId == model.FeedBackId && x.CommentId == model.CommentId).ProjectToType<LikeDTO>().FirstOrDefault();
            
            return new JsonResult(
                like1,
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }
    }
}
