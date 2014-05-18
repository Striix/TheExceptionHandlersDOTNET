using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using HoGent_Stages.Models.Domain;

namespace HoGent_Stages.Models.DAL
{
    public class StudentRepository : IStudentRepository
    {
            private stagesContext context;
            public DbSet<Student> students;

            public StudentRepository(stagesContext context)
            {
                this.context = context;
                students = context.Student;
            }

            public IEnumerable<Student> GetAll()
            {
                return students.AsEnumerable();
            }

            public void Add(Student student)
            {
                students.Add(student);
            }

            public void Delete(Student student)
            {
                students.Remove(student);
            }

            public Student FindBy(string studentId)
            {
                return students.Find(studentId);
            }

            public IQueryable<Student> FindAll()
            {
                return students.Include(b => b.email).OrderBy(b => b.email);
            }
            public IQueryable<Stage> FindAllStudentOpdrachten(ICollection<Stage> lijst)
            {

                IEnumerable<Student> sublijst = FindAll();//alle studenten
                for (int i = 0; i < sublijst.Count(); i++)
                {
                    for (int j = 0; j < sublijst.ElementAt(i).Stage.Count(); j++)//opdrachtlijst = sublijst.ElementAt(i).Stageopdrachten
                    {

                        if (!lijst.Contains(sublijst.ElementAt(i).Stage.ElementAt(j)))
                        {
                            lijst.Add(sublijst.ElementAt(i).Stage.ElementAt(j));//voegt opdracht op element j van student i aan de lijst toe
                        }
                    }
                }
                return lijst.AsQueryable();
            }

            public void SaveChanges()
            {
                context.SaveChanges();
            }
           

        
    }
}