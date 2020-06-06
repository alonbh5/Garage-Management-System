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
        private const int k_LicenseLength = 9; //// check valid by guy
 
        private readonly Garage r_MyGarage = new Garage();

        internal ConsoleUI()
        {
            int choice = 0;

            PrintOptions(out choice);

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

                //Console.Clear();
                PrintOptions(out choice);
            }
        }      

        public void PrintOptions(out int io_Choice)
        {                    
            io_Choice = 0;            
            string error = "Invalid input.\nThe input must be an option between 1-8.\n";
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
                //addInfo(vehicleChoice);
                Console.WriteLine("This vehicle added successfuly to THE BEST GARAGE IN TOWN! HAVE FUN");
            }
            else
            {
                Console.WriteLine("This vehicle is already in THE BEST GARAGE IN TOWN! BIATCH");
            }
        
        }

        private void printByFilter ()
        {
            bool seeInReapir, seePaid, seePrepared,seeAll;

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

        private void getYesOrNO (out bool io_Choice)
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

        private void changeState ()
        {
            string  lincenseInput;
            int input;
            getLicenseNumber(out lincenseInput);

            Console.WriteLine("Enter new state : 1 - In Repair 2 - Fixed 3 - Paid");
            int.TryParse(Console.ReadLine(),out input);

            while (input != 1 && input != 2 && input !=3)
            {
                Console.WriteLine("Wrong Input!  1 - In Repair | 2 - Fixed | 3 - Paid");
                int.TryParse(Console.ReadLine(), out input);
            }

            if(r_MyGarage.ChangeServiceStatus(lincenseInput,input))
            {
                Console.WriteLine("Status Updated!");
            }
            else
            {
                Console.WriteLine("Lincense does not exsit in THE BEST GARAGE IN TOWN");
            }


        }

        private void inflateToMax ()
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
            int gasType=-1;
            float amountToadd=-1f;

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

            if (r_MyGarage.FillGasTank(licensenumber,gasType,amountToadd))
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
            float amountToAdd=-1f;

            getLicenseNumber(out licenseNumber);

            Console.WriteLine("how many minutes do you want to add?");
            float.TryParse(Console.ReadLine(), out amountToAdd);
            while (amountToAdd <= 0)
            {
                Console.WriteLine("Wrong input! enter postive number");
                float.TryParse(Console.ReadLine(), out amountToAdd);
            }

            if (r_MyGarage.FillCharge(licenseNumber,amountToAdd))
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
            string input = Console.ReadLine();

            while (input.Length != k_LicenseLength ) 
            {
                Console.WriteLine(string.Format("Invalid input.{0}License number should be {0} digits", Environment.NewLine, k_LicenseLength));
                input = Console.ReadLine();
            }

            io_LicenseNumber = input;
        }
        private void getNameAndPhone(out string io_Name, out string io_Phone)
        {
            Console.WriteLine("Please enter customer name");
            io_Name = Console.ReadLine();  //// check name

            while (io_Name.Length<3)  
            {
                Console.WriteLine("Name Should be at least 3 letters, Try agian:");
                io_Phone = Console.ReadLine();  //// check number
            }

            Console.WriteLine("Please enter customer phone number");
            io_Phone = Console.ReadLine();  //// check number

            while (!int.TryParse(io_Phone,out int check) || io_Phone.Length != 10 || io_Phone[0] != '0' || io_Phone[1]!='5')
            {
                Console.WriteLine("Phone number Invalid (10 digit's in the form 05xxxxxxxx)");
                io_Phone = Console.ReadLine();  //// check number
            }
        }
        private void getSupportedVehicles(out int vehicleChoice)
        {
            vehicleChoice = 0;
            Console.Write("Choose one of our supported vehicles:{0}{1}", Environment.NewLine, r_MyGarage.ShowSupportedVehicles());
            int.TryParse(Console.ReadLine(), out vehicleChoice);
            while (vehicleChoice < 1 || vehicleChoice > r_MyGarage.NumOfSupportedVehicles())
            {
                Console.WriteLine(string.Format("Wrong input.{0}Must be one of these options (1-{1})", Environment.NewLine, r_MyGarage.NumOfSupportedVehicles()));
                int.TryParse(Console.ReadLine(), out vehicleChoice);
            }
        }
        private void addInfo (int io_Choice)
        {
            List<object> infoList = r_MyGarage.GetExtraInfo(io_Choice);
            object i = infoList.First();
        
           // while()
        }
    }
}