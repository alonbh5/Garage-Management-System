using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Vehicle
    {
        private readonly string r_SerialNumber;
        private readonly Wheel[] r_Wheels;
        private readonly object r_EnergyType;
        private string m_ModelName;
        private float m_PercentagesOfEnergyRemaining;

        protected Vehicle(string i_SerialNumber, uint i_NumOfWheels, float io_MaxAirPressure, object i_EnergyType) //// protected?
        {
            m_PercentagesOfEnergyRemaining = 0f;
            m_ModelName = string.Empty;
            r_EnergyType = i_EnergyType;
            r_SerialNumber = i_SerialNumber;
            r_Wheels = new Wheel[i_NumOfWheels];

            for (int i = 0; i < r_Wheels.Length; i++)
            {
                r_Wheels[i] = new Wheel(io_MaxAirPressure);
            }
        }

        internal Wheel[] Wheels
        {
            get { return r_Wheels; }
        }

        internal object EnergyType
        {
            get { return r_EnergyType; }
        }

        internal string SerialNumber
        {
            get { return r_SerialNumber; }
        }

        internal string ModelName
        {
            get { return m_ModelName; }
            set { m_ModelName = value; }
        }

        internal float PercentagesOfEnergyRemaining
        {
            get { return m_PercentagesOfEnergyRemaining; }
            set { m_PercentagesOfEnergyRemaining = value; }
        }
    }
}