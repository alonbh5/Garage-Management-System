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

    internal class Fuel
    {
        private readonly eFuelType r_FuelType;
        private readonly float r_MaxFuelTank;
        private float m_CurrentFuelTank;

        internal Fuel(eFuelType i_FuelType, float i_MaxFuel)
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

        internal float CurrentFuelTank
        {
            get { return m_CurrentFuelTank; }
            set { m_CurrentFuelTank = value; }
        }

        public bool FillTank(float i_AmountToAdd, eFuelType i_FuelType)
        {
            bool filled = false;

            if (i_FuelType.Equals(FuelType))
            {
                if (i_AmountToAdd + CurrentFuelTank <= MaxTank)
                {
                    CurrentFuelTank += i_AmountToAdd;
                    filled = true;
                }
            }
            return filled;
        }

        public override string ToString()
        {
            StringBuilder msg = new StringBuilder();

            msg.Append(string.Format("Fuel type is {0}, it has {1} amount of gas left out of {2}{3}",FuelType.ToString(),CurrentFuelTank,MaxTank,Environment.NewLine));
            

            return msg.ToString();
        }
    }
}