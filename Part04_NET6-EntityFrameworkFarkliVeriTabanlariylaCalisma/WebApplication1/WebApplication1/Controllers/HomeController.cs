using MFramework.Services.FakeData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApplication1.Entities;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly DatabaseContext _databaseContext;

        public HomeController(ILogger<HomeController> logger/*, DatabaseContext databaseContext*/)
        {
            _logger = logger;
            //_databaseContext = databaseContext;
        }

        public IActionResult Index([FromServices] DatabaseContext databaseContext)
        {
            return View(databaseContext.Albums.ToList());
        }

        public string ImportData([FromServices] DatabaseContext databaseContext)
        {
            if (databaseContext.Albums.Any()) return "ok";

            for (int i = 0; i < 10; i++)
            {
                databaseContext.Albums.Add(new Album
                {
                    Name = NameData.GetCompanyName(),
                    Description = TextData.GetSentence()
                });
            }

            databaseContext.SaveChanges();
            return "ok";
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}