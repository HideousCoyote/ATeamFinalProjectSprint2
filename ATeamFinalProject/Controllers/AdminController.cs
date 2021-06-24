using ATeamFinalProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ATeamFinalProject.Controllers
{
   // [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        ApplicationDbContext db;
        public AdminController(ApplicationDbContext db)
        {
            this.db = db;
        }
        //async allLesson method for efficiency
        public async Task<IActionResult> AllLesson()
        {
            var lesson = await db.Lessons.ToListAsync();
            return View(lesson);
        }
        //add a swim lesson 
        public async Task<IActionResult> AddLesson(int? id)
        {
            var lesson = await db.Lessons.SingleOrDefaultAsync(c => c.LessonId == id);
            ViewBag.Lesson = lesson;
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
        //HTTPPOST Add a Lesson action method for update to database
        [HttpPost]
        public async Task<IActionResult> AddLesson(Lesson lesson)
        {
            db.Add(lesson);
            await db.SaveChangesAsync();
            return RedirectToAction("AllLesson", "Lesson");
        }

    }
}
