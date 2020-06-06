using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class Wheel
    {
        private readonly float r_MaxAirPressure;
        private string m_Manufacturer;
        private float m_CurrentAirPressure;

        internal Wheel(float i_MaxAirPressure)
        {
            r_MaxAirPressure = i_MaxAirPressure;
        }

        public float MaxAirPressure
        {
            get { return r_MaxAirPressure; }
        }

        public string Manufacturer
        {
            get { return m_Manufacturer; }
            set { m_Manufacturer = value; }
        }

        public float CurrentAirPressure
        {
            get { return m_CurrentAirPressure; }
            set { m_CurrentAirPressure = value; }
        }

        public void InflatingAirPressure(float io_AddPressure)
        {
            if (io_AddPressure + CurrentAirPressure <= MaxAirPressure)
            {
                CurrentAirPressure += io_AddPressure;
            }
            else
            {
                throw new ValueOutOfRangeException((MaxAirPressure - CurrentAirPressure), 0f);
            }
        }

        public override string ToString()
        {
            string wheel = string.Format(
                "Manufacturer is {0}, current air pressure is {1} out of {2}{3}",
                Manufacturer,
                CurrentAirPressure,
                MaxAirPressure,
                Environment.NewLine);

            return wheel;
        }
    }
}