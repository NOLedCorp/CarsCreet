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
using Microsoft.AspNetCore.Http;

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
        #region User
        public class UserEntrance
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
        [HttpGet("get-user-by-id/{id}")]
        public IActionResult GetUserById(long id)
        {
            
            var user = DbContext.Users.Where(x => x.Id==id).Include(x => x.Books).Include(x => x.Topics).ProjectToType<UserDTO>().FirstOrDefault();
            if (user == null)
            {
                return new StatusCodeResult(500);
            }
            if(id == 6)
            {
                user.Topics = DbContext.Topics.Where(x => x.UserReciverId == id).OrderByDescending(x => x.ModifyDate).ProjectToType<TopicDTO>().ToList();
            }
            else
            {
                user.Topics = user.Topics.OrderByDescending(x => x.ModifyDate).ToList();
            }
            
            user.Books=user.Books.OrderByDescending(x => x.DateFinish).ToList();
            foreach(TopicDTO t in user.Topics)
            {
                t.Messages = t.Messages.OrderByDescending(x => x.CreateDate).ToList();
                if(id == 6)
                {
                    t.User = DbContext.Users.Where(x => x.Id == t.UserId).ProjectToType<UserDTO>().FirstOrDefault();
                }
                else
                {
                    t.User = DbContext.Users.Where(x => x.Id == t.UserReciverId).ProjectToType<UserDTO>().FirstOrDefault();
                }
                
            }
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
        private UserDTO AddNewUser(UserReg model)
        {
            
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
            user.Id = DbContext.Users.Where(x => x.Email == model.Email).FirstOrDefault().Id;

            return user.Adapt<UserDTO>();
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
        [HttpPost("upload-user-photo")]
        public bool UploadPhoto()
        {
            
            
            var files = Request.Form.Files;
            var k = 0;
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
        #endregion User
        #region Booking
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
            if(model.SalesId!=0 && !CheckSale(model.DateStart, model.DateFinish, model.SalesId, model.CarId)){
                return new StatusCodeResult(300);
            }
            var k = (model.DateFinish - model.DateStart).Days;
            var book = new Book()
            {
                DateStart = model.DateStart,
                DateFinish = model.DateFinish,
                CreateDate = DateTime.Now,
                CarId = model.CarId,
                UserId = model.UserId,
                Place = model.Place,
                Price = model.Price,
                Sum = model.Price * (model.DateFinish - model.DateStart).Days,
                SalesId = model.SalesId
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
        public class BookTimes
        {
            public long CarId { get; set; }
            public DateTime DateStart { get; set; }
            public DateTime DateFinish { get; set; }
        }

        [HttpGet("get-book-times")]
        public BookTimes[] GetBookTimes(long Carid)
        {

            var books = DbContext.Books.Where(b => b.CarId == Carid).Where(b => b.DateStart > DateTime.Now).ToArray();

            return books.Adapt<BookTimes[]>();
        }
        #endregion Booking
        #region Car
        [HttpGet("get-car/{id}")]
        public IActionResult GetCar(long Id)
        {

            var car = DbContext.Cars.Where(x => x.Id == Id).Include(c => c.Reports).Include(c => c.Books).Include(x => x.Sales).ProjectToType<CarDTO>().FirstOrDefault();
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
            var cars = DbContext.Cars
                //.Include(x => x.Reports)
                //    .ThenInclude(c => c.Comments)
                //        .ThenInclude(u => u.User)
                //.Include(x => x.Reports)
                //    .ThenInclude(u => u.User)
                //.Include(x => x.Books)
                .Include(x => x.Sales).ProjectToType<CarDTO>().ToList();

            

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
        #endregion Car
        #region Report
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
        #endregion Report
        #region Messager
        [HttpPut("save-message")]
        public IActionResult SaveMessage([FromBody]MessageDTO model)
        {
            var message = model.Adapt<Message>();
            message.CreateDate = DateTime.Now;
            var topic = DbContext.Topics.Where(x => x.Id == model.TopicId).FirstOrDefault();
            topic.ModifyDate = message.CreateDate;
            topic.Seen = false;
            DbContext.Messages.Add(message);
            DbContext.SaveChanges();
            //model.User = DbContext.Users.Where(x => x.Id == model.UserReciverId).ProjectToType<UserDTO>().FirstOrDefault();

            return new JsonResult(
                model,
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }
        public class NewMessage
        {
            public string Email { get; set; }
            public string Name { get; set; }
            public string Text { get; set; }
        }
        [HttpPut("send-message")]
        public IActionResult SandMessage([FromBody]NewMessage model)
        {
            var user = DbContext.Users.Where(x => x.Email == model.Email).Include(x => x.Topics).ProjectToType<UserDTO>().FirstOrDefault();
            List<TopicDTO> result = new List<TopicDTO>();
            var date = DateTime.Now;
            if (user == null)
            {
                user = AddNewUser(new UserReg()
                {
                    Name = model.Name,
                    Email = model.Email,
                    Password = GeneratePassword()
                });
            }
            else{
                if (user.Topics.Count > 0)
                {
                    
                    var t1 = user.Topics.Where(x => (x.UserReciverId == 6 && x.UserId == user.Id)).FirstOrDefault();

                    var m1 = new MessageDTO()
                    {
                        UserId = user.Id,
                        TopicId = t1.Id,
                        Text = model.Text,
                        CreateDate = date
                    };
                    SaveMessage(m1);
                    result = DbContext.Topics.Where(x => x.UserId == user.Id).Include(x => x.Messages).ProjectToType<TopicDTO>().ToList();
                    foreach(TopicDTO tt in result)
                    {
                        tt.User = DbContext.Users.Where(x => x.Id == tt.UserReciverId).ProjectToType<UserDTO>().FirstOrDefault();
                        tt.Messages = tt.Messages.OrderByDescending(x => x.CreateDate).ToList();
                        if(tt.Id == t1.Id)
                        {
                            tt.ModifyDate = date;
                        }
                    }
                    return new JsonResult(
                        result,
                        new JsonSerializerSettings()
                        {
                            Formatting = Formatting.Indented
                        });
                }
            }
            var topic = CreateNewTopic(
                new TopicDTO()
                {
                    UserId = user.Id
                }, date);
            var m = new MessageDTO()
            {
                UserId = user.Id,
                TopicId = topic.Id,
                Text = model.Text,
                CreateDate = date
            };

            SaveMessage(m);
            var t = topic.Adapt<TopicDTO>();
            t.User = DbContext.Users.Where(x => x.Id == t.UserReciverId).ProjectToType<UserDTO>().FirstOrDefault();
            result.Add(t);
            return new JsonResult(
                result,
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }
        [HttpPut("create-topic")]
        public IActionResult CreateTopic([FromBody]TopicDTO model)
        {
            var topic = model.Adapt<Topic>();
            topic.ModifyDate = DateTime.Now;
            topic.UserReciverId = 6;
            DbContext.Topics.Add(topic);
            DbContext.SaveChanges();
            var result = DbContext.Topics.Where(x => (x.UserId == topic.UserId && x.UserReciverId == topic.UserReciverId)).ProjectToType<TopicDTO>().FirstOrDefault();
            result.User = DbContext.Users.Where(x => x.Id == result.UserReciverId).ProjectToType<UserDTO>().FirstOrDefault();

            return new JsonResult(
                result,
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }
        private Topic CreateNewTopic(TopicDTO model, DateTime date)
        {
            var topic = model.Adapt<Topic>();
            topic.ModifyDate = date;
            topic.UserReciverId = 6;
            DbContext.Topics.Add(topic);
            DbContext.SaveChanges();
            topic = DbContext.Topics.Where(x => x.Id == topic.Id).FirstOrDefault();
            

            return topic;


        }
        private string GeneratePassword()
        {
            string password = "";
            Random r = new Random();
            string[] alf = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
            for(int i = 0; i < 10; i++)
            {
                var k = r.Next(1, 3);
                if(k == 1)
                {
                    int l = r.Next(1, 3);
                    if (l == 1)
                    {
                        password += alf[r.Next(0, alf.Length)].ToUpper();
                    }
                    else
                    {
                        password += alf[r.Next(0, alf.Length)];
                    }
                }
                else
                {
                    password += r.Next(0, 10).ToString();
                }
            }


            return password;
        }
        [HttpDelete("change-seen/{id}")]
        public bool DeleteSeen(long id)
        {
            var topic = DbContext.Topics.Where(x => x.Id == id).FirstOrDefault();
            if (topic != null)
            {
                topic.Seen = true;
            };


            DbContext.SaveChanges();
            return true;
        }
        [HttpGet("get-topics/{id}")]
        public IActionResult GetTopics(long id)
        {
            List<TopicDTO> topics = new List<TopicDTO>();
            if(id == 6)
            {
                topics = DbContext.Topics.Where(x => x.UserReciverId == id).Include(x => x.Messages).ProjectToType<TopicDTO>().OrderByDescending(x => x.ModifyDate).ToList();
                foreach(var t in topics)
                {
                    t.User = DbContext.Users.Where(x => x.Id == t.UserId).ProjectToType<UserDTO>().FirstOrDefault();
                    t.Messages = t.Messages.OrderByDescending(x => x.CreateDate).ToList();
                }
            }
            else
            {
                topics = DbContext.Topics.Where(x => x.UserId == id).Include(x => x.Messages).ProjectToType<TopicDTO>().ToList();
                foreach (var t in topics)
                {
                    t.User = DbContext.Users.Where(x => x.Id == t.UserReciverId).ProjectToType<UserDTO>().FirstOrDefault();
                }
            }

            return new JsonResult(
                topics,
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });


        }
        #endregion Messager
        #region Sales
        [HttpPut("add-sale")]
        public IActionResult AddSale([FromBody] SaleDTO model)
        {
            var sale = model.Adapt<Sale>();
            DbContext.Sales.Add(sale);
            DbContext.SaveChanges();
            return new JsonResult(
                sale.Adapt<SaleDTO>(),
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }

        [HttpGet("get-sales")]
        public IActionResult GetSales()
        {
            var sales = DbContext.Sales.Where(x => x.DateFinish>DateTime.Now && x.DateStart<DateTime.Now).OrderBy(x => x.DateFinish).ProjectToType<SaleDTO>().ToList();
            return new JsonResult(
                sales,
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }

        private bool CheckSale(DateTime DateStart, DateTime DateFinish, long SaleId, long CarId )
        {
            DateTime date = DateTime.Now;
            var sale = DbContext.Sales.Where(x => x.Id == SaleId).FirstOrDefault();
            bool res = false;
            if (sale != null)
            {
                if(sale.CarId == CarId)
                {
                    if(sale.DateStart <= date && sale.DateFinish >= date)
                    {
                        if(sale.Type == 0)
                        {
                            res = true;
                        }
                        else
                        {
                            if((DateFinish - DateStart).Days >= sale.DaysNumber)
                            {
                                res = true;
                            }
                        }
                    }
                }
            }
            return res;
        }
        #endregion Sales
    }
}
