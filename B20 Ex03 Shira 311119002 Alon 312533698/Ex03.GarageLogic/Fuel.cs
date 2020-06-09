using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public enum eFuelType
    {
        Octan95 = 1,
        Octan96,
        Octan98, 
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
            set
            {
                if (value >= 0 && value <= MaxTank)
                {
                    m_CurrentFuelTank = value;
                }
                else
                {
                    Exception ex = new Exception("Amount of fuel's input is invalid");
                    throw new ValueOutOfRangeException(ex, MaxTank, 0f);
                }
            }
        }

        public static string GetFuelTypes()
        {
           // int index = 1;
            StringBuilder fuelTypes = new StringBuilder();

            foreach (eFuelType fuelType in Enum.GetValues(typeof(eFuelType)))
            {
                fuelTypes.Append(string.Format("{0}. {1}{2}", (int)fuelType, fuelType.ToString(), Environment.NewLine));
            }

            return fuelTypes.ToString();
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
                else
                {
                    Exception ex = new Exception("Fuel amount Invalid");
                    throw new ValueOutOfRangeException(ex, MaxTank - CurrentFuelTank, 0f);
                }
            }
            else
            {
                throw new ArgumentException("Fuel Type does not match");
            }            

            return filled;
        }       

        public override string ToString()
        {
            string fuel = string.Format(
                "Fuel type is {0}, it has {1} amount of gas left out of {2}{3}",
                FuelType.ToString(),
                CurrentFuelTank,
                MaxTank,
                Environment.NewLine);           

            return fuel;
        }
    }
}