using Life30.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Life30.ViewModels;

namespace Life30.DAL
{
    public class UserDataContext : IdentityDbContext<ApplicationUser>, IUserDataContext
    {
        public DbSet<User> UsersList { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            optionsBuilder
                .UseSqlServer(config["Data:DefaultConnection:ConnectionString"]);
        }

       

        public int GetUserIdWhithName(string name)
        {
            return UsersList.Where(a => a.Name.Equals(name)).Select(a => a.Id).FirstOrDefault();
        }

        public void AddNewUser(string name)
        {
            UsersList.Add(new User { Name = name });
            SaveChanges();

        }
    }
}
