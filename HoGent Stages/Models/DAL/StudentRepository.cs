using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using HoGent_Stages.Models.Domain;
using Hogent_Stages.Models.Domain;

namespace HoGent_Stages.Models.DAL
{
    public class StudentRepository : IStudentRepository
    {
            private stagesContext context;
            private DbSet<Student> students;

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

            public Student FindBy(int studentId)
            {
                return students.Find(studentId);
            }

            public IQueryable<Student> FindAll()
            {
                return students.Include(b => b.email).OrderBy(b => b.email);
            }

            public void SaveChanges()
            {
                context.SaveChanges();
            }

    }
}