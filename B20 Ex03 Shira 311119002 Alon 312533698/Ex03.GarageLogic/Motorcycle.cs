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

    internal class Motorcycle : Vehicle
    {
         private eLicenseType m_LicenseType;
         private int m_EngineCapacityInCC;

        internal Motorcycle(string i_LicenseNumber, uint i_NumOfWheels, float io_MaxAirPressure, object i_EnergyType) :
        base(i_LicenseNumber, i_NumOfWheels, io_MaxAirPressure, i_EnergyType)
        {           
        }

        internal eLicenseType LicenseType
        {
            get { return m_LicenseType; }
            set { m_LicenseType = value; }
        }

        internal int EngineCapacityInCC
        {
            get { return m_EngineCapacityInCC; }
            set { m_EngineCapacityInCC = value; }
        }

        public override string ToString()
        {
            string motorcycle = string.Format(
                "{0}The motorcycle license type is {1} and his engine capacity is {2} CC{3}",
                base.ToString(),
                LicenseType.ToString(),
                EngineCapacityInCC,
                Environment.NewLine);

            return motorcycle;
        }
    }
}