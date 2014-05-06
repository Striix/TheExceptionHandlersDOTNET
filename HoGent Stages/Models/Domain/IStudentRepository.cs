using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hogent_Stages.Repository.Stages;
using Hogent_Stages.Repository.Stages.Model;

namespace HoGent_Stages.Models.Domain
{
    public interface IStudentRepository
    {
        Student FindBy(int studentId);
        IQueryable<Student> FindAll();
        void Add(Student stage);
        void Delete(Student stage);
        void SaveChanges();
        IEnumerable<Student> GetAll();
    }
}