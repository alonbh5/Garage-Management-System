using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Vehicle
    {
        string m_ModelName;
        readonly string r_SerialNumber;
        float m_PercentagesOfEnergyRemaining;
        readonly Wheel[] r_Wheels;
        object r_EnergyType;

        protected Vehicle(string i_SerialNumber, uint i_NumOfWheels, float io_MaxAirPressure, object i_EnergyType)
        {
            m_PercentagesOfEnergyRemaining = 0f;
            r_EnergyType = i_EnergyType;
            m_ModelName = string.Empty;
            r_SerialNumber = i_SerialNumber;
            r_Wheels = new Wheel[i_NumOfWheels];

            for (int i = 0; i < r_Wheels.Length; i++)
            {
                r_Wheels[i] = new Wheel(io_MaxAirPressure);
            }       
        }

    }
}
