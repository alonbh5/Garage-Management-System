using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
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

    public class Garage
    {
        private readonly Dictionary<string, Customer> r_Vehicles = new Dictionary<string, Customer>();           

        public bool AddNewVehicle(string io_CustomerName, string io_CustomerPhoneNumber, int io_Choice, string io_LicenseNumber)
        { // function 1
            bool isAdded = false;

            if (IsExist(io_LicenseNumber))
            {
                try
                {
                    r_Vehicles[io_LicenseNumber].VehicleStatus = eServiceStatus.InRepair;
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("License Number {0} is not the system", io_LicenseNumber), ex);
                }
            }
            else
            {
                if (Enum.IsDefined(typeof(eSupportVehicles), io_Choice))
                {
                    Vehicle newVehicle = SupportedVehicles.CreateVehicle((eSupportVehicles)io_Choice, io_LicenseNumber);
                    Customer newCustomer = new Customer(io_CustomerName, io_CustomerPhoneNumber, eServiceStatus.InRepair, newVehicle);
                    r_Vehicles.Add(io_LicenseNumber, newCustomer);
                    isAdded = true;
                }
                else
                {
                    throw new ValueOutOfRangeException((float)NumOfSupportedVehicles(), 1f);
                }
            }

            return isAdded;
        }

        public string VehicleStringByFilter(bool i_InRepair, bool i_Fixed, bool i_Paid)
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

            return vehicleList.ToString();
        }

        public bool ChangeServiceStatus(string i_LicenseNumber, int i_NewStatus)
        { // function 3             
            bool isChanged = false;

            if (IsExist(i_LicenseNumber))
            {
                if (Enum.IsDefined(typeof(eServiceStatus), i_NewStatus))
                {
                    try
                    {
                        r_Vehicles[i_LicenseNumber].VehicleStatus = (eServiceStatus)i_NewStatus;
                        isChanged = true;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(string.Format("License Number {0} is not the system", i_LicenseNumber), ex);
                    }
                }
                else
                {
                    throw new ValueOutOfRangeException((float)NumOfSupportedVehicles(), 1f);
                }
            }

            return isChanged;
        }

        public bool InflateWheels(string i_LicenseNumber)
        { // function 4
            bool isInflated = false;
            float amountToAdd = 0f;

            if (IsExist(i_LicenseNumber))
            {
                if (r_Vehicles[i_LicenseNumber].VehicleStatus.Equals(eServiceStatus.InRepair))
                {
                    try
                    {
                        Wheel[] currentWheels = r_Vehicles[i_LicenseNumber].Vehicle.Wheels;

                        for (int i = 0; i < currentWheels.Length; i++)
                        {
                            amountToAdd = currentWheels[i].MaxAirPressure - currentWheels[i].CurrentAirPressure;
                            currentWheels[i].InflatingAirPressure(amountToAdd);
                        }

                        isInflated = true;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(string.Format("License Number {0} is not in the system", i_LicenseNumber), ex);
                    }
                }
                else
                {
                    throw new ArgumentException("The vehicle is not 'In-Repair' status. You can not work on this vehicle.");
                }
            }

            return isInflated;
        }

        public bool FillGasTank(string i_LicenseNumber, int io_FuelType, float io_AmountToAdd)
        { // function 5
            bool isFilled = false;

            if (IsExist(i_LicenseNumber))
            {
                if (r_Vehicles[i_LicenseNumber].VehicleStatus.Equals(eServiceStatus.InRepair))
                {
                    if (Enum.IsDefined(typeof(eFuelType), io_FuelType))
                    {
                        if (r_Vehicles[i_LicenseNumber].Vehicle.EnergyType is Fuel)
                        {
                            isFilled = (r_Vehicles[i_LicenseNumber].Vehicle.EnergyType as Fuel).FillTank(io_AmountToAdd, (eFuelType)io_FuelType);
                            if (isFilled)
                            {
                                try
                                {
                                    float percent = (r_Vehicles[i_LicenseNumber].Vehicle.EnergyType as Fuel).CurrentFuelTank / (r_Vehicles[i_LicenseNumber].Vehicle.EnergyType as Fuel).MaxTank;
                                    r_Vehicles[i_LicenseNumber].Vehicle.PercentagesOfEnergyRemaining = percent * 100f;
                                }
                                catch (Exception ex)
                                {
                                    throw new Exception(string.Format("License Number {0} is not in the system", i_LicenseNumber), ex);
                                }
                            }
                        }
                        else
                        {
                            throw new FormatException("The vehicle engien is not runnig on fuel!");
                        }
                    }
                    else
                    {
                        throw new ValueOutOfRangeException((float)NumOfFuelType(), 1f);
                    }
                }
                else
                {
                    throw new ArgumentException("The vehicle is not 'inrepair' status. You can not work on this vehicle.");
                }
            }

            return isFilled;
        }

        public bool FillCharge(string i_LicenseNumber, float io_MinutesToAdd)
        { // function 6
            bool isFilled = false;

            if (IsExist(i_LicenseNumber))
            {
                if (r_Vehicles[i_LicenseNumber].VehicleStatus.Equals(eServiceStatus.InRepair)) 
                {
                    if (r_Vehicles[i_LicenseNumber].Vehicle.EnergyType is Electric)
                    {
                        isFilled = (r_Vehicles[i_LicenseNumber].Vehicle.EnergyType as Electric).ChargeBattery(io_MinutesToAdd / 60f);
                        if (isFilled)
                        {
                            float percent = (r_Vehicles[i_LicenseNumber].Vehicle.EnergyType as Electric).HoursLeftInBattery / (r_Vehicles[i_LicenseNumber].Vehicle.EnergyType as Electric).MaxHoursInBattery;
                            r_Vehicles[i_LicenseNumber].Vehicle.PercentagesOfEnergyRemaining = percent * 100f;
                        }
                    }
                    else
                    {
                        throw new FormatException("The vehicle engien is not electric!");
                    }
                }
                else
                {
                    throw new ArgumentException("The vehicle is not 'In-Repair' status. You can not work on this vehicle.");
                }
            }

            return isFilled;
        }

        public bool VehicleInfo(string i_LicenseNumber, out string io_VehicleInfo)
        { // function 7
            bool found = false;

            StringBuilder vehicleInfo = new StringBuilder();

            if (IsExist(i_LicenseNumber))
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

        public string GetFuelTypes()
        {
            return Fuel.GetFuelTypes();
        }

        public Dictionary<eQuestions, object> GetExtraInfo(int io_Choice)
        {
            Dictionary<eQuestions, object> DicToFill = new Dictionary<eQuestions, object>();

            if (io_Choice > 0 && io_Choice <= NumOfSupportedVehicles())
            {
                eSupportVehicles currentVehicle = (eSupportVehicles)io_Choice;

                DicToFill.Add(eQuestions.WheelManufacturer, string.Empty);
                DicToFill.Add(eQuestions.CurentWheelAirPressure, string.Empty);
                DicToFill.Add(eQuestions.ModelName, string.Empty);                               

                if (currentVehicle.Equals(eSupportVehicles.ElectricMotorcycle) || currentVehicle.Equals(eSupportVehicles.ElectricCar))
                { // Hours left in elecrtic engien
                    DicToFill.Add(eQuestions.CurrentHours, string.Empty);                    
                }
                else
                { // Fuel left
                    DicToFill.Add(eQuestions.CurrentFuel, string.Empty); 
                }

                if (currentVehicle.Equals(eSupportVehicles.RegularMotorcycle) || currentVehicle.Equals(eSupportVehicles.ElectricMotorcycle))
                { // Case of motorcycle
                    DicToFill.Add(eQuestions.LicenseType, string.Empty);
                    DicToFill.Add(eQuestions.EngineCC, string.Empty);                    
                }

                if (currentVehicle.Equals(eSupportVehicles.ElectricCar) || currentVehicle.Equals(eSupportVehicles.RegularCar))
                { // Case of car  
                    DicToFill.Add(eQuestions.Color, string.Empty);
                    DicToFill.Add(eQuestions.Doors, string.Empty);                    
                }

                if (currentVehicle.Equals(eSupportVehicles.Truck))
                { // Case of truck  
                    DicToFill.Add(eQuestions.HazardousMaterials, false);
                    DicToFill.Add(eQuestions.CargoCpacity, string.Empty);                    
                }
            }

            return DicToFill;
        }

        public void UpdateInfo(Dictionary<eQuestions, object> i_FilledDictionary, string io_LicenseNumber)
        {
            if (i_FilledDictionary.ContainsKey(eQuestions.ModelName))
            {
                r_Vehicles[io_LicenseNumber].Vehicle.ModelName = i_FilledDictionary[eQuestions.ModelName].ToString();
            }

            if (i_FilledDictionary.ContainsKey(eQuestions.CurrentFuel))
            {
                if (float.TryParse(i_FilledDictionary[eQuestions.CurrentFuel].ToString(), out float currFuel))
                {                    
                    (r_Vehicles[io_LicenseNumber].Vehicle.EnergyType as Fuel).CurrentFuelTank = currFuel;
                    float percent = (r_Vehicles[io_LicenseNumber].Vehicle.EnergyType as Fuel).CurrentFuelTank / (r_Vehicles[io_LicenseNumber].Vehicle.EnergyType as Fuel).MaxTank;
                    r_Vehicles[io_LicenseNumber].Vehicle.PercentagesOfEnergyRemaining = percent * 100f;
                }
                else
                {
                    throw new FormatException("Amount of fuel is not a valid input");
                }
            }

            if (i_FilledDictionary.ContainsKey(eQuestions.CurrentHours))
            {
                if (float.TryParse(i_FilledDictionary[eQuestions.CurrentHours].ToString(), out float curMin))
                { 
                    (r_Vehicles[io_LicenseNumber].Vehicle.EnergyType as Electric).HoursLeftInBattery = curMin/60f;
                    float percent = (r_Vehicles[io_LicenseNumber].Vehicle.EnergyType as Electric).HoursLeftInBattery / (r_Vehicles[io_LicenseNumber].Vehicle.EnergyType as Electric).MaxHoursInBattery;
                    r_Vehicles[io_LicenseNumber].Vehicle.PercentagesOfEnergyRemaining = percent * 100f;
                }
                else
                {
                    throw new FormatException("Amount of hours is not a valid input");
                }
            }

            if (i_FilledDictionary.ContainsKey(eQuestions.CurentWheelAirPressure))
            {
                if (float.TryParse(i_FilledDictionary[eQuestions.CurentWheelAirPressure].ToString(), out float curAirPressure))
                {
                    for (int i = 0; i < r_Vehicles[io_LicenseNumber].Vehicle.Wheels.Length; i++) 
                    {
                        r_Vehicles[io_LicenseNumber].Vehicle.Wheels[i].CurrentAirPressure = curAirPressure;
                    }
                }
                else
                {
                    throw new FormatException("Wheel pressure's input is invalid");
                }
            }

            if (i_FilledDictionary.ContainsKey(eQuestions.WheelManufacturer))
            {
                for (int i = 0; i < r_Vehicles[io_LicenseNumber].Vehicle.Wheels.Length; i++)
                {
                    r_Vehicles[io_LicenseNumber].Vehicle.Wheels[i].Manufacturer = i_FilledDictionary[eQuestions.WheelManufacturer].ToString();
                }
            }

            if (i_FilledDictionary.ContainsKey(eQuestions.Doors))
            {
                if (int.TryParse(i_FilledDictionary[eQuestions.Doors].ToString(), out int doors))
                {
                    (r_Vehicles[io_LicenseNumber].Vehicle as Car).Doors = (eDoors)doors;
                }
                else
                {
                    throw new FormatException("Number of doors's input is invalid");
                }
            }

            if (i_FilledDictionary.ContainsKey(eQuestions.LicenseType))
            {
                if (int.TryParse(i_FilledDictionary[eQuestions.LicenseType].ToString(), out int licenseType))
                {
                    (r_Vehicles[io_LicenseNumber].Vehicle as Motorcycle).LicenseType = (eLicenseType)licenseType;
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
                    (r_Vehicles[io_LicenseNumber].Vehicle as Car).Color = (eColor)color;
                }
                else
                {
                    throw new FormatException("Choice of color is invalid");
                }
            }

            if (i_FilledDictionary.ContainsKey(eQuestions.HazardousMaterials))
            {
                (r_Vehicles[io_LicenseNumber].Vehicle as Truck ).HazardousMaterials = (bool)i_FilledDictionary[eQuestions.HazardousMaterials];
            }

            if (i_FilledDictionary.ContainsKey(eQuestions.CargoCpacity))
            {
                if(float.TryParse(i_FilledDictionary[eQuestions.CargoCpacity].ToString(), out float cargo))
                {
                    (r_Vehicles[io_LicenseNumber].Vehicle as Truck).CargoVolume = cargo;
                }
                else
                {
                    throw new FormatException("Cargo capacity's input is invalid");
                }
            }

            if (i_FilledDictionary.ContainsKey(eQuestions.EngineCC))
            {
                if (int.TryParse(i_FilledDictionary[eQuestions.EngineCC].ToString(), out int engienCC))
                {
                    (r_Vehicles[io_LicenseNumber].Vehicle as Motorcycle).EngineCapacityInCC = engienCC;
                }
                else
                {
                    throw new FormatException("Engine CC's input is invalid");
                }
            }
        }        

        internal bool UpdateVehicleInfo(string i_LicenseNumber)
        {
            return true;
        }

        private bool IsExist(string io_LicenseNumber)
        {
            return r_Vehicles.ContainsKey(io_LicenseNumber);
        }

        public int NumOfSupportedVehicles()
        {
            return SupportedVehicles.sr_SupportedVehicles.Length;
        }

        public int NumOfFuelType()
        {
            return Enum.GetValues(typeof(eFuelType)).Length;
        }
    }
}