using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using HoGent_Stages.Models.Domain;

namespace HoGent_Stages.Models.DAL
{
    public class UserRepository : IUserRepository
    {
        private StagesContext context;
        private DbSet<User> users;

        public UserRepository(StagesContext context)
        {
            this.context = context;
            users = context.User;
        }

        public IEnumerable<User> GetAll()
        {
            return users.AsEnumerable();
        }

        public void Add(User user)
        {
            users.Add(user);
        }

        public void Delete(User user)
        {
            users.Remove(user);
        }

        public User FindBy(int userId)
        {
            return users.Find(userId);
        }

        public IQueryable<User> FindAll()
        {
            return users.Include(b => b.email).OrderBy(b => b.email);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}