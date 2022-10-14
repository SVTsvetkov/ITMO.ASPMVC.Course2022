using System.Linq;
using System.Web.Mvc;
using Grades.MVC.Models;
using System.Data.Entity;

namespace Grades.MVC.Controllers
{
    public class GradeController : Controller
    {
        private Context db;

        public GradeController()
        {
            db = new Context();
        }

        public ActionResult Index()
        {
            var list = db.Disciplines.ToList();
            return View(list);
        }
        public ActionResult FindByDiscipline(int id)
        {
            var list = db.Grades.Include(x => x.Discipline).Include(x => x.Student).ToList();
            var disciplineGrades = list.Where(x => x.DisciplineId == id);
            var model = disciplineGrades.Select(
                    x => new StudentWithGrades
                    {
                        Grade = x.Value,
                        GradeId = x.GradeId,
                        StudentDocumentId = x.Student.StudentDocumentId,
                        StudentName = x.Student.Name
                    })
                .ToArray();
            return PartialView(model);
        }

        public ActionResult Register()
        {
            var gradeRegistration = new GradeRegistration();
            gradeRegistration.Students = db.Students.ToArray().Select(x => new SelectListItem {Text = $"{x.Name} - {x.StudentDocumentId}", Value = x.StudentId.ToString()}).ToList();
            gradeRegistration.Disciplines = db.Disciplines.ToArray().Select(x => new SelectListItem { Text = x.Name, Value = x.DisciplineId.ToString() }).ToList();
            return View(gradeRegistration);
        }

        [HttpPost]
        public ActionResult Register(GradeRegistration registration)
        {
            if (!ModelState.IsValid)
            {
                registration.Students = db.Students.ToArray().Select(x => new SelectListItem { Text = $"{x.Name} - {x.StudentDocumentId}", Value = x.StudentId.ToString()}).ToList();
                registration.Disciplines = db.Disciplines.ToArray().Select(x => new SelectListItem { Text = x.Name, Value = x.DisciplineId.ToString() }).ToList();
                return View(registration);
            }
            var student = db.Students.FirstOrDefault(x => x.StudentId == registration.SelectedStudentId);
            var discipline = db.Disciplines.FirstOrDefault(x => x.DisciplineId == registration.SelectedDisciplineId);
            if (student != null && discipline != null)
            {
                if (db.Grades.Any(x => x.DisciplineId == discipline.DisciplineId && x.StudentId == student.StudentId))
                {
                    var existingGrade = db.Grades.FirstOrDefault(x =>
                        x.DisciplineId == discipline.DisciplineId && x.StudentId == student.StudentId);
                    existingGrade.Value = registration.Grade;
                }
                else
                {
                    db.Grades.Add(new Grade
                    {
                        StudentId = student.StudentId,
                        DisciplineId = discipline.DisciplineId,
                        Value = registration.Grade
                    });
                }
                db.SaveChanges();
            }
            else
            {
                ViewBag.ErrorMessage = "Wrong data. Check form again";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var grade = db.Grades.FirstOrDefault(s => s.GradeId == id);
            if (grade == null)
            {
                TempData["ErrorMessage"] = "Grade not found!";
                return RedirectToAction("Index");
            }
            db.Grades.Remove(grade);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Statistics()
        {
            var students = db.Students.ToList();
            if (students.Count() < 10)
            {
                ViewBag.InfoMessage = "Not enough data for statistics";
            }
            var list = db.Grades.Include(x => x.Discipline).Include(x => x.Student).ToList();
            var studentsGeneralGrades = list.GroupBy(x => x.StudentId).ToArray();
           
            var model = studentsGeneralGrades.Select(x => new GradeSum
                { StudentName = x.FirstOrDefault().Student.Name, StudentDocumentId = x.FirstOrDefault().Student.StudentDocumentId, Value = x.Sum(y => y.Value) })
                .OrderByDescending(x => x.Value).ToList();
            if (studentsGeneralGrades.Length < students.Count())
            {
                var studentsWithoutGrades = students.Where(x => studentsGeneralGrades.All(y => y.Key != x.StudentId));
                model.AddRange(studentsWithoutGrades.Select(x => new GradeSum {StudentDocumentId = x.StudentDocumentId, StudentName = x.Name, Value = 0}));
            }
            return View(model);
        }
    }
}