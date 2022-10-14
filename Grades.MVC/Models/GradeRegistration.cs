using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Grades.MVC.Models
{
    public class GradeRegistration
    {
        [Required]
        [DisplayName("Select student")]
        public int SelectedStudentId { get; set; }
        [Required]
        [DisplayName("Select discipline")]
        public int SelectedDisciplineId { get; set; }
        public List<SelectListItem> Students { get; set; }
        public List<SelectListItem> Disciplines { get; set; }
        [Range(2,5)]
        public int Grade { get; set; }
    }
}