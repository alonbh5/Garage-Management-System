using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class Electric
    {
        private readonly float r_MaxHoursInBattery;
        private float m_HoursLeftInBattery;

        internal Electric(float i_MaxHours)
        {
            r_MaxHoursInBattery = i_MaxHours;
            m_HoursLeftInBattery = 0f;
        }

        internal float MaxHoursInBattery
        {
            get { return r_MaxHoursInBattery; }
        }

        internal float HoursLeftInBattery
        {
            get { return m_HoursLeftInBattery; }
            set { m_HoursLeftInBattery = value; }
        }

        public bool ChargeBattery(float io_HoursToAdd)
        {
            return true;
        }

        public override string ToString()
        {
            StringBuilder msg = new StringBuilder();

            msg.Append(string.Format("The battay has {0} hours left out of {1} hours{2}", HoursLeftInBattery, MaxHoursInBattery, Environment.NewLine));


            return msg.ToString();
        }
    }
}
