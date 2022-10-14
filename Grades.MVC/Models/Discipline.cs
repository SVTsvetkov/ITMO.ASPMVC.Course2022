using System.ComponentModel.DataAnnotations;

namespace Grades.MVC.Models
{
    public class Discipline
    {
        public int DisciplineId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}