using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

    public struct Info<T>
    {
        private eQuestions m_NumOfInfo;
        private T m_InfoVehicle;

        public Info(eQuestions i_NumOfInfo, T i_Info)
        {
            m_NumOfInfo = i_NumOfInfo;
            m_InfoVehicle = i_Info;
        }

        public T InfoVehicle
        {
            set { m_InfoVehicle = value; }
        }
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

        public void UpdateInfo(Dictionary<eQuestions, object> i_FilledDictionary)
        {

        }

        /* public List<object> GetExtraInfo(int io_Choice)
         {
             List<object> listToFill = new List<object>();            

             if (io_Choice > 0 && io_Choice < NumOfSupportedVehicles())
             {
                 eSupportVehicles currentVehicle = (eSupportVehicles)io_Choice;

                 listToFill.Add(new Info<string>(eQuestions.WheelManufacturer, string.Empty));
                 listToFill.Add(new Info<float>(eQuestions.CurentWheelAirPressure, 0f));
                 listToFill.Add(new Info<string>(eQuestions.ModelName, string.Empty));

                 if (currentVehicle.Equals(eSupportVehicles.ElectricMotorcycle) || currentVehicle.Equals(eSupportVehicles.ElectricCar))
                 { // Hours left in elecrtic engien
                     listToFill.Add(new Info<float>(eQuestions.CurrentHours, 0f));
                 }
                 else
                 { // Fuel left
                     listToFill.Add(new Info<float>(eQuestions.CurrentFuel, 0f));
                 }

                 if (currentVehicle.Equals(eSupportVehicles.RegularMotorcycle) || currentVehicle.Equals(eSupportVehicles.ElectricMotorcycle))
                 { // Case of motorcycle
                     listToFill.Add(new Info<int>(eQuestions.LicenseType, 0));
                     listToFill.Add(new Info<int>(eQuestions.EngineCC, 0));
                 }

                 if (currentVehicle.Equals(eSupportVehicles.ElectricCar) || currentVehicle.Equals(eSupportVehicles.RegularCar))
                 { // Case of car  
                     listToFill.Add(new Info<int>(eQuestions.Color, 0));
                     listToFill.Add(new Info<int>(eQuestions.Doors, 0));
                 }

                 if (currentVehicle.Equals(eSupportVehicles.Truck))
                 { // Case of truck  
                     listToFill.Add(new Info<bool>(eQuestions.HazardousMaterials, false));
                     listToFill.Add(new Info<float>(eQuestions.CargoCpacity, 0f));
                 }
             }

         }*/

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