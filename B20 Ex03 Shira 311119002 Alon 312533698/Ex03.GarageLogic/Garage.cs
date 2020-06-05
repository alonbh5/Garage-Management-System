using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private readonly Dictionary<string, Customer> r_Vehicles = new Dictionary<string, Customer>();

        public bool IsExist(string io_LicenseNumber)
        {
            return r_Vehicles.ContainsKey(io_LicenseNumber);
        }

        public int NumOfSupportedVehicles()
        {
            return SupportedVehicles.sr_SupportedVehicles.Length;
        }

        public bool AddNewVehicle(string io_CustomerName, string io_CustomerPhoneNumber, eSupportVehicles io_Choice, string io_LicenseNumber)
        { // function 1
            bool isAdded = false;

            if (r_Vehicles.ContainsKey(io_LicenseNumber))
            {
                r_Vehicles[io_LicenseNumber].VehicleStatus = eServiceStatus.InRepair;
            }
            else
            {
                Vehicle newVehicle = SupportedVehicles.CreateVehicle(io_Choice, io_LicenseNumber);
                Customer newCustomer = new Customer(io_CustomerName, io_CustomerPhoneNumber, eServiceStatus.InRepair, newVehicle);
                r_Vehicles.Add(io_LicenseNumber, newCustomer);
                isAdded = true;
            }

            return isAdded;
        }

        public StringBuilder VehicleStringByFilter(bool i_InRepair, bool i_Fixed, bool i_Paid)
        { // function 2
            int index = 1;
            StringBuilder vehicleList = new StringBuilder();

            foreach (KeyValuePair<string, Customer> entry in r_Vehicles)
            {
                if (i_InRepair && entry.Value.VehicleStatus.Equals(eServiceStatus.InRepair))
                {
                    vehicleList.Append(string.Format("{0}. {1}{2}", index++, entry.Key, Environment.NewLine));
                }

                if (i_Fixed && entry.Value.VehicleStatus.Equals(eServiceStatus.Fixed))
                {                
                    vehicleList.Append(string.Format("{0}. {1}{2}", index++, entry.Key, Environment.NewLine));
                }

                if (i_Paid && entry.Value.VehicleStatus.Equals(eServiceStatus.Paid))
                {
                    vehicleList.Append(string.Format("{0}. {1}{2}", index++, entry.Key, Environment.NewLine));
                }
            }

            return vehicleList;
        }

        public bool ChangeServiceStatus(string i_LicenseNumber, eServiceStatus i_NewStatus)
        { // function 3
            bool isChanged = false;

            if (r_Vehicles.ContainsKey(i_LicenseNumber)) 
            {
                r_Vehicles[i_LicenseNumber].VehicleStatus = i_NewStatus;
                isChanged = true;
            }

            return isChanged;
        }

        public bool InflateWheels(string i_LicenseNumber)
        { // function 4
            bool isInflated = false;
            float amountToAdd = 0f;

            if (r_Vehicles.ContainsKey(i_LicenseNumber))
            {
                Wheel[] currentWheels = r_Vehicles[i_LicenseNumber].Vehicle.Wheels;

                for (int i = 0; i <= currentWheels.Length; i++) 
                {
                    amountToAdd = currentWheels[i].MaxAirPressure - currentWheels[i].CurrentAirPressure;
                    currentWheels[i].InflatingAirPressure(amountToAdd);
                }

                isInflated = true;
            }

            return isInflated;
        }

        public bool FillGasTank(string i_LicenseNumber, eFuelType io_FuelType, float io_AmountToAdd)
        { // function 5
            bool isFilled = false;

            if (r_Vehicles.ContainsKey(i_LicenseNumber))
            {
                if (r_Vehicles[i_LicenseNumber].Vehicle.EnergyType is Fuel)
                {
                    isFilled = (r_Vehicles[i_LicenseNumber].Vehicle.EnergyType as Fuel).FillTank(io_AmountToAdd, io_FuelType);
                }
            }

            return isFilled;
        }

        public bool FillCharge(string i_LicenseNumber, float io_MinutesToAdd)
        { // function 6
            bool isFilled = false;

            if (r_Vehicles.ContainsKey(i_LicenseNumber))
            {
                if (r_Vehicles[i_LicenseNumber].Vehicle.EnergyType is Electric)
                {
                    isFilled = (r_Vehicles[i_LicenseNumber].Vehicle.EnergyType as Electric).ChargeBattery(io_MinutesToAdd / 60f);
                }
            }

            return isFilled;
        }

        public bool VehicleInfo(string i_LicenseNumber, out string io_VehicleInfo)
        { // function 7
            bool found = false;

            StringBuilder vehicleInfo = new StringBuilder();

            if (r_Vehicles.ContainsKey(i_LicenseNumber))
            {
                vehicleInfo.Append(r_Vehicles[i_LicenseNumber].ToString());
                found = true;
            }

            io_VehicleInfo = vehicleInfo.ToString();
            return found;
        }

        public string ShowSupportedVehicles()
        {
            int index = 1;
            StringBuilder supportedVehicle = new StringBuilder();

            foreach (string currentVehicle in SupportedVehicles.sr_SupportedVehicles)
            {
                supportedVehicle.Append(string.Format("{0}. {1}{2}", index++, currentVehicle, Environment.NewLine));
            }

            return supportedVehicle.ToString();
        }

        internal bool UpdateVehicleInfo(string i_LicenseNumber)
        {

        }
    }
}