using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Life30.Models
{
    public interface IDepenseDataContext
    {
        List<Depense> GetDepenses(int userId);
        void AddDepenses(Depense objectif);
        void UpdateDepense(Depense obj);
        void DeleteDepense(Depense obj);

    }
}
