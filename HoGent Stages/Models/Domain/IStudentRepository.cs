using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoGent_Stages.Models.Domain
{
    public interface IStudentRepository
    {
        Student FindBy(int studentId);
        IQueryable<Student> FindAll();
        void Add(Student student);
        void Delete(Student student);
        void SaveChanges();
        IEnumerable<Student> GetAll();
    }
}