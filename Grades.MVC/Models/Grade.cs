namespace Grades.MVC.Models
{
    public class Grade
    {
        public int GradeId { get; set; }
        public int StudentId { get; set; }
        public int DisciplineId { get; set; }
        public int Value { get; set; }
        public virtual Student Student { get; set; }
        public virtual Discipline Discipline { get; set; }
    }
}