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

    class Motorcycle : Vehicle
    {
         eLicenseType m_LiccenseType;
         int m_EngineCapacityInCC;

        internal Motorcycle(string i_SerialNumber, uint i_NumOfWheels, float io_MaxAirPressure, object i_EnergyType) :
        base(i_SerialNumber, i_NumOfWheels, io_MaxAirPressure, i_EnergyType)
        {           
        }


    }
}
