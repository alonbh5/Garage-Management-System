using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class Wheel
    {
        string m_Manufacturer;
        float m_CurrentAirPressure;
        readonly float r_MaxAirPressure;

        internal Wheel (float i_MaxAirPressure)
        {
            r_MaxAirPressure = i_MaxAirPressure;
        }

        public float CurrentAirPressure
        {
            get { return m_CurrentAirPressure; }
        }

        public float MaxAirPressure
        {
            get { return r_MaxAirPressure; }
        }

        public void InflatingAirPressure(float io_AddPressure)
        {

        }
    }
}
