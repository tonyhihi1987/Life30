using Life30.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Life30.DAL
{
    public class ChargeDataContext : IdentityDbContext<ApplicationUser>, IChargeDataContext
    {
        public DbSet<Charge> Charges { get; set; }
        public DbSet<User> UsersList { get; set; }

        public void AddCharge(Charge depense)
        {
            Charges.Add(depense);
            SaveChanges();
        }

        public void DeleteCharge(Charge charge)
        {
            var oldCharge = Charges.Where(a => a.Id.Equals(charge.Id)).FirstOrDefault();
            Charges.Remove(oldCharge);
            SaveChanges();
        }

        public List<Charge> GetCharges(int userId)
        {
            return Charges.Where(a => a.UserId.Equals(userId)).ToList();
        }

        public void UpdateCharge(Charge newCharge)
        {
            var charge = Charges.Where(a => a.Id.Equals(newCharge.Id)).FirstOrDefault();
            charge = newCharge;
            SaveChanges();
        }
    }
}
