using System.ComponentModel;

namespace Grades.MVC.Models
{
    public class GradeSum
    {
        public string StudentName { get; set; }
        [DisplayName("Document id")]
        public string StudentDocumentId { get; set; }
        [DisplayName("Overall score")]
        public int Value { get; set; }
    }
}