using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public enum eSupportVehicles
    {
        RegularMotorcycle = 1,
        ElectricMotorcycle,
        RegularCar,
        ElectricCar,
        Truck
    }



    public class SupportedVehicles
    {
        public static readonly string[] sr_SupportedVehicles = new string[] { "Regular Motorycle", "Electric Motorcycle", "Regular Car", "Electric Car", "Truck" };

        internal static Vehicle CreateVehicle(eSupportVehicles i_Choice, string io_LicenseNumber)
        {
            Vehicle res = null;

            switch (i_Choice)
            {
                case eSupportVehicles.ElectricMotorcycle:
                    {
                        res = CreateElectricMotorcycle(io_LicenseNumber);
                        break;
                    }

                case eSupportVehicles.ElectricCar:
                    {
                        res = CreateElectricCar(io_LicenseNumber);
                        break;
                    }

                case eSupportVehicles.Truck:
                    {
                        res = CreateTruck(io_LicenseNumber);
                        break;
                    }

                case eSupportVehicles.RegularMotorcycle:
                    {
                        res = CreateRrgularMotorcycle(io_LicenseNumber);
                        break;
                    }

                case eSupportVehicles.RegularCar:
                    {
                        res = CreateRrgularCar(io_LicenseNumber);
                        break;
                    }
            }

            return res;
        }

        private static Vehicle CreateElectricMotorcycle(string io_LicenseNumber)
        {
            Electric engine = new Electric(1.2f);
            return new Motorcycle(io_LicenseNumber, 2, 30, engine);
        }

        private static Vehicle CreateRrgularMotorcycle(string io_LicenseNumber)
        {
            Fuel engine = new Fuel(eFuelType.Octan95, 7);
            return new Motorcycle(io_LicenseNumber, 2, 30, engine);
        }

        private static Vehicle CreateElectricCar(string io_LicenseNumber)
        {
            Electric engine = new Electric(2.1f);
            return new Car(io_LicenseNumber, 4, 32, engine);
        }

        private static Vehicle CreateRrgularCar(string io_LicenseNumber)
        {
            Fuel engine = new Fuel(eFuelType.Octan96, 60);
            return new Car(io_LicenseNumber, 4, 32, engine);
        }

        private static Vehicle CreateTruck(string io_LicenseNumber)
        {
            Fuel engine = new Fuel(eFuelType.Soler, 120);
            return new Truck(io_LicenseNumber, 16, 28, engine);
        }
    }
}