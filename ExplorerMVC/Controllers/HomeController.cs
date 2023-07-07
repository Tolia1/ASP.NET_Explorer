using ExplorerMVC.Models;
using ExplorerMVC.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ExplorerMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ExplorerDbContext explorerDbContext;

        public HomeController(ILogger<HomeController> logger, ExplorerDbContext explorerDbContext)
        {
            _logger = logger;
            this.explorerDbContext = explorerDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var folders = await explorerDbContext.Folders.Where(f => f.ParentId == null).ToListAsync();
            return View(folders);
        }
        public async Task<IActionResult> ViewFolder(int Id)
        {
            var parentFolder = await explorerDbContext.Folders.Where(f => f.Id == Id).ToListAsync();
            ViewData["parentName"] = parentFolder[0].Name;
            if(parentFolder[0].ParentId == null)
                ViewData["backLink"] = "/Home/Index";
            else
                ViewData["backLink"] = parentFolder[0].ParentId;
            var folders = await explorerDbContext.Folders.Where(f => f.ParentId == Id).ToListAsync();
            return View(folders);
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