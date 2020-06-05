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
        internal static Vehicle CreateVehicle(eSupportVehicles i_Choice, string io_SerialNumber)
        {
            Vehicle res = null;

            switch (i_Choice)
            {
                case eSupportVehicles.ElectricMotorcycle:
                    {
                        res = CreateElectricMotorcycle(io_SerialNumber);
                        break;
                    }

                case eSupportVehicles.ElectricCar:
                    {
                        res = CreateElectricCar(io_SerialNumber);
                        break;
                    }

                case eSupportVehicles.Truck:
                    {
                        res = CreateTruck(io_SerialNumber);
                        break;
                    }

                case eSupportVehicles.RegularMotorcycle:
                    {
                        res = CreateRrgularMotorcycle(io_SerialNumber);
                        break;
                    }

                case eSupportVehicles.RegularCar:
                    {
                        res = CreateRrgularCar(io_SerialNumber);
                        break;
                    }
            }

            return res;
        }

        private static Vehicle CreateElectricMotorcycle(string io_SerialNumber)
        {
            Electric engine = new Electric(1.2f);
            return new Motorcycle(io_SerialNumber, 2, 30, engine);
        }

        private static Vehicle CreateRrgularMotorcycle(string io_SerialNumber)
        {
            Fuel engine = new Fuel(eFuelType.Octan95, 7);
            return new Motorcycle(io_SerialNumber, 2, 30, engine);
        }

        private static Vehicle CreateElectricCar(string io_SerialNumber)
        {
            Electric engine = new Electric(2.1f);
            return new Car(io_SerialNumber, 4, 32, engine);
        }

        private static Vehicle CreateRrgularCar(string io_SerialNumber)
        {
            Fuel engine = new Fuel(eFuelType.Octan96, 60);
            return new Car(io_SerialNumber, 4, 32, engine);
        }

        private static Vehicle CreateTruck(string io_SerialNumber)
        {
            Fuel engine = new Fuel(eFuelType.Soler, 120);
            return new Truck(io_SerialNumber, 16, 28, engine);
        }
    }
}