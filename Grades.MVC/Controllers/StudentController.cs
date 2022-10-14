using System.Linq;
using System.Web.Mvc;
using Grades.MVC.Models;

namespace Grades.MVC.Controllers
{
    public class StudentController : Controller
    {
        private Context db;

        public StudentController()
        {
            db = new Context();
        }
        public ActionResult Index()
        {
            var list = db.Students.ToList();
            return View(list);
        }

        public ActionResult Edit(int id)
        {
            var student = db.Students.FirstOrDefault(s => s.StudentId == id);
            if (student == null)
            {
                TempData["ErrorMessage"] = "Student not found!";
                return RedirectToAction("Index");
            }
            return View(student);
        }

        [HttpPost]
        public ActionResult Edit(Student std)
        {
            if (!ModelState.IsValid)
            {
                return View(std);
            }
            var student = db.Students.FirstOrDefault(s => s.StudentId == std.StudentId);
            if (student == null)
            {
                ViewBag.ErrorMessage = "Student not found!";
                return View(std);
            }
            student.Age = std.Age;
            student.Name = std.Name;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            var newStudent = new Student();
            return View(newStudent);
        }

        [HttpPost]
        public ActionResult Create(Student std)
        {
            if (!ModelState.IsValid)
            {
                return View(std);
            }
            db.Students.Add(std);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var student = db.Students.FirstOrDefault(s => s.StudentId == id);
            if (student == null)
            {
                TempData["ErrorMessage"] = "Student not found!";
                return RedirectToAction("Index");
            }
            db.Students.Remove(student);
            var grades = db.Grades.Where(x => x.StudentId == id).ToArray();
            db.Grades.RemoveRange(grades);
            db.SaveChanges();

            ViewBag.InfoMessage = $"Student {student.Name} deleted successfully";
            return RedirectToAction("Index");
        }
    }
}