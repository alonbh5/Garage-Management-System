using System;
using System.Text;

namespace Ex03.GarageLogic
{
    public enum eColors
    {
        Black = 1,
        White,
        Gray,
        Silver
    }

    public enum eDoors
    {
        Two = 1,
        Three,
        Four,
        Five
    }

    internal class Car : Vehicle
    {
        private eColors m_Color;
        private eDoors m_Doors;
        
        internal Car(string i_LicenseNumber, uint i_NumOfWheels, float io_MaxAirPressure, object i_EnergyType) :
        base(i_LicenseNumber, i_NumOfWheels, io_MaxAirPressure, i_EnergyType)
        {
        }

        internal eColors Color
        {
            get { return m_Color; }
            set
            {
                if (Enum.IsDefined(typeof(eColors), value))
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

        internal static string GetDoorsOptions()
        {
            // int index = 1;
            StringBuilder doors = new StringBuilder();

            foreach (eDoors door in Enum.GetValues(typeof(eDoors)))
            {
                doors.Append(string.Format("{0}. {1}{2}", (int)door, door.ToString(), Environment.NewLine));
            }

            return doors.ToString();
        }


        internal static string GetColorsOptions()
        {
            // int index = 1;
            StringBuilder colors = new StringBuilder();

            foreach (eColors color in Enum.GetValues(typeof(eColors)))
            {
                colors.Append(string.Format("{0}. {1}{2}", (int)color, color.ToString(), Environment.NewLine));
            }

            return colors.ToString();
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