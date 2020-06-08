using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class Truck : Vehicle
    {
        private bool m_HazardousMaterials;
        private float m_CargoVolume;

        internal Truck(string i_LicenseNumber, uint i_NumOfWheels, float io_MaxAirPressure, object i_EnergyType) :
        base(i_LicenseNumber, i_NumOfWheels, io_MaxAirPressure, i_EnergyType)
        {
        }

        internal bool HazardousMaterials
        {
            get { return m_HazardousMaterials; }
            set { m_HazardousMaterials = value; }
        }

        internal float CargoVolume
        {
            get { return m_CargoVolume; }
            set
            {
                if (value >= 0)
                {
                    m_CargoVolume = value;
                }
                else
                {
                    throw new ArgumentException("Cargo volume's input is invalid");
                }
            }
        }

        public override string ToString()
        {
            string hazardousMsg = string.Empty;

            if (HazardousMaterials)
            {
                hazardousMsg = "does";
            }
            else
            {
                hazardousMsg = "does not";
            }

            string truck = string.Format(
                "{0}The truck {1} contains hazardous materials and his cargo capacity is {2}{3}",
                base.ToString(),
                hazardousMsg,
                CargoVolume,
                Environment.NewLine);

            return truck;
        }
    }
}