using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Grades.MVC.Models
{
    public class StudentWithGrades
    {
        public int GradeId { get; set; }
        public int Grade { get; set; }
        public string StudentName { get; set; }
        public string StudentDocumentId { get; set; }
    }
}