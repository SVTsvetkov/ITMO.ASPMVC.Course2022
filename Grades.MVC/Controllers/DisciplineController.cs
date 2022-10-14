using System.Linq;
using System.Web.Mvc;
using Grades.MVC.Models;

namespace Grades.MVC.Controllers
{
    public class DisciplineController : Controller
    {
        private Context db;

        public DisciplineController()
        {
            db = new Context();
        }
        public ActionResult Index()
        {
            var list = db.Disciplines.ToList();
            return View(list);
        }

        public ActionResult Edit(int id)
        {
            var existingDiscipline = db.Disciplines.FirstOrDefault(s => s.DisciplineId == id);
            if (existingDiscipline == null)
            {
                TempData["ErrorMessage"] = "Discipline not found!";
                return RedirectToAction("Index");
            }
            return View(existingDiscipline);
        }

        [HttpPost]
        public ActionResult Edit(Discipline discipline)
        {
            if (!ModelState.IsValid)
            {
                return View(discipline);
            }
            var existingDiscipline = db.Disciplines.FirstOrDefault(s => s.DisciplineId == discipline.DisciplineId);
            if (existingDiscipline == null)
            {
                ViewBag.ErrorMessage = "Discipline not found!";
                return View(discipline);
            }
            existingDiscipline.Name = discipline.Name;
            existingDiscipline.Description = discipline.Description;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            var newDiscipline = new Discipline();
            return View(newDiscipline);
        }

        [HttpPost]
        public ActionResult Create(Discipline discipline)
        {
            if (!ModelState.IsValid)
            {
                return View(discipline);
            }
            db.Disciplines.Add(discipline);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var existingDiscipline = db.Disciplines.FirstOrDefault(s => s.DisciplineId == id);
            if (existingDiscipline == null)
            {
                TempData["ErrorMessage"] = "Discipline not found!";
                return RedirectToAction("Index");
            }
            db.Disciplines.Remove(existingDiscipline);
            var grades = db.Grades.Where(x => x.DisciplineId == id).ToArray();
            db.Grades.RemoveRange(grades);
            db.SaveChanges();

            ViewBag.InfoMessage = $"Discipline {existingDiscipline.Name} deleted successfully";
            return RedirectToAction("Index");
        }
    }
}