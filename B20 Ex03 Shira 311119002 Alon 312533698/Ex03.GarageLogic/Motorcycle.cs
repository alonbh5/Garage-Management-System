using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

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
            set
            {
                if (Enum.IsDefined(typeof(eLicenseType), value))
                {
                    m_LicenseType = value;
                }
                else
                {
                    Exception ex = new Exception("License type's input is invalid");
                    throw new ValueOutOfRangeException(ex, 3f, 0f);                 
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