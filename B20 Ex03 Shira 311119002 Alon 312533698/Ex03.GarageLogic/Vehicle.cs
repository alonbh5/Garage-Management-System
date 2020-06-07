using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class Vehicle
    {
        private readonly string r_LicenseNumber;
        private readonly Wheel[] r_Wheels;
        private readonly object r_EnergyType;
        private string m_ModelName;
        private float m_PercentagesOfEnergyRemaining;

        protected Vehicle(string i_LicenseNumber, uint i_NumOfWheels, float io_MaxAirPressure, object i_EnergyType) 
        {
            m_PercentagesOfEnergyRemaining = 0f;
            m_ModelName = string.Empty;
            r_EnergyType = i_EnergyType;
            r_LicenseNumber = i_LicenseNumber;
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

        internal string LicenseNumber
        {
            get { return r_LicenseNumber; }
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

        

        public override string ToString()
        {
            StringBuilder vehicle = new StringBuilder();
            int index = 1;

            vehicle.Append(string.Format(
                "License Number is: {0}{1}Model Name is: {2}{1}Wheels info: {1}", 
                LicenseNumber,
                Environment.NewLine,
                ModelName));

            foreach (Wheel currentWheel in Wheels)
            {
                vehicle.Append(string.Format("Wheel #{0}. {1}", index++, currentWheel.ToString()));
            }

            vehicle.Append(string.Format("Enregy is ({0})% full. {1}", PercentagesOfEnergyRemaining, r_EnergyType.ToString()));

            return vehicle.ToString();
        }
    }
}