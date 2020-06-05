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
                switch (choice)
                {
                    case 1:
                        AddNewVehicleInput();
                        break;
                    case 2:
                        //// fun 2 with input
                        break;
                    case 3:
                        //// fun 3 with input
                        break;
                    case 4:
                        //// fun 4 with input
                        break;
                    case 5:
                        //// fun 5 with input
                        break;
                    case 6:
                        //// fun 6 with input
                        break;
                    case 7:
                        PrintByLicense();
                        break;
                }

                Console.Clear();
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
7. Show vehicle details.
8.Exit THE BEST GARAGE IN TOWN");
            
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
            int vehicleChoice = 0;

            Console.WriteLine("Please enter your name");
            name = Console.ReadLine();  //// check name
            Console.WriteLine("Please enter your phone number");
            phoneNumber = Console.ReadLine();  //// check number

            Console.Write("Choose one of our supported vehicles:{0}{1}", Environment.NewLine, r_MyGarage.ShowSupportedVehicles());
            int.TryParse(Console.ReadLine(), out vehicleChoice);
            while (vehicleChoice < 1 || vehicleChoice > r_MyGarage.NumOfSupportedVehicles())
            {
                Console.WriteLine(string.Format("Wrong input.{0}Must be one of these options (1-{1})", Environment.NewLine, r_MyGarage.NumOfSupportedVehicles()));
                int.TryParse(Console.ReadLine(), out vehicleChoice);
            }

            getLicenseNumber(out licenseNumber);
            
            bool added = r_MyGarage.AddNewVehicle(name, phoneNumber, (eSupportVehicles)vehicleChoice, licenseNumber);

            if (!added) 
            {
                Console.WriteLine("This vehicle is already in THE BEST GARAGE IN TOWN! BIATCH");
            }
            else
            {
                //// need other info about car call moreinfo
                Console.WriteLine("This vehicle added successfuly to THE BEST GARAGE IN TOWN! HAVE FUN");
            }            
        }

        public void PrintByLicense()
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
    }
}