using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hogent_Stages.Repository.Stages;
using Hogent_Stages.Repository.Stages.Model;

namespace HoGent_Stages.Models.Domain
{
    public interface IUserRepository
    {
        User FindBy(int userId);
        IQueryable<User> FindAll();
        void Add(User user);
        void Delete(User user);
        void SaveChanges();
        IEnumerable<User> GetAll();
    }
}