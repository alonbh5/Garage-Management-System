using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class Electric
    {
        float m_HoursLeftInBattery;
        readonly float r_MaxHoursInBattery;

        internal Electric(float i_MaxHours)
        {
            r_MaxHoursInBattery = i_MaxHours;
            m_HoursLeftInBattery = 0f;
        }
            

        public bool ChargeBattery (float io_HoursToAdd)
        {
            return true;
        }
    }
}
