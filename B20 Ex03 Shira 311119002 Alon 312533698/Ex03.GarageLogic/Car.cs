using System;
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
            set { m_Color = value; }
        }

        internal eDoors Doors
        {
            get { return m_Doors; }
            set { m_Doors = value; }
        }

        public override string ToString()
        {
            StringBuilder msg = new StringBuilder();
            
            msg.Append(string.Format("{0} The car is {1} and has {2} doors {3}", base.ToString(), Color.ToString(),Doors.ToString(), Environment.NewLine));
            
            return msg.ToString();
        }
    }
}
