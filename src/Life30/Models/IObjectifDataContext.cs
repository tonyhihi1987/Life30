using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Life30.ViewModels;

namespace Life30.Models
{
    public interface IObjectifDataContext
    {
        List<Objectif> GetObjectifs();
        Objectif GetObjectif(int id);
        List<Objectif> GetObjectifsByType(string name);
        ObjectifType GetObjectifTypeByName(string name);
        ObjectifType GetObjectifTypeById(int id);
        List<Item> GetItems(int ObjectifTypeId);
        void AddObjectif(Objectif objectif);
        void UpdateObjectifs(Objectif obj);
        void DeleteObjectif(Objectif obj);

    }
}
