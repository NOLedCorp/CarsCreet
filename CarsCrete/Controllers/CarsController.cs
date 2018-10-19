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
    
    [Route("api/[controller]")]
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
        [HttpGet("get-user")]
        public IActionResult GetUser()
        {
            
            var user = DbContext.Users.Where(x => x.Id == 1).Include(x => x.Reports).Include(x => x.Books).FirstOrDefault();
            
            return new JsonResult(
                user.Adapt<UserDTO>(),
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }
    }
}
