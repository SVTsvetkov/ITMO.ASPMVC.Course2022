using System.Data.Entity;

namespace Grades.MVC.Models
{
    public class GradesDbInitializer : DropCreateDatabaseIfModelChanges<Context>
    {
        protected override void Seed(Context context)
        {
            context.Disciplines.Add(new Discipline { Description = "", Name = "Math" });
            context.Disciplines.Add(new Discipline { Description = "Physical Education", Name = "PE" });
            context.Disciplines.Add(new Discipline { Description = "", Name = "English" });
            context.Disciplines.Add(new Discipline { Description = "", Name = "Russian" });


            context.Students.Add(new Student { Name = "Ivan", Address = "Moscow", StudentDocumentId = "3453453", Age = 20 });
            context.Students.Add(new Student { Name = "Petr", Address = "Moscow", StudentDocumentId = "9756765", Age = 30 });
            context.Students.Add(new Student { Name = "Slava", Address = "Moscow", StudentDocumentId = "423543", Age = 21 });
            context.Students.Add(new Student { Name = "Bogdan", Address = "Moscow", StudentDocumentId = "653567", Age = 22 });
            context.Students.Add(new Student { Name = "Igor", Address = "Moscow", StudentDocumentId = "235643", Age = 20 });
            context.Students.Add(new Student { Name = "Masha", Address = "Moscow", StudentDocumentId = "1123123", Age = 22 });
            context.Students.Add(new Student { Name = "Sasha", Address = "Moscow", StudentDocumentId = "756466", Age = 20 });
            context.Students.Add(new Student { Name = "Valentin", Address = "Moscow", StudentDocumentId = "09567345", Age = 20 });
            context.Students.Add(new Student { Name = "Alex", Address = "Moscow", StudentDocumentId = "785967", Age = 31 });
            context.Students.Add(new Student { Name = "Valentina", Address = "Moscow", StudentDocumentId = "34592850", Age = 20 });

            base.Seed(context);
        }
    }
}