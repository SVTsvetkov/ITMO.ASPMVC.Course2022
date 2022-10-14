using System.Data.Entity;

namespace Grades.MVC.Models
{
    public class Context : DbContext
    {
        public Context() : base("DefaultConnection")
        {
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Discipline> Disciplines { get; set; }
        public DbSet<Grade> Grades { get; set; }
    }
}