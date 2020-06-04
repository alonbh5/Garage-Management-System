using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public enum eColor
    {
        Black,
        White,
        Gray,
        Silver
    }
    public enum eDoors
    {
        Two = 2,
        Three,
        Four,
        Five
    }


    class Car : Vehicle
    {
        readonly eColor r_Color;
        readonly eDoors r_Doors; 
    }
}
