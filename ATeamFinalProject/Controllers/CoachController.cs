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
    //[Authorize(Roles ="Coach")]
    public class CoachController : Controller
    {
        private readonly ApplicationDbContext db;
        public CoachController(ApplicationDbContext db)
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
            Coach coach = new Coach();
            if (db.Coaches.Any(c => c.UserId == currentUserId))
            {
                coach = db.Coaches.FirstOrDefault(c => c.UserId == currentUserId);
            }
            else
            {
                coach.UserId = currentUserId;
            }
            return View(coach);
        }
        [HttpPost]
        public async Task<IActionResult> AddProfile(Coach coach)
        {
            var currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (db.Coaches.Any(c => c.UserId == currentUserId))
            {
                var coachToUpdate = db.Coaches.FirstOrDefault(c => c.UserId == currentUserId);
                coachToUpdate.CoachName = coach.CoachName;
                coachToUpdate.CoachName = coach.CoachNumber;
                db.Update(coachToUpdate);
            }
            else
            {
                db.Add(coach);
            }
            await db.SaveChangesAsync();
            return View("Index");
        }
        public IActionResult AddSession()
        {
            Session session = new Session();
            var currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            session.CoachId = db.Coaches.SingleOrDefault(c => c.UserId == currentUserId).CoachId;
            return View(session);
        }
        [HttpPost]
        public async Task<IActionResult> AddSession(Session session)
        {
            db.Add(session);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Coach");
        }
        public async Task<IActionResult> SessionByCoach()
        {
            var currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var CoachId = db.Coaches.SingleOrDefault(c => c.UserId == currentUserId).CoachId;
            var session = await db.Sessions.Where(c => c.CoachId == CoachId).ToListAsync();
            return View(session);
        }
        public async Task<IActionResult> PostProgress(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var allSwimmers = await db.Enrollments.Include(c => c.Session).Where(c => c.SessionId == id).ToListAsync();
            if (allSwimmers == null)
            {
                return NotFound();
            }
            return View(allSwimmers);
        }
    }
}