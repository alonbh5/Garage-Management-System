using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public enum eColor
    {
        Black,
        White,
        Gray,
        Silver
    }

    public enum eDoors
    {
        Two = 2,
        Three,
        Four,
        Five
    }

    internal class Car : Vehicle
    {
        private eColor m_Color;
        private eDoors m_Doors;

        internal Car(string i_LicenseNumber, uint i_NumOfWheels, float io_MaxAirPressure, object i_EnergyType) :
        base(i_LicenseNumber, i_NumOfWheels, io_MaxAirPressure, i_EnergyType)
        {
        }

        internal eColor Color
        {
            get { return m_Color; }
            set
            {
                if (Enum.IsDefined(typeof(eColor), value))
                {
                    m_Color = value;
                }
                else
                {
                    Exception ex = new Exception("Color input is invalid");
                    throw new ValueOutOfRangeException(ex, 3f, 0f);
                }
            }
        }

        internal eDoors Doors
        {
            get { return m_Doors; }
            set
            {
                if (Enum.IsDefined(typeof(eDoors), value))
                {
                    m_Doors = value;
                }
                else
                {
                    Exception ex = new Exception("Doors number's input is invalid");
                    throw new ValueOutOfRangeException(ex, 5f, 2f);
                }
            }
        }

        public override string ToString()
        {
            string car = string.Format(
                "{0}The car is {1} and has {2} doors{3}",
                base.ToString(),
                Color.ToString(),
                Doors.ToString(),
                Environment.NewLine);

            return car;
        }
    }
}