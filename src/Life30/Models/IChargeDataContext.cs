using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Life30.Models
{
    public interface IChargeDataContext
    {
        List<Charge> GetCharges(int userId);
        void AddCharge(Charge charge);
        void UpdateCharge(Charge charge);
        void DeleteCharge(Charge charge);

    }
}
