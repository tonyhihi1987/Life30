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
    public class ObjectifDataContext : IdentityDbContext<ApplicationUser>,  IObjectifDataContext
    {
        public DbSet<Objectif> Objectifs { get; set; }
        public DbSet<User> UsersList { get; set; }
        public DbSet<ObjectifType> ObjectifTypes { get; set; }
        public DbSet<Item> Items { get; set; }
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

        public Objectif GetObjectif(int id)
        {
            return Objectifs.Where(a => a.Id.Equals(id)).FirstOrDefault();                       
        }

        public List<Objectif> GetObjectifs()
        {
            return Objectifs.ToList();
        }

        public ObjectifType GetObjectifType(int id)
        {
            return ObjectifTypes.Where(a => a.Id.Equals(id)).FirstOrDefault() ;
        }

        public int GetTotalByType(ObjectifType type,User user)
        {
            return 0;
        }

        public void AddObjectif(Objectif objectif)
        {
            Objectifs.Add(objectif);
            SaveChanges();
        }

        public void EditObjectif (Objectif objectif)
        {
            var obj = Objectifs.Where(a => a.Id.Equals(objectif.Id)).FirstOrDefault();

            obj = objectif;
        }

        public void RemoveObjectif(Objectif objectif)
        {
            var obj = Objectifs.Where(a => a.Id.Equals(objectif.Id)).FirstOrDefault();

            Objectifs.Remove(obj);
        }

        public List<Item> GetItems(int ObjectifTypeId)
        {
            return Items.Where(a => a.ObjectifTypeId.Equals(ObjectifTypeId)).ToList();
        }

        public List<Objectif> GetObjectifsByType(string name)
        {
            ObjectifType objType = ObjectifTypes.Where(a => a.Name.Equals(name)).FirstOrDefault();
            if (Objectifs.Any())
                return Objectifs?.Where(a => a.ObjTypeId.Equals(objType.Id)).OrderByDescending(a => a.Date).ToList();
            else
                return null;
        }

        public ObjectifType GetObjectifTypeByName(string name)
        {
           return ObjectifTypes.Where(a => a.Name.Equals(name)).FirstOrDefault();
        }

        public ObjectifType GetObjectifTypeById(int id)
        {
            return ObjectifTypes.Where(a => a.Id.Equals(id)).FirstOrDefault();
        }

        public void DeleteObjectif(Objectif obj)
        {
            var objectif = Objectifs.Where(a => a.Id.Equals(obj.Id)).FirstOrDefault();
            Objectifs.Remove(objectif);
            SaveChanges();
        }

        public int GetUserIdWhithName(string name)
        {
            return UsersList.Where(a => a.Name.Equals(name)).Select(a => a.Id).FirstOrDefault();
        }

        public void UpdateObjectifs(Objectif obj)
        {
            var objectif = Objectifs.Where(a => a.Id.Equals(obj.Id)).FirstOrDefault();
            objectif.Id = obj.Id;
            objectif.Tache = obj.Tache;            
            objectif.NbPoint = obj.NbPoint;
            objectif.ObjTypeId = obj.ObjTypeId;
            objectif.Date = obj.Date;
            objectif.Commentaire = obj.Commentaire;
            SaveChanges();            
        }

        public void AddNewUser(string name)
        {
            UsersList.Add(new User { Name = name });
            SaveChanges();

        }
    }
}
