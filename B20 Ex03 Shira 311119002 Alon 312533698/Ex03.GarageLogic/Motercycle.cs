using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public enum eLicenseType
    {
        A,
        A1,
        Aa,
        B
    }

    class Motercycle : Vehicle
    {
        readonly eLicenseType r_LiccenseType;
        readonly int r_EngineCapacityInCC;
    }
}
