using System;

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
            set
            {
                if (value != string.Empty)
                {
                    m_Manufacturer = value;
                }
                else
                {
                    throw new FormatException("Manufacturer's name input is invalid.");
                }
            }
        }

        public float CurrentAirPressure
        {
            get { return m_CurrentAirPressure; }
            set
            {
                if (value >= 0 && value <= MaxAirPressure)
                {
                    m_CurrentAirPressure = value;
                }
                else
                {
                    Exception ex = new Exception("Amount of air pressure's input is invalid.");
                    throw new ValueOutOfRangeException(ex, MaxAirPressure, 0f);
                }
            }
        }

        public void InflatingAirPressure(float io_AddPressure)
        {
            if (io_AddPressure + CurrentAirPressure <= MaxAirPressure)
            {
                CurrentAirPressure += io_AddPressure;
            }
            else
            {
                Exception ex = new Exception("Amount of air pressure's input is invalid.");
                throw new ValueOutOfRangeException(ex, MaxAirPressure - CurrentAirPressure, 0f);
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