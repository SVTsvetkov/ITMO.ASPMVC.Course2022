using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.Ajax.Utilities;

namespace Grades.MVC.Models
{
    public class Student
    {
        public int StudentId { get; set; }

        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(8)]
        [DisplayName("Document id")]
        public string StudentDocumentId { get; set; }
        [Required]
        [Range(18, 100)]
        public int Age { get; set; }
    }
}