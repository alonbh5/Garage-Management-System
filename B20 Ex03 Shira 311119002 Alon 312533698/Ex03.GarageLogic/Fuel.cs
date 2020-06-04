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
        
        internal Fuel (eFuelType i_FuelType,float i_MaxFuel)
        {
            r_FuelType = i_FuelType;
            r_MaxFuelTank = i_MaxFuel;
            m_CurrentFuelTank = 0f;
        }

        internal eFuelType FuelType
        {
            get { return r_FuelType; }
        }

        internal float MaxTank
        {
            get { return r_MaxFuelTank; }
        }

        public bool FillTank (float i_AmountToAdd, eFuelType i_FuelType)
        {

        }

    }
}
