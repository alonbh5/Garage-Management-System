using System;

namespace Ex03.GarageLogic
{
    public enum eLicenseTypes
    {
        A = 1,
        A1,
        Aa,
        B
    }

    internal class Motorcycle : Vehicle
    {
         private eLicenseTypes m_LicenseType;
         private int m_EngineCapacityInCC;

        internal Motorcycle(string i_LicenseNumber, uint i_NumOfWheels, float io_MaxAirPressure, object i_EnergyType) :
        base(i_LicenseNumber, i_NumOfWheels, io_MaxAirPressure, i_EnergyType)
        {           
        }

        internal eLicenseTypes LicenseType
        {
            get { return m_LicenseType; }
            set
            {
                if (Enum.IsDefined(typeof(eLicenseTypes), value))
                {
                    m_LicenseType = value;
                }
                else
                {
                    Exception ex = new Exception("License type's input is invalid");
                    throw new ValueOutOfRangeException(ex, NumOfLicenseTypes(), 1f);                 
                }
            }
        }

        internal int EngineCapacityInCC
        {
            get { return m_EngineCapacityInCC; }
            set
            {
                if (value >= 0)
                {
                    m_EngineCapacityInCC = value;
                }
                else
                {
                    throw new ArgumentException("Engine capacity's input is invalid");
                }
            }            
        }

        public int NumOfLicenseTypes()
        {
            return Enum.GetValues(typeof(eLicenseTypes)).Length;
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