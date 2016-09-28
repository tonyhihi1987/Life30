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
    public class DepenseDataContext : IdentityDbContext<ApplicationUser>,  IDepenseDataContext
    {
        public DbSet<Depense> Depenses { get; set; }
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
        public void AddDepenses(Depense depense)
        {
            Depenses.Add(depense);
            SaveChanges();
        }

        public void DeleteDepense(Depense depense)
        {
            var dep = Depenses.Where(a => a.Id.Equals(depense.Id)).FirstOrDefault();
            Depenses.Remove(dep);
            SaveChanges();
        }

        public List<Depense> GetDepenses(int userId)
        {
            return Depenses.Where(a => a.UserId.Equals(userId)).ToList();
        }

        public void UpdateDepense(Depense depense)
        {
            var dep = Depenses.Where(a => a.Id.Equals(depense.Id)).FirstOrDefault();
            dep = depense;
            SaveChanges();
        }
    }
}
