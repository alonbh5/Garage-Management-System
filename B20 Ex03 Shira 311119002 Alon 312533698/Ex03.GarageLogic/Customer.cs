using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{

    public enum eServiceStatus
    {
        InRepair,
        Fixed,
        Paid
    }

    internal class Customer
    {
        readonly string r_Name;
        readonly string r_PhoneNumber;
        eServiceStatus m_VehicleStatus;
        readonly Vehicle r_Vehicle;

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
    }
}