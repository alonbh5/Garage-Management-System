using System;
using System.Text;

namespace Ex03.GarageLogic
{
    public enum eServiceStatuses
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
        private eServiceStatuses m_VehicleStatus;

        internal Customer(string i_Name, string i_PhoneNumber, eServiceStatuses i_VehicleStatus, Vehicle i_Vehicle)
        {
            r_Name = i_Name;
            r_PhoneNumber = i_PhoneNumber;
            m_VehicleStatus = i_VehicleStatus;
            r_Vehicle = i_Vehicle;
        }

        internal string Name
        {
            get { return r_Name; }
        }

        internal string PhoneNumber
        {
            get { return r_PhoneNumber; }
        }

        internal eServiceStatuses VehicleStatus
        {
            get { return m_VehicleStatus; }
            set 
            {
                if (Enum.IsDefined(typeof(eServiceStatuses), value))
                {
                    m_VehicleStatus = value;
                }
                else
                {                    
                    Exception ex = new Exception("Vehicle status's input is invalid");
                    throw new ValueOutOfRangeException(ex, (float)Enum.GetValues(typeof(eServiceStatuses)).Length, 1f);
                }
            }
        }

        internal Vehicle Vehicle
        {
            get { return r_Vehicle; }
        }

        internal static string ShowServiceStatuses()
        {
            StringBuilder serviceStatuses = new StringBuilder();

            foreach (eServiceStatuses serviceStatus in Enum.GetValues(typeof(eServiceStatuses)))
            {
                serviceStatuses.Append(string.Format("{0}. {1}{2}", (int)serviceStatus, serviceStatus.ToString(), Environment.NewLine));
            }

            return serviceStatuses.ToString();
        }

        public override string ToString()
        {
            string customer = string.Format(
                "The owner is {0} and his phone number is {1}{2}{3}Vehicle Status is: {4}",
                Name,
                PhoneNumber,
                Environment.NewLine,
                Vehicle.ToString(),
                VehicleStatus.ToString());

            return customer;
        }
    }
}