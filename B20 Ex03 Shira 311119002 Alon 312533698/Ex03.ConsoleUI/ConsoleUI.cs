using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class ConsoleUI
    {
        private const int k_LicenseLength = 9;
        
        private readonly Garage r_MyGarage = new Garage();

        internal ConsoleUI()
        {
            int choice = 0;

            printOptions(out choice);

            while (choice != 8)
            {
                try
                {
                    switch (choice)
                    {
                        case 1:
                            AddNewVehicleInput();
                            break;
                        case 2:
                            printByFilter();
                            break;
                        case 3:
                            changeState();
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
                    Console.WriteLine(ex.Message);
                }

                System.Threading.Thread.Sleep(2000);
                Console.Clear();
                printOptions(out choice);
            }
        }      
        
        private void printOptions(out int io_Choice)
        {                    
            io_Choice = 0;
            string error = string.Format("Invalid input.{0}The input must be an option between 1-8.{0}", Environment.NewLine);
            string menu = string.Format(@"Welcome to THE BEST GARAGE IN TOWN
1. Enter new vehicle to the garage.
2. Show license number of vehicles by filter.
3. Change status of vehicle.
4. Inflating air in wheels.
5. Fill gas tank.
6. Charge battery.
7. Show vehicle details.
8. Exit THE BEST GARAGE IN TOWN");
            
            Console.WriteLine(menu);

            int.TryParse(Console.ReadLine(), out io_Choice);            

            while (io_Choice < 1 || io_Choice > 8)
            {                
                Console.WriteLine(error);
                int.TryParse(Console.ReadLine(), out io_Choice);
            }
        }

        public void AddNewVehicleInput()
        {
            string name, phoneNumber, licenseNumber;
            int vehicleChoice;

            getNameAndPhone(out name, out phoneNumber);
            getSupportedVehicles(out vehicleChoice);
            getLicenseNumber(out licenseNumber);

            if (r_MyGarage.AddNewVehicle(name, phoneNumber, vehicleChoice, licenseNumber))
            {
                getExtraInfo(vehicleChoice, licenseNumber);
                Console.WriteLine("This vehicle added successfuly to THE BEST GARAGE IN TOWN! HAVE FUN");
            }
            else
            {
                Console.WriteLine("This vehicle is already in THE BEST GARAGE IN TOWN!");
            }        
        }

        private void printByFilter()
        {
            bool seeInReapir, seePaid, seePrepared, seeAll;

            Console.WriteLine("Do you want to see all vehicle (without filter) (Y/N)?");
            getYesOrNO(out seeAll);
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

            Console.WriteLine(r_MyGarage.VehicleStringByFilter(seeInReapir, seePrepared, seePaid));
        }

        private void getYesOrNO(out bool io_Choice)
        {
            string input = Console.ReadLine();

            while (input != "Y" && input != "N")
            {
                Console.WriteLine("Worng input - Enter Y/N");
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

        private void changeState()
        {
            string lincenseInput;
            int input;
            getLicenseNumber(out lincenseInput);

            Console.WriteLine("Enter new state : 1 - In Repair 2 - Fixed 3 - Paid");
            int.TryParse(Console.ReadLine(), out input);

            while (input != 1 && input != 2 && input != 3) 
            {
                Console.WriteLine("Wrong Input!  1 - In Repair | 2 - Fixed | 3 - Paid");
                int.TryParse(Console.ReadLine(), out input);
            }

            if (r_MyGarage.ChangeServiceStatus(lincenseInput, input)) 
            {
                Console.WriteLine("Status Updated!");
            }
            else
            {
                Console.WriteLine("Lincense does not exsit in THE BEST GARAGE IN TOWN");
            }
        }

        private void inflateToMax()
        {
            string licensenumber;

            getLicenseNumber(out licensenumber);

            if (r_MyGarage.InflateWheels(licensenumber))
            {
                Console.WriteLine("The wheels are now full");
            }
            else
            {
                Console.WriteLine("Lincense does not exsit in THE BEST GARAGE IN TOWN");
            }
        }

        private void fillGas()
        {
            string licensenumber;

            getLicenseNumber(out licensenumber);
            int gasType = -1;
            float amountToadd = -1f;

            Console.WriteLine("Which type of gas to you wish to add?");
            Console.WriteLine(r_MyGarage.GetFuelTypes());

            int.TryParse(Console.ReadLine(), out gasType);

            while (gasType < 1 || gasType > 4)
            {
                Console.WriteLine("Wrong input - try agian");
                int.TryParse(Console.ReadLine(), out gasType);
            }

            Console.WriteLine("how much do you want to add?");
            
            float.TryParse(Console.ReadLine(), out amountToadd);

            while (amountToadd < 0f)
            {
                Console.WriteLine("Wrong input - please enter positive amount");
                float.TryParse(Console.ReadLine(), out amountToadd);
            }

            if (r_MyGarage.FillGasTank(licensenumber, gasType, amountToadd)) 
            {
                Console.WriteLine("Tank Filled");
            }
            else
            {
                Console.WriteLine("Tank did NOT filled"); //exeption !@#!@$!@!@%!@%!@%!@%!@%!@%!@%!@
            }
        }

        private void chargeBattery()
        {
            string licenseNumber;
            float amountToAdd = -1f;

            getLicenseNumber(out licenseNumber);

            Console.WriteLine("how many minutes do you want to add?");
            float.TryParse(Console.ReadLine(), out amountToAdd);

            while (amountToAdd <= 0)
            {
                Console.WriteLine("Wrong input! enter postive number");
                float.TryParse(Console.ReadLine(), out amountToAdd);
            }

            if (r_MyGarage.ChargeElectricBattery(licenseNumber, amountToAdd)) 
            {
                Console.WriteLine("Battery Charged!");
            }
            else
            {
                Console.WriteLine("Fail"); // exepetion !31311313^#$#$%#$%#$%#$%
            }
        }

        private void printByLicense()
        { // function 7
            string msg;

            getLicenseNumber(out string licenseNumber);

            if (r_MyGarage.VehicleInfo(licenseNumber, out msg)) 
            {
                Console.WriteLine(msg);
            }
            else 
            {
                Console.WriteLine("Vehicle did not found");
            }
        }

        private void getLicenseNumber(out string io_LicenseNumber)
        {            
            Console.WriteLine("Enter your license number");
            io_LicenseNumber = Console.ReadLine();

            while (io_LicenseNumber.Length != k_LicenseLength ) 
            {
                Console.WriteLine(string.Format("Invalid input.{0}License number should be {1} digits", Environment.NewLine, k_LicenseLength));
                io_LicenseNumber = Console.ReadLine();
            }
        }

        private void getNameAndPhone(out string io_Name, out string io_Phone)
        {
            Console.WriteLine("Please enter customer name");
            io_Name = Console.ReadLine();

            while (io_Name.Length < 3) 
            {
                Console.WriteLine("Name should be at least 3 letters, try agian:");
                io_Name = Console.ReadLine();
            }

            Console.WriteLine("Please enter customer phone number");
            io_Phone = Console.ReadLine();

            while (!int.TryParse(io_Phone, out int check) || io_Phone.Length != 10 || io_Phone[0] != '0' || io_Phone[1] != '5') 
            {
                Console.WriteLine("Phone number invalid (10 digit's in the form 05xxxxxxxx)");
                io_Phone = Console.ReadLine();
            }
        }

        private void getSupportedVehicles(out int vehicleChoice)
        {
            int maxChoice = r_MyGarage.NumOfSupportedVehicles();
            vehicleChoice = 0;

            Console.Write("Choose one of our supported vehicles:{0}{1}", Environment.NewLine, r_MyGarage.ShowSupportedVehicles());
            int.TryParse(Console.ReadLine(), out vehicleChoice);

            while (vehicleChoice < 1 || vehicleChoice > maxChoice)
            {
                Console.WriteLine(string.Format("Wrong input.{0}Must be one of these options (1-{1})", Environment.NewLine, maxChoice));
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
                    Console.WriteLine("Please enter Model Name");
                    infoDicToFill[eQuestions.ModelName] = Console.ReadLine();
                }

                if (infoDicToFill.ContainsKey(eQuestions.CurrentFuel))
                {
                    Console.WriteLine("Please enter Current Fuel Amount");
                    infoDicToFill[eQuestions.CurrentFuel] = Console.ReadLine();
                }

                if (infoDicToFill.ContainsKey(eQuestions.CurrentHours))
                {
                    Console.WriteLine("Please enter Current Hours in Battery");
                    infoDicToFill[eQuestions.CurrentHours] = Console.ReadLine();
                }

                if (infoDicToFill.ContainsKey(eQuestions.CurentWheelAirPressure)) 
                {
                    Console.WriteLine("Please enter Current Wheels Air Pressure");
                    infoDicToFill[eQuestions.CurentWheelAirPressure] = Console.ReadLine();
                }

                if (infoDicToFill.ContainsKey(eQuestions.WheelManufacturer))
                {
                    Console.WriteLine("Please enter Wheels Manufacturer");
                    infoDicToFill[eQuestions.WheelManufacturer] = Console.ReadLine();
                }

                if (infoDicToFill.ContainsKey(eQuestions.Doors))
                {
                    string question = string.Format("Choose Number of Doors:{0}{1}", Environment.NewLine, r_MyGarage.GetCarDoors());
                    Console.Write(question);
                    infoDicToFill[eQuestions.Doors] = Console.ReadLine();
                }

                if (infoDicToFill.ContainsKey(eQuestions.LicenseType))
                {
                    string question = string.Format("Choose License Type of your Motorcycle{0}{1}", Environment.NewLine, r_MyGarage.GetMotorcycleLicenseTypes());
                    Console.Write(question);
                    infoDicToFill[eQuestions.LicenseType] = Console.ReadLine();
                }

                if (infoDicToFill.ContainsKey(eQuestions.Color))
                {
                    string question = string.Format("Choose car's Color:{0}{1}", Environment.NewLine, r_MyGarage.GetCarColors());
                    Console.Write(question);
                    infoDicToFill[eQuestions.Color] = Console.ReadLine();
                }

                if (infoDicToFill.ContainsKey(eQuestions.HazardousMaterials))
                {
                    Console.WriteLine("Does the Truck Contain Hazardous Materials (Y/N)");
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
                    Console.WriteLine("Please enter Engine in CC");
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
                    Console.WriteLine("Enter info again:");
                }
            }
        }        
    }
}