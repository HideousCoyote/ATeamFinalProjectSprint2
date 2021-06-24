using ATeamFinalProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ATeamFinalProject.Controllers
{
    //[Authorize(Roles ="Swimmer")]
    public class SwimmerController : Controller
    {
        private readonly ApplicationDbContext db;
        public SwimmerController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddProfile()
        {
            var currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Swimmer swimmer = new Swimmer();
            if (db.Swimmers.Any(s => s.UserId == currentUserId))
            {
                swimmer = db.Swimmers.FirstOrDefault(s => s.UserId == currentUserId);
            }
            else
            {
                swimmer.UserId = currentUserId;
            }
            return View(swimmer);
        }
        [HttpPost]
        public async Task<IActionResult> AddProfile(Swimmer swimmer)
        {
            var currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (db.Swimmers.Any(s => s.UserId == currentUserId))
            {
                var swimmerToUpdate = db.Swimmers.FirstOrDefault(s => s.UserId == currentUserId);
                swimmerToUpdate.SwimmerName = swimmer.SwimmerName;
                db.Update(swimmerToUpdate);
            }
            else
            {
                db.Add(swimmer);
            }
            await db.SaveChangesAsync();
            return View("Index");
        }
        public async Task<IActionResult> AllSession()
        {
            var session = await db.Sessions.Include(s => s.Coach).ToListAsync();
            return View(session);
        }
        public async Task<IActionResult> EnrollSession(int id)
        {
            var currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var swimmerId = db.Swimmers.FirstOrDefault(s => s.UserId == currentUserId).SwimmerId;
            Enrollment enrollment = new Enrollment
            {
                SessionId = id,
                SwimmerId = swimmerId
            };
            db.Add(enrollment);
            var session = await db.Sessions.FindAsync(enrollment.SessionId);
            session.SeatsAvailable--;
            await db.SaveChangesAsync();
            return View("Index");
        }
    }
}