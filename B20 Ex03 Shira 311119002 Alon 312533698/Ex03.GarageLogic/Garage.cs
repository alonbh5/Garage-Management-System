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

    internal struct Info<T>
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

        public bool IsExist(string io_LicenseNumber)
        {
            return r_Vehicles.ContainsKey(io_LicenseNumber);
        }

        public int NumOfSupportedVehicles()
        {
            return SupportedVehicles.sr_SupportedVehicles.Length;
        }

        public bool AddNewVehicle(string io_CustomerName, string io_CustomerPhoneNumber, int io_Choice, string io_LicenseNumber)
        { // function 1
            bool isAdded = false;

            if (r_Vehicles.ContainsKey(io_LicenseNumber))
            {
                r_Vehicles[io_LicenseNumber].VehicleStatus = eServiceStatus.InRepair;
            }
            else
            {
                Vehicle newVehicle = SupportedVehicles.CreateVehicle((eSupportVehicles)io_Choice, io_LicenseNumber);
                Customer newCustomer = new Customer(io_CustomerName, io_CustomerPhoneNumber, eServiceStatus.InRepair, newVehicle);
                r_Vehicles.Add(io_LicenseNumber, newCustomer);
                isAdded = true;
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

            if (r_Vehicles.ContainsKey(i_LicenseNumber) && Enum.IsDefined(typeof(eServiceStatus), i_NewStatus))  //maybe add exeption
            {
                r_Vehicles[i_LicenseNumber].VehicleStatus = (eServiceStatus)i_NewStatus;
                isChanged = true;
            }

            return isChanged;
        }

        public bool InflateWheels(string i_LicenseNumber)
        { // function 4
            bool isInflated = false;
            float amountToAdd = 0f;

            if (r_Vehicles.ContainsKey(i_LicenseNumber) && r_Vehicles[i_LicenseNumber].VehicleStatus.Equals(eServiceStatus.InRepair))
            {
                Wheel[] currentWheels = r_Vehicles[i_LicenseNumber].Vehicle.Wheels;

                for (int i = 0; i < currentWheels.Length; i++) 
                {
                    amountToAdd = currentWheels[i].MaxAirPressure - currentWheels[i].CurrentAirPressure;
                    currentWheels[i].InflatingAirPressure(amountToAdd);
                }

                isInflated = true;
            }

            return isInflated;
        }

        public bool FillGasTank(string i_LicenseNumber, int io_FuelType, float io_AmountToAdd)
        { // function 5
            bool isFilled = false;


            if (r_Vehicles.ContainsKey(i_LicenseNumber) && r_Vehicles[i_LicenseNumber].VehicleStatus.Equals(eServiceStatus.InRepair) && Enum.IsDefined(typeof(eFuelType),io_FuelType))
            {
                if (r_Vehicles[i_LicenseNumber].Vehicle.EnergyType is Fuel)
                {
                    isFilled = (r_Vehicles[i_LicenseNumber].Vehicle.EnergyType as Fuel).FillTank(io_AmountToAdd, (eFuelType)io_FuelType);
                    if (isFilled)
                    {
                        float percent = (r_Vehicles[i_LicenseNumber].Vehicle.EnergyType as Fuel).CurrentFuelTank / (r_Vehicles[i_LicenseNumber].Vehicle.EnergyType as Fuel).MaxTank;
                        r_Vehicles[i_LicenseNumber].Vehicle.PercentagesOfEnergyRemaining = percent;
                    }
                }
            }

            return isFilled;
        }

        public bool FillCharge(string i_LicenseNumber, float io_MinutesToAdd)
        { // function 6
            bool isFilled = false;

            if (r_Vehicles.ContainsKey(i_LicenseNumber) && r_Vehicles[i_LicenseNumber].VehicleStatus.Equals(eServiceStatus.InRepair) && r_Vehicles[i_LicenseNumber].VehicleStatus.Equals(eServiceStatus.InRepair))
            {
                if (r_Vehicles[i_LicenseNumber].Vehicle.EnergyType is Electric)
                {
                    isFilled = (r_Vehicles[i_LicenseNumber].Vehicle.EnergyType as Electric).ChargeBattery(io_MinutesToAdd / 60f);
                    if (isFilled)
                    {
                        float percent = (r_Vehicles[i_LicenseNumber].Vehicle.EnergyType as Electric).HoursLeftInBattery / (r_Vehicles[i_LicenseNumber].Vehicle.EnergyType as Electric).MaxHoursInBattery;
                        r_Vehicles[i_LicenseNumber].Vehicle.PercentagesOfEnergyRemaining = percent;
                    }
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

        public string GetFuelTypes()
        {
            return Fuel.GetFuelTypes();
        }

        public List<object> GetExtraInfo(int io_Choice)
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

            return listToFill;
        }

        internal bool UpdateVehicleInfo(string i_LicenseNumber)
        {
            return true;
        }
    }
}