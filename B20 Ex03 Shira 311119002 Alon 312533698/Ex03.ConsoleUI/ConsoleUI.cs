using System;
using System.Collections.Generic;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class ConsoleUI
    {
        private const int k_LicenseLength = 9;
        private const int k_NumberOfOptions = 8;

        private readonly string r_ErrorMsg = "Ivalid input, please try again.";
        private readonly Garage r_MyGarage = new Garage();

        internal ConsoleUI()
        {
            //// Run garage menu
            int userChoice = 0;
            
            printOptions(out userChoice);

            while (userChoice != k_NumberOfOptions)
            {
                try
                {
                    switch (userChoice)
                    {
                        case 1:
                            addNewVehicleInput();
                            break;
                        case 2:
                            printByFilter();
                            break;
                        case 3:
                            changeVehicleStatus();
                            break;
                        case 4:
                            inflateToMax();
                            break;
                        case 5:
                            fillGas();
                            break;
                        case 6:
                            chargeBattery();
                            break;
                        case 7:
                            printByLicense();
                            break;
                    }
                }                
                catch (Exception ex)
                {
                    if (ex is ValueOutOfRangeException)
                    {
                        Console.WriteLine((ex as ValueOutOfRangeException).InnerException.Message);
                    }

                    Console.WriteLine(ex.Message);
                }

                System.Threading.Thread.Sleep(2000);
                Console.Clear();
                printOptions(out userChoice);
            }
        }      
        
        private void printOptions(out int io_Choice)
        {                    
            io_Choice = 0;
            string error = string.Format("{0}{1}The input must be an option between 1-8.{1}", r_ErrorMsg, Environment.NewLine);
            string menu = string.Format(
                @"Welcome to {0}

What whould you like to do?
1. Enter new vehicle to the garage.
2. Show license number of vehicles by filter.
3. Change status of vehicle.
4. Inflating air in wheels.
5. Fill gas tank.
6. Charge battery.
7. Show vehicle details.
8. Exit {0}",
r_MyGarage.Name);

            Console.WriteLine(menu);

            while (!int.TryParse(Console.ReadLine(), out io_Choice) || io_Choice < 1 || io_Choice > k_NumberOfOptions)
            {                
                Console.WriteLine(error);
            }
        }

        private void addNewVehicleInput()
        {
            getNameAndPhone(out string name, out string phoneNumber);
            getVehicle(out int vehicleChoice);
            getLicenseNumber(out string licenseNumber);

            if (r_MyGarage.AddNewVehicle(name, phoneNumber, vehicleChoice, licenseNumber))
            {
                getExtraInfo(vehicleChoice, licenseNumber);
                Console.WriteLine("This vehicle added successfuly to {0}", r_MyGarage.Name);
            }
            else
            {
                Console.WriteLine("This vehicle is already in {0}", r_MyGarage.Name);
            }        
        }

        private void printByFilter()
        {
            bool seeInReapir, seePaid, seePrepared;

            Console.WriteLine("Do you want to see all vehicle (without filter) (Y/N)?");
            getYesOrNO(out bool seeAll);

            if (!seeAll)
            {
                Console.WriteLine("Do you want to see vehicle in-reapir? (Y/N)");
                getYesOrNO(out seeInReapir);

                Console.WriteLine("Do you want to see vehicle prepared? (Y/N)");
                getYesOrNO(out seePrepared);

                Console.WriteLine("Do you want to see vehicle paid? (Y/N)");
                getYesOrNO(out seePaid);
            }
            else
            {
                seeInReapir = true;
                seePrepared = true;
                seePaid = true;
            }

            Console.Write(r_MyGarage.VehicleStringByFilter(seeInReapir, seePrepared, seePaid));
        }

        private void getYesOrNO(out bool io_Choice)
        {
            string input = Console.ReadLine();

            while (input != "Y" && input != "N")
            {
                Console.WriteLine("{0}{1}Please enter Y/N", r_ErrorMsg, Environment.NewLine);
                input = Console.ReadLine();
            }

            if (input == "Y")
            {
                io_Choice = true;
            }
            else
            {
                io_Choice = false;
            }
        }

        private void changeVehicleStatus()
        {
            int choice = 0;
            getLicenseNumber(out string licenseInput);

            Console.Write("Choose new status:{0}{1}", Environment.NewLine, r_MyGarage.ShowOptionsOfServiceStatuses());

            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > r_MyGarage.NumOfServiceStatuses())
            {
                Console.WriteLine(r_ErrorMsg);                
            }

            if (r_MyGarage.ChangeServiceStatus(licenseInput, choice)) 
            {
                Console.WriteLine("Status Updated!");
            }
            else
            {
                Console.WriteLine("License does not exists in {0}", r_MyGarage.Name);
            }
        }

        private void inflateToMax()
        {
            getLicenseNumber(out string licenseNumber);

            if (r_MyGarage.InflateWheels(licenseNumber))
            {
                Console.WriteLine("The wheels are now full");
            }
            else
            {
                Console.WriteLine("License does not exists in {0}", r_MyGarage.Name);
            }
        }

        private void fillGas()
        {
            int gasType = -1;
            float amountToadd = -1f;

            getLicenseNumber(out string licenseNumber);            

            Console.WriteLine("Which type of gas do you want to add?");
            Console.Write(r_MyGarage.ShowFuelTypes());            

            while (!int.TryParse(Console.ReadLine(), out gasType) || gasType < 1 || gasType > r_MyGarage.NumOfFuelTypes())
            {
                Console.WriteLine(r_ErrorMsg);
            }

            Console.WriteLine("How much do you want to add?");           

            while (!float.TryParse(Console.ReadLine(), out amountToadd) || amountToadd < 0f)
            {
                Console.WriteLine("{0}{1}Please enter positive amount", r_ErrorMsg, Environment.NewLine);
            }

            if (r_MyGarage.FillGasTank(licenseNumber, gasType, amountToadd)) 
            {
                Console.WriteLine("Tank Filled");
            }
            else
            {
                Console.WriteLine("Vehicle did not found.");
            }
        }

        private void chargeBattery()
        {
            string licenseNumber;
            float amountToAdd = -1f;

            getLicenseNumber(out licenseNumber);

            Console.WriteLine("How much minutes do you want to add?");
            float.TryParse(Console.ReadLine(), out amountToAdd);

            while (amountToAdd <= 0)
            {
                Console.WriteLine("{0}{1}Please enter postive number.", r_ErrorMsg, Environment.NewLine);
                float.TryParse(Console.ReadLine(), out amountToAdd);
            }

            if (r_MyGarage.ChargeElectricBattery(licenseNumber, amountToAdd)) 
            {
                Console.WriteLine("Battery charged.");
            }
            else
            {
                Console.WriteLine("Vehicle did not found.");
            }
        }

        private void printByLicense()
        { 
            getLicenseNumber(out string licenseNumber);

            if (r_MyGarage.VehicleInfo(licenseNumber, out string msg)) 
            {
                Console.WriteLine("{0}{1}{0}Press any key to go back to menu.", Environment.NewLine, msg);
                Console.ReadLine();
            }
            else 
            {
                Console.WriteLine("Vehicle did not found.");
            }
        }

        private void getLicenseNumber(out string io_LicenseNumber)
        {            
            Console.WriteLine("Enter your license number:");
            io_LicenseNumber = Console.ReadLine();

            while (io_LicenseNumber.Length != k_LicenseLength ) 
            {
                Console.WriteLine("{0}{1}License number should be {2} digits.", r_ErrorMsg, Environment.NewLine, k_LicenseLength);
                io_LicenseNumber = Console.ReadLine();
            }
        }

        private void getNameAndPhone(out string io_Name, out string io_Phone)
        {
            Console.WriteLine("Please enter customer's name:");
            io_Name = Console.ReadLine();

            while (io_Name.Length < 3) 
            {
                Console.WriteLine("{0}{1}Name should be at least 3 letters.", r_ErrorMsg, Environment.NewLine);
                io_Name = Console.ReadLine();
            }

            Console.WriteLine("Please enter customer's phone number:");
            io_Phone = Console.ReadLine();

            while (!int.TryParse(io_Phone, out int check) || io_Phone.Length != 10 || io_Phone[0] != '0' || io_Phone[1] != '5') 
            {
                Console.WriteLine("{0}{1}Phone number should be 10 digit's in the form 05xxxxxxxx.", r_ErrorMsg, Environment.NewLine);
                io_Phone = Console.ReadLine();
            }
        }

        private void getVehicle(out int vehicleChoice)
        {
            int maxChoice = r_MyGarage.NumOfSupportedVehicles();
            vehicleChoice = 0;

            Console.Write("Choose one of our supported vehicles:{0}{1}", Environment.NewLine, r_MyGarage.ShowSupportedVehicles());
            int.TryParse(Console.ReadLine(), out vehicleChoice);

            while (vehicleChoice < 1 || vehicleChoice > maxChoice)
            {
                Console.WriteLine("{0}{1}Must be one of these options (1-{2}).", r_ErrorMsg, Environment.NewLine, maxChoice);
                int.TryParse(Console.ReadLine(), out vehicleChoice);
            }
        }

        private void getExtraInfo(int io_Choice, string io_LicenseNumber)
        {   
            Dictionary<eQuestions, object> infoDicToFill = r_MyGarage.GetExtraInfo(io_Choice);
            bool invalidInput = true;
            
            while (invalidInput)
            {
                if (infoDicToFill.ContainsKey(eQuestions.ModelName))
                {
                    Console.WriteLine("Please enter Model Name:");
                    infoDicToFill[eQuestions.ModelName] = Console.ReadLine();
                }

                if (infoDicToFill.ContainsKey(eQuestions.CurrentFuel))
                {
                    Console.WriteLine("Please enter Current Fuel Amount:");
                    infoDicToFill[eQuestions.CurrentFuel] = Console.ReadLine();
                }

                if (infoDicToFill.ContainsKey(eQuestions.CurrentHours))
                {
                    Console.WriteLine("Please enter Current Minutes in Battery:");
                    infoDicToFill[eQuestions.CurrentHours] = Console.ReadLine();
                }

                if (infoDicToFill.ContainsKey(eQuestions.CurentWheelAirPressure)) 
                {
                    Console.WriteLine("Please enter Current Wheels Air Pressure:");
                    infoDicToFill[eQuestions.CurentWheelAirPressure] = Console.ReadLine();
                }

                if (infoDicToFill.ContainsKey(eQuestions.WheelManufacturer))
                {
                    Console.WriteLine("Please enter Wheels Manufacturer:");
                    infoDicToFill[eQuestions.WheelManufacturer] = Console.ReadLine();
                }

                if (infoDicToFill.ContainsKey(eQuestions.Doors))
                {
                    string question = string.Format("Choose Number of Doors:{0}{1}", Environment.NewLine, r_MyGarage.ShowCarDoors());
                    Console.Write(question);
                    infoDicToFill[eQuestions.Doors] = Console.ReadLine();
                }

                if (infoDicToFill.ContainsKey(eQuestions.LicenseType))
                {
                    string question = string.Format("Choose License Type of your Motorcycle:{0}{1}", Environment.NewLine, r_MyGarage.ShowMotorcycleLicenseTypes());
                    Console.Write(question);
                    infoDicToFill[eQuestions.LicenseType] = Console.ReadLine();
                }

                if (infoDicToFill.ContainsKey(eQuestions.Color))
                {
                    string question = string.Format("Choose car's Color:{0}{1}", Environment.NewLine, r_MyGarage.ShowCarColors());
                    Console.Write(question);
                    infoDicToFill[eQuestions.Color] = Console.ReadLine();
                }

                if (infoDicToFill.ContainsKey(eQuestions.HazardousMaterials))
                {
                    Console.WriteLine("Does the Truck Contains Hazardous Materials (Y/N)");
                    getYesOrNO(out bool answer);
                    infoDicToFill[eQuestions.HazardousMaterials] = answer;
                }

                if (infoDicToFill.ContainsKey(eQuestions.CargoCpacity))
                {
                    Console.WriteLine("What is the Truck Cargo Cpacity?");
                    infoDicToFill[eQuestions.CargoCpacity] = Console.ReadLine();
                }

                if (infoDicToFill.ContainsKey(eQuestions.EngineCC))
                {
                    Console.WriteLine("Please enter Engine in CC:");
                    infoDicToFill[eQuestions.EngineCC] = Console.ReadLine();
                }

                try
                {
                    r_MyGarage.InsertInfo(infoDicToFill, io_LicenseNumber);
                    invalidInput = false;
                }
                catch (Exception ex)
                {
                    if (ex is ValueOutOfRangeException)
                    {
                        Console.WriteLine((ex as ValueOutOfRangeException).InnerException.Message);
                    }

                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Please enter extra info again:");
                }
            }
        }        
    }
}