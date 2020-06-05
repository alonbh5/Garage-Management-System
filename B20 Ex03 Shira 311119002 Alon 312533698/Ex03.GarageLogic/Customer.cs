using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public enum eServiceStatus
    {
        InRepair = 1,
        Fixed,
        Paid
    }

    internal class Customer
    {
        private readonly string r_Name;
        private readonly string r_PhoneNumber;
        private readonly Vehicle r_Vehicle;
        private eServiceStatus m_VehicleStatus;

        internal Customer(string i_Name, string i_PhoneNumber, eServiceStatus i_VehicleStatus, Vehicle i_Vehicle)
        {
            r_Name = i_Name;
            r_PhoneNumber = i_PhoneNumber;
            m_VehicleStatus = i_VehicleStatus;
            r_Vehicle = i_Vehicle;
        }

        public string Name
        {
            get { return r_Name; }
        }

        public string PhoneNumber
        {
            get { return r_PhoneNumber; }
        }

        public eServiceStatus VehicleStatus
        {
            get { return m_VehicleStatus; }
            set { m_VehicleStatus = value; }
        }

        public Vehicle Vehicle
        {
            get { return r_Vehicle; }
        }

        public override string ToString()
        {
            string customer = string.Format(
                "The owner is {0} and his phone number is {1}{2}{3}{4}",
                Name,
                PhoneNumber,
                Environment.NewLine,
                Vehicle.ToString(),
                VehicleStatus.ToString());

            return customer;
        }
    }
}