using Be.Areas.Admin.ViewModels;
using Be.DAL;
using Be.Models;
using Be.Utilities.Extentions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Be.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public TeamController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Team> teams = await _db.Teams.Include(x => x.Position).ToListAsync();
            return View(teams);
        }

        public async Task<IActionResult> Create()
        {
            CreateTeamVM create = new CreateTeamVM
            {
                Positions = await _db.Positions.ToListAsync()
            };
            return View(create);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateTeamVM create)
        {
            if (!ModelState.IsValid)
            {
                create.Positions = await _db.Positions.ToListAsync();
                return View(create);
            }

            bool result = await _db.Teams.AnyAsync(x => x.Name.Trim().ToLower() == create.Name.Trim().ToLower());
            if (result)
            {
                create.Positions = await _db.Positions.ToListAsync();
                ModelState.AddModelError("Name", "is exists");
                return View(create);
            }
            if (!create.Photo.ValidateType())
            {
                create.Positions = await _db.Positions.ToListAsync();
                ModelState.AddModelError("Photo", "not valid");
                return View(create);
            }
            if (!create.Photo.ValidateSize(10))
            {
                create.Positions = await _db.Positions.ToListAsync();
                ModelState.AddModelError("Photo", "max 10mb");
                return View(create);
            }

            Team team = new Team
            {
                Name = create.Name,
                Surname = create.Surname,
                TwitLink = create.TwitLink,
                FaceLink = create.FaceLink,
                PlusLink = create.PlusLink,
                PositionId = create.PositionId,
                Image = await create.Photo.CreateFile(_env.WebRootPath, "assets", "img")
            };

            await _db.Teams.AddAsync(team);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) return BadRequest();
            Team team = await _db.Teams.FirstOrDefaultAsync(x => x.Id == id);
            if (team == null) return NotFound();

            UpdateTeamVM update = new UpdateTeamVM
            {
                Name = team.Name,
                Surname = team.Surname,
                TwitLink = team.TwitLink,
                FaceLink = team.FaceLink,
                PlusLink = team.PlusLink,
                PositionId = team.PositionId,
                Image = team.Image,
                Positions = await _db.Positions.ToListAsync()
            };

            return View(update);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateTeamVM update)
        {
            if (!ModelState.IsValid)
            {
                update.Positions = await _db.Positions.ToListAsync();
                return View(update);
            }
            Team team = await _db.Teams.FirstOrDefaultAsync(x => x.Id == id);
            if (team == null) return NotFound();

            bool result = await _db.Teams.AnyAsync(x => x.Name.Trim().ToLower() == update.Name.Trim().ToLower() && x.Id != id);
            if (result)
            {
                update.Positions = await _db.Positions.ToListAsync();
                ModelState.AddModelError("Name", "is exists");
                return View(update);
            }

            if (update.Photo is not null)
            {
                if (!update.Photo.ValidateType())
                {
                    update.Positions = await _db.Positions.ToListAsync();
                    ModelState.AddModelError("Photo", "not valid");
                    return View(update);
                }
                if (!update.Photo.ValidateSize(10))
                {
                    update.Positions = await _db.Positions.ToListAsync();
                    ModelState.AddModelError("Photo", "max 10mb");
                    return View(update);
                }
                team.Image.DeleteFile(_env.WebRootPath, "assets", "img");
                team.Image = await update.Photo.CreateFile(_env.WebRootPath, "assets", "img");
            }

            team.Name = update.Name;
            team.Surname = update.Surname;
            team.PositionId = update.PositionId;
            team.FaceLink = update.FaceLink;
            team.TwitLink = update.TwitLink;
            team.PlusLink = update.PlusLink;

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();
            Team team = await _db.Teams.FirstOrDefaultAsync(x => x.Id == id);
            if (team == null) return BadRequest();

            _db.Teams.Remove(team);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
