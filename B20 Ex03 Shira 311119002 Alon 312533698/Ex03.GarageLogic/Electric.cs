using System;

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
            set
            {
                if (value >= 0 && value <= MaxHoursInBattery)
                {
                    m_HoursLeftInBattery = value;
                }
                else
                {
                    Exception ex = new Exception("Amount of hours's input is invalid");
                    throw new ValueOutOfRangeException(ex, MaxHoursInBattery, 0f);
                }
            }
        }

        internal bool ChargeBattery(float io_HoursToAdd)
        {
            bool charged = false;

            if (io_HoursToAdd + HoursLeftInBattery <= MaxHoursInBattery)
            {
                HoursLeftInBattery += io_HoursToAdd;
                charged = true;
            }
            else
            {
                Exception ex = new Exception("Amount of hours to charge is invalid");
                throw new ValueOutOfRangeException(ex, (MaxHoursInBattery - HoursLeftInBattery) * 60f, 0f);
            }

            return charged;
        }

        public override string ToString()
        {
            string electric = string.Format(
                "The battay has {0} hours left out of {1} hours{2}",
                HoursLeftInBattery,
                MaxHoursInBattery,
                Environment.NewLine);

            return electric;
        }
    }
}