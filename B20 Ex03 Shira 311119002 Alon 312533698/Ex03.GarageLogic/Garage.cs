using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private readonly Dictionary<string, Customer> r_Vehicles = new Dictionary<string, Customer>();           

        public bool AddNewVehicle(string io_CustomerName, string io_CustomerPhoneNumber, int io_Choice, string io_LicenseNumber)
        { // function 1
            bool isAdded = false;

            if (isExist(io_LicenseNumber))
            {
                try
                {
                    r_Vehicles[io_LicenseNumber].VehicleStatus = eServiceStatuses.InRepair;
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("License bumber {0} is not the system.", io_LicenseNumber), ex);
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

        public string VehicleStringByFilter(bool i_InRepair, bool i_Fixed, bool i_Paid)
        { // function 2
            int index = 1;
            StringBuilder vehicleList = new StringBuilder();

            foreach (KeyValuePair<string, Customer> entry in r_Vehicles)
            {
                if (i_InRepair && entry.Value.VehicleStatus.Equals(eServiceStatuses.InRepair))
                {
                    vehicleList.Append(string.Format("{0}. {1}{2}", index++, entry.Key, Environment.NewLine));
                }

                if (i_Fixed && entry.Value.VehicleStatus.Equals(eServiceStatuses.Fixed))
                {                
                    vehicleList.Append(string.Format("{0}. {1}{2}", index++, entry.Key, Environment.NewLine));
                }

                if (i_Paid && entry.Value.VehicleStatus.Equals(eServiceStatuses.Paid))
                {
                    vehicleList.Append(string.Format("{0}. {1}{2}", index++, entry.Key, Environment.NewLine));
                }
            }

            return vehicleList.ToString();
        }

        public bool ChangeServiceStatus(string i_LicenseNumber, int i_NewStatus)
        { // function 3             
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
                    throw new ValueOutOfRangeException(ex, 3f, 1f);
                }
            }

            return isChanged;
        }

        public bool InflateWheels(string i_LicenseNumber)
        { // function 4
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
        { // function 5
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
                        throw new ValueOutOfRangeException(ex, (float)numOfFuelTypes(), 1f);
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
        { // function 6
            bool isFilled = false;

            if (isExist(i_LicenseNumber))
            {
                if (r_Vehicles[i_LicenseNumber].VehicleStatus.Equals(eServiceStatuses.InRepair)) 
                {
                    if (r_Vehicles[i_LicenseNumber].Vehicle.EnergyType is Electric)
                    {
                        Electric engine = r_Vehicles[i_LicenseNumber].Vehicle.EnergyType as Electric;
                        isFilled = engine.ChargeBattery(io_MinutesToAdd / 60f);

                        if (isFilled)
                        {
                            float percent = engine.HoursLeftInBattery / engine.MaxHoursInBattery;
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
        { // function 7
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
            SupportedVehicles.GetInfo(out Dictionary<eQuestions, object> DicToFill, io_Choice);
            return DicToFill;
        }

        public void InsertInfo(Dictionary<eQuestions, object> i_FilledDictionary, string io_LicenseNumber)
        {
            SupportedVehicles.FillVehicleInfo(r_Vehicles[io_LicenseNumber].Vehicle, i_FilledDictionary);
        }        

        private bool isExist(string io_LicenseNumber)
        {
            return r_Vehicles.ContainsKey(io_LicenseNumber);
        }

        public int NumOfSupportedVehicles()
        {
            return SupportedVehicles.sr_SupportedVehicles.Length;
        }

        private int numOfFuelTypes()
        {
            return Enum.GetValues(typeof(eFuelTypes)).Length;
        }

        public string GetCarDoors()
        {
            return Car.GetDoorsOptions();
        }

        public string GetCarColors()
        {
            return Car.GetColorsOptions();
        }

        public string GetMotorcycleLicenseTypes()
        {
            return Motorcycle.GetLicenseTypes();
        }
    }
}