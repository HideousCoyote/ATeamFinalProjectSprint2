using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ATeamFinalProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ATeamFinalProject.Controllers
{
    public class LessonController : Controller
    {
        private readonly ApplicationDbContext db;
        public LessonController(ApplicationDbContext db)
        {
            this.db = db;
        }
        //async allLesson method for efficiency
        public async Task<IActionResult> AllLesson()
        {
            var lesson = await db.Lessons.ToListAsync();
            return View(lesson);
        }
            public IActionResult Index()
        {
            return View();
        }
        //add a swim lesson 
        public async Task<IActionResult> AddLesson(int? id)
        {
            var lesson = await db.Lessons.SingleOrDefaultAsync(c => c.LessonId == id);
            ViewBag.Lesson = lesson;
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
