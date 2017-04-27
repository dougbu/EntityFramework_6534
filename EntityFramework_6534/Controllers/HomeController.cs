using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityFramework_6534.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework_6534.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext _dbContext;

        public HomeController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public async Task<List<Results>> Bug1()
        {
            var val = from c in _dbContext.Users
                      where c.EmailConfirmed
                      orderby c.Email
                      select new Results
                      {
                          Count = c.Claims.Count(x => x.Id <= 3),
                          Name = c.UserName,
                      };

            return await val.ToListAsync();
        }

        public class Results
        {
            public int Count { get; set; }

            public string Name { get; set; }
        }
    }
}
