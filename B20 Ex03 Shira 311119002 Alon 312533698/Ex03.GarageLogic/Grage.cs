using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ex03.GarageLogic
{
    class Grage
    {
        readonly Dictionary<string, Customer> r_Vehicles = new Dictionary<string, Customer>();
        
        public bool AddNewVehicle(string io_CustomerName, string io_CustomerPhoneNumber, eSupportVehicles io_Choice, string io_SerialNumber)
        {
            bool isAdded = false;

            if (r_Vehicles.ContainsKey(io_SerialNumber))
            {
                r_Vehicles[io_SerialNumber].VehicleStatus = eServiceStatus.InRepair;
            }
            else
            {
                Vehicle newVehicle = VehicleObjectCreator.CreateVehicle(io_Choice, io_SerialNumber);
                Customer newCustomer = new Customer(io_CustomerName, io_CustomerPhoneNumber, eServiceStatus.InRepair, newVehicle);
                r_Vehicles.Add(io_SerialNumber, newCustomer);
                isAdded = true;
            }

            return isAdded;
        }          
    }
}