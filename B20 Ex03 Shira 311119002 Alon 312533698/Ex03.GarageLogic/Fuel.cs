using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public enum eFuelType
    {
        Octan95 = 95,
        Octan96 = 96,
        Octan98 = 98,
        Soler
    }

    class Fuel
    {
        readonly eFuelType r_FuelType;
        readonly float r_MaxFuelTank;
        float m_CurrentFuelTank;

        public void FillTank (float i_AmountToAdd, eFuelType i_FuelType)
        {

        }

    }
}
