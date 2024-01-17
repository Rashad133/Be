using Be.DAL;
using Be.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Be.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;
        public HomeController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            List<Team> teams = await _db.Teams.Include(x=>x.Position).ToListAsync();
            return View(teams);
        }
    }
}
