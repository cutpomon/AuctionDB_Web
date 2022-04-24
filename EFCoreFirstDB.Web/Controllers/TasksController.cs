using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreFirstDB.Web.Controllers
{
    public class TasksController : Controller
    {
        private readonly AuctionContext _context;

        public TasksController(AuctionContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Task1()
        {
            return View();
        }
    }
}
