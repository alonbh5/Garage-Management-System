using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private readonly string r_Name = "THE BEST GARAGE IN TOWN!";
        private readonly Dictionary<string, Customer> r_Vehicles = new Dictionary<string, Customer>();           

        public string Name
        {
            get { return r_Name; }
        }

        public bool AddNewVehicle(string io_CustomerName, string io_CustomerPhoneNumber, int io_Choice, string io_LicenseNumber)
        {
            //// Gets new customer (name, phone number and license plate number) and choice out of 
            //// the garage supported vehicles.
            //// Two possible cases:
            //// 1. New license plate number - add it to the garage.
            //// 2. Existing license plate number - change state of car to - "In-Repair"
            //// Return T/F if insertion succeeded.
           
            bool isAdded = false;

            if (isExist(io_LicenseNumber))
            {
                try
                {
                    r_Vehicles[io_LicenseNumber].VehicleStatus = eServiceStatuses.InRepair;
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("License number {0} is not the system.", io_LicenseNumber), ex);
                }
            }
            else
            {
                if (Enum.IsDefined(typeof(eSupportVehicles), io_Choice))
                {
                    Vehicle newVehicle = SupportedVehicles.CreateVehicle((eSupportVehicles)io_Choice, io_LicenseNumber);
                    Customer newCustomer = new Customer(io_CustomerName, io_CustomerPhoneNumber, eServiceStatuses.InRepair, newVehicle);
                    r_Vehicles.Add(io_LicenseNumber, newCustomer);
                    isAdded = true;
                }
                else
                {
                    Exception ex = new Exception("Choice of supported vehicle is invalid");
                    throw new ValueOutOfRangeException(ex, (float)NumOfSupportedVehicles(), 1f);
                }
            }

            return isAdded;
        }

        public string VehicleStringByFilter(bool i_ShowInRepair, bool i_ShowFixed, bool i_ShowPaid)
        {
            //// Return a string of all vehicles license number in the garage by filter or unfilter.
         
            int index = 1;
            StringBuilder vehicleList = new StringBuilder();

            foreach (KeyValuePair<string, Customer> entry in r_Vehicles)
            {
                if (i_ShowInRepair && entry.Value.VehicleStatus.Equals(eServiceStatuses.InRepair))
                {
                    vehicleList.Append(string.Format("{0}. {1}{2}", index++, entry.Key, Environment.NewLine));
                }

                if (i_ShowFixed && entry.Value.VehicleStatus.Equals(eServiceStatuses.Fixed))
                {                
                    vehicleList.Append(string.Format("{0}. {1}{2}", index++, entry.Key, Environment.NewLine));
                }

                if (i_ShowPaid && entry.Value.VehicleStatus.Equals(eServiceStatuses.Paid))
                {
                    vehicleList.Append(string.Format("{0}. {1}{2}", index++, entry.Key, Environment.NewLine));
                }
            }

            if (vehicleList.ToString().Equals(string.Empty))
            {
                vehicleList.Append("No vehicles in the garage.");
            }

            return vehicleList.ToString();
        }

        public bool ChangeServiceStatus(string i_LicenseNumber, int i_NewStatus)
        {
            //// Gets license number and new status and change the state of the car in the system.
            //// Return T/F if change succeeded.
            
            bool isChanged = false;

            if (isExist(i_LicenseNumber))
            {
                if (Enum.IsDefined(typeof(eServiceStatuses), i_NewStatus))
                {
                    try
                    {
                        r_Vehicles[i_LicenseNumber].VehicleStatus = (eServiceStatuses)i_NewStatus;
                        isChanged = true;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(string.Format("License Number {0} is not the system", i_LicenseNumber), ex);
                    }
                }
                else
                {
                    Exception ex = new Exception("Choice of vehicle status is invalid");
                    throw new ValueOutOfRangeException(ex, (float)NumOfServiceStatuses(), 1f);
                }
            }

            return isChanged;
        }

        public bool InflateWheels(string i_LicenseNumber)
        {
            //// Gets car's license number and infalte all of the car's tires to max.
            //// Return T/F if inflation succeeded.
            
            bool isInflated = false;
            float amountToAdd = 0f;

            if (isExist(i_LicenseNumber))
            {
                if (r_Vehicles[i_LicenseNumber].VehicleStatus.Equals(eServiceStatuses.InRepair))
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
        {
            //// Gets car's license number, fuel type and amount of fuel to add.
            //// Filled the tank if fuel type match and amount is not above max.
            //// Return T/F if filling succeeded.
            
            bool isFilled = false;

            if (isExist(i_LicenseNumber))
            {
                if (r_Vehicles[i_LicenseNumber].VehicleStatus.Equals(eServiceStatuses.InRepair))
                {
                    if (Enum.IsDefined(typeof(eFuelTypes), io_FuelType))
                    {
                        if (r_Vehicles[i_LicenseNumber].Vehicle.EnergyType is Fuel)
                        {
                            Fuel currEngine = r_Vehicles[i_LicenseNumber].Vehicle.EnergyType as Fuel;
                            isFilled = currEngine.FillTank(io_AmountToAdd, (eFuelTypes)io_FuelType);

                            if (isFilled)
                            {
                                float percent = currEngine.CurrentFuelTank / currEngine.MaxTank;
                                r_Vehicles[i_LicenseNumber].Vehicle.PercentagesOfEnergyRemaining = percent * 100f;
                            }
                        }
                        else
                        {
                            throw new FormatException("The vehicle engine is not runnig on fuel.");
                        }
                    }
                    else
                    {
                        Exception ex = new Exception("Choice of fuel type is invalid");
                        throw new ValueOutOfRangeException(ex, (float)NumOfFuelTypes(), 1f);
                    }
                }
                else
                {
                    throw new ArgumentException("The vehicle is not 'inrepair' status. You can not work on this vehicle.");
                }
            }

            return isFilled;
        }

        public bool ChargeElectricBattery(string i_LicenseNumber, float io_MinutesToAdd)
        {
            //// Gets car's license number and minutes of electricity to add.
            //// Charge the car if minutes is not above max.
            //// Return T/F if charge succeeded.
            
            bool isFilled = false;

            if (isExist(i_LicenseNumber))
            {
                if (r_Vehicles[i_LicenseNumber].VehicleStatus.Equals(eServiceStatuses.InRepair)) 
                {
                    if (r_Vehicles[i_LicenseNumber].Vehicle.EnergyType is Electric)
                    {
                        Electric currEngine = r_Vehicles[i_LicenseNumber].Vehicle.EnergyType as Electric;
                        isFilled = currEngine.ChargeBattery(io_MinutesToAdd / 60f);

                        if (isFilled)
                        {
                            float percent = currEngine.HoursLeftInBattery / currEngine.MaxHoursInBattery;
                            r_Vehicles[i_LicenseNumber].Vehicle.PercentagesOfEnergyRemaining = percent * 100f;
                        }                        
                    }
                    else
                    {
                        throw new FormatException("The vehicle engine is not electric!");
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
        {
            //// Gets car's license number and out string.
            //// Store in out string all the information about said car.
            //// Return T/F if car is found in the garage.
            
            bool found = false;

            StringBuilder vehicleInfo = new StringBuilder();

            if (isExist(i_LicenseNumber))
            {
                vehicleInfo.Append(r_Vehicles[i_LicenseNumber].ToString());
                found = true;
            }

            io_VehicleInfo = vehicleInfo.ToString();
            return found;
        }

        public Dictionary<eQuestions, object> GetExtraInfo(int io_VehicleChoice)
        {
            //// For UI - gets vehicle choice out of supported vehicles.
            //// Return a dictionary where key is Enum of eQusetion and data is object for the UI to insert.
            //// eQusetion helps UI to know what information is needed from user.
            //// Enum of eQusetion can be found in SupportedVehicles Class.
            
            SupportedVehicles.GetInfo(out Dictionary<eQuestions, object> DicToFill, io_VehicleChoice);
            return DicToFill;
        }

        public void InsertInfo(Dictionary<eQuestions, object> i_FilledDictionary, string io_LicenseNumber)
        {
            //// For UI - gets FILLED dictionary that was given to UI by Garage::GetExtraInfo
            //// and license number of car needed for extra information.
            //// If value is invalid - an exception will be thrown.
            
            SupportedVehicles.FillVehicleInfo(r_Vehicles[io_LicenseNumber].Vehicle, i_FilledDictionary);
        }        

        private bool isExist(string io_LicenseNumber)
        {
            return r_Vehicles.ContainsKey(io_LicenseNumber);
        }

        public int NumOfSupportedVehicles()
        {
            return SupportedVehicles.ArrOfSupportedVehicles.Length;
        }

        public int NumOfServiceStatuses()
        {
            return Enum.GetValues(typeof(eServiceStatuses)).Length;
        }

        public int NumOfFuelTypes()
        {
            return Enum.GetValues(typeof(eFuelTypes)).Length;
        }

        public string ShowSupportedVehicles()
        {
            return SupportedVehicles.ShowSupportedVehiclesTypes();
        }

        public string ShowFuelTypes()
        {
            return Fuel.ShowFuelTypes();
        }

        public string ShowCarDoors()
        {
            return Car.ShowDoorsOptions();
        }

        public string ShowCarColors()
        {
            return Car.ShowColorsOptions();
        }

        public string ShowMotorcycleLicenseTypes()
        {
            return Motorcycle.ShowLicenseTypes();
        }

        public string ShowOptionsOfServiceStatuses()
        {
            return Customer.ShowServiceStatuses();
        }
    }
}