using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityFramework_6534.Data;
using EntityFramework_6534.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
            var match = new ApplicationUser
            {
                Claims =
                {
                    new IdentityUserClaim<string>
                    {
                        ClaimType = "My",
                        ClaimValue = "Claim",
                    },
                    new IdentityUserClaim<string>
                    {
                        ClaimType = "Claim",
                        ClaimValue = "My",
                    },
                },
                Email = "joe@fred.com",
                EmailConfirmed = true,
                UserName = "Joe",
            };

            await _dbContext.AddAsync(match);
            await _dbContext.SaveChangesAsync();

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

        public async Task<Results> Bug2()
        {
            var user = await _dbContext.Users
                .Select(x => new Results
                {
                    Count = x.Claims.Count(y => y.ClaimType == "Claim"),
                    Name = x.Id,
                })
                .FirstOrDefaultAsync();

            return user;
        }

        public List<SimpleClass> Bug3()
        {
            return _dbContext.UserClaims
                .Select(claim => new SimpleClass
                {
                    Count = claim.ClaimType.Length,
                    Id = claim.Id.ToString(),
                    Name = claim.ClaimType,
                })
                .ToList();
        }

        public class Results
        {
            public int Count { get; set; }

            public string Name { get; set; }
        }

        public class SimpleClass
        {
            public int Count { get; set; }

            public string Id { get; set; }

            public string Name { get; set; }
        }
    }
}
