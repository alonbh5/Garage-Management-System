using System;
using System.Text;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal enum eSupportVehicles
    {
        RegularMotorcycle = 1,
        ElectricMotorcycle,
        RegularCar,
        ElectricCar,
        Truck
    }

    public enum eQuestions
    {
        Doors,
        Color,
        ModelName,
        CurrentFuel,
        CurrentHours,
        HazardousMaterials,
        CargoCpacity,
        WheelManufacturer,
        CurentWheelAirPressure,
        LicenseType,
        EngineCC
    }

    //// This class is the only one that will be updated in case of added garage information.
    //// It conatines dynamic information about:
    ////     1. Vehicles that the garage is currently supports.
    ////     2. Sort of information needed for the supported vehicles (in the from of enum "eQuestions").
    
    public class SupportedVehicles
    {        
        private static readonly string[] sr_ArrOfSupportedVehicles = new string[] { "Regular Motorycle", "Electric Motorcycle", "Regular Car", "Electric Car", "Truck" }; 

        internal static string[] ArrOfSupportedVehicles
        {
            //// Returns a string of Supported Vehicles by enum index
            
            get { return sr_ArrOfSupportedVehicles; }
        }

        internal static Vehicle CreateVehicle(eSupportVehicles i_VehiclesChoice, string io_LicenseNumber)
        {
            //// Gets license number of vehicle and choice of vehicle type out of supported vehicles.
            //// Create and return new vehicle according to supported vehicles fixed criterias.
            
            Vehicle newVehicle = null;

            switch (i_VehiclesChoice)
            {
                case eSupportVehicles.ElectricMotorcycle:
                    {
                        newVehicle = createElectricMotorcycle(io_LicenseNumber);
                        break;
                    }

                case eSupportVehicles.ElectricCar:
                    {
                        newVehicle = createElectricCar(io_LicenseNumber);
                        break;
                    }

                case eSupportVehicles.Truck:
                    {
                        newVehicle = createTruck(io_LicenseNumber);
                        break;
                    }

                case eSupportVehicles.RegularMotorcycle:
                    {
                        newVehicle = createRrgularMotorcycle(io_LicenseNumber);
                        break;
                    }

                case eSupportVehicles.RegularCar:
                    {
                        newVehicle = createRrgularCar(io_LicenseNumber);
                        break;
                    }
            }

            return newVehicle;
        }

        private static Vehicle createElectricMotorcycle(string io_LicenseNumber)
        {
            Electric engine = new Electric(1.2f);
            return new Motorcycle(io_LicenseNumber, 2, 30, engine);
        }

        private static Vehicle createRrgularMotorcycle(string io_LicenseNumber)
        {
            Fuel engine = new Fuel(eFuelTypes.Octan95, 7f);
            return new Motorcycle(io_LicenseNumber, 2, 30, engine);
        }

        private static Vehicle createElectricCar(string io_LicenseNumber)
        {
            Electric engine = new Electric(2.1f);
            return new Car(io_LicenseNumber, 4, 32, engine);
        }

        private static Vehicle createRrgularCar(string io_LicenseNumber)
        {
            Fuel engine = new Fuel(eFuelTypes.Octan96, 60f);
            return new Car(io_LicenseNumber, 4, 32, engine);
        }

        private static Vehicle createTruck(string io_LicenseNumber)
        {
            Fuel engine = new Fuel(eFuelTypes.Soler, 120f);
            return new Truck(io_LicenseNumber, 16, 28, engine);
        }

        internal static void GetInfo(out Dictionary<eQuestions, object> io_DicToFill, int i_Choice)
        {
            //// Gets vehicle choice out of supported vehicles and a dictionary.
            //// Flll the dictionary keys with all the eQusetions (Enum) needed for this vehicle choice.
            
            io_DicToFill = new Dictionary<eQuestions, object>();

            if (i_Choice > 0 && i_Choice <= ArrOfSupportedVehicles.Length)
            {
                eSupportVehicles currentVehicle = (eSupportVehicles)i_Choice;

                io_DicToFill.Add(eQuestions.WheelManufacturer, string.Empty);
                io_DicToFill.Add(eQuestions.CurentWheelAirPressure, string.Empty);
                io_DicToFill.Add(eQuestions.ModelName, string.Empty);

                if (currentVehicle.Equals(eSupportVehicles.ElectricMotorcycle) || currentVehicle.Equals(eSupportVehicles.ElectricCar))
                { // Hours left in elecrtic engine
                    io_DicToFill.Add(eQuestions.CurrentHours, string.Empty);
                }
                else
                { // Fuel left
                    io_DicToFill.Add(eQuestions.CurrentFuel, string.Empty);
                }

                if (currentVehicle.Equals(eSupportVehicles.RegularMotorcycle) || currentVehicle.Equals(eSupportVehicles.ElectricMotorcycle))
                { // Case of motorcycle
                    io_DicToFill.Add(eQuestions.LicenseType, string.Empty);
                    io_DicToFill.Add(eQuestions.EngineCC, string.Empty);
                }

                if (currentVehicle.Equals(eSupportVehicles.ElectricCar) || currentVehicle.Equals(eSupportVehicles.RegularCar))
                { // Case of car  
                    io_DicToFill.Add(eQuestions.Color, string.Empty);
                    io_DicToFill.Add(eQuestions.Doors, string.Empty);
                }

                if (currentVehicle.Equals(eSupportVehicles.Truck))
                { // Case of truck  
                    io_DicToFill.Add(eQuestions.HazardousMaterials, false);
                    io_DicToFill.Add(eQuestions.CargoCpacity, string.Empty);
                }
            }
        }

        internal static void FillVehicleInfo(Vehicle io_Vehicle, Dictionary<eQuestions, object> i_FilledDictionary)
        {
            //// Gets a vehicle X and a filled dictionary.
            //// Fill the blanks in Vehicle X info by the information filled in dictionary.
            //// Throw exceptions with information about each mismatch between information needed and information given.
            
            if (i_FilledDictionary.ContainsKey(eQuestions.ModelName))
            {
                io_Vehicle.ModelName = i_FilledDictionary[eQuestions.ModelName].ToString();
            }

            if (i_FilledDictionary.ContainsKey(eQuestions.CurrentFuel))
            {
                if (float.TryParse(i_FilledDictionary[eQuestions.CurrentFuel].ToString(), out float currFuel))
                {
                    Fuel engine = io_Vehicle.EnergyType as Fuel;
                    engine.CurrentFuelTank = currFuel;
                    float percent = engine.CurrentFuelTank / engine.MaxTank;
                    io_Vehicle.PercentagesOfEnergyRemaining = percent * 100f;
                }
                else
                {
                    throw new FormatException("Amount of fuel is invalid input");
                }
            }

            if (i_FilledDictionary.ContainsKey(eQuestions.CurrentHours))
            {
                if (float.TryParse(i_FilledDictionary[eQuestions.CurrentHours].ToString(), out float currMinutes))
                {
                    Electric engine = io_Vehicle.EnergyType as Electric;
                    engine.HoursLeftInBattery = currMinutes / 60f;
                    float percent = engine.HoursLeftInBattery / engine.MaxHoursInBattery;
                    io_Vehicle.PercentagesOfEnergyRemaining = percent * 100f;
                }
                else
                {
                    throw new FormatException("Amount of hours is invalid input");
                }
            }

            if (i_FilledDictionary.ContainsKey(eQuestions.CurentWheelAirPressure))
            {
                if (float.TryParse(i_FilledDictionary[eQuestions.CurentWheelAirPressure].ToString(), out float curAirPressure))
                {
                    for (int i = 0; i < io_Vehicle.Wheels.Length; i++)
                    {
                        io_Vehicle.Wheels[i].CurrentAirPressure = curAirPressure;
                    }
                }
                else
                {
                    throw new FormatException("Amount of wheel's air pressure is invalid input");
                }
            }

            if (i_FilledDictionary.ContainsKey(eQuestions.WheelManufacturer))
            {
                for (int i = 0; i < io_Vehicle.Wheels.Length; i++)
                {
                    io_Vehicle.Wheels[i].Manufacturer = i_FilledDictionary[eQuestions.WheelManufacturer].ToString();
                }
            }

            if (i_FilledDictionary.ContainsKey(eQuestions.Doors))
            {
                if (int.TryParse(i_FilledDictionary[eQuestions.Doors].ToString(), out int doors))
                {
                    (io_Vehicle as Car).Doors = (eDoors)doors;
                }
                else
                {
                    throw new FormatException("Choice of doors input is invalid");
                }
            }

            if (i_FilledDictionary.ContainsKey(eQuestions.LicenseType))
            {
                if (int.TryParse(i_FilledDictionary[eQuestions.LicenseType].ToString(), out int licenseType))
                {
                    (io_Vehicle as Motorcycle).LicenseType = (eLicenseTypes)licenseType;
                }
                else
                {
                    throw new FormatException("Choice of license type is invalid");
                }
            }

            if (i_FilledDictionary.ContainsKey(eQuestions.Color))
            {
                if (int.TryParse(i_FilledDictionary[eQuestions.Color].ToString(), out int color))
                {
                    (io_Vehicle as Car).Color = (eColors)color;
                }
                else
                {
                    throw new FormatException("Choice of color is invalid");
                }
            }

            if (i_FilledDictionary.ContainsKey(eQuestions.HazardousMaterials))
            {
                (io_Vehicle as Truck).HazardousMaterials = (bool)i_FilledDictionary[eQuestions.HazardousMaterials];
            }

            if (i_FilledDictionary.ContainsKey(eQuestions.CargoCpacity))
            {
                if (float.TryParse(i_FilledDictionary[eQuestions.CargoCpacity].ToString(), out float cargo))
                {
                    (io_Vehicle as Truck).CargoVolume = cargo;
                }
                else
                {
                    throw new FormatException("Cargo capacity's input is invalid");
                }
            }

            if (i_FilledDictionary.ContainsKey(eQuestions.EngineCC))
            {
                if (int.TryParse(i_FilledDictionary[eQuestions.EngineCC].ToString(), out int engineCC))
                {
                    (io_Vehicle as Motorcycle).EngineCapacityInCC = engineCC;
                }
                else
                {
                    throw new FormatException("Engine CC's input is invalid");
                }
            }
        }

        internal static string ShowSupportedVehiclesTypes()
        {
            int index = 1;
            StringBuilder supportedVehicles = new StringBuilder();

            foreach (string currentVehicle in ArrOfSupportedVehicles)
            {
                supportedVehicles.Append(string.Format("{0}. {1}{2}", index++, currentVehicle, Environment.NewLine));
            }

            return supportedVehicles.ToString();
        }
    }
}