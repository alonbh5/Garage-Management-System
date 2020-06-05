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
            int vehicleChoice;

            getNameAndPhone(out name, out phoneNumber);

            getSupportedVehicles(out vehicleChoice);

            getLicenseNumber(out licenseNumber);
            
            bool added = r_MyGarage.AddNewVehicle(name, phoneNumber, (eSupportVehicles)vehicleChoice, licenseNumber);

            if (r_MyGarage.AddNewVehicle(name, phoneNumber, (eSupportVehicles)vehicleChoice, licenseNumber)) 
            {
                updateInfo();
                Console.WriteLine("This vehicle added successfuly to THE BEST GARAGE IN TOWN! HAVE FUN");                
            }
            else
            {
                Console.WriteLine("This vehicle is already in THE BEST GARAGE IN TOWN! BIATCH");
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

            while (!int.TryParse(io_Phone,out int check) || io_Phone.Length != 9 || io_Phone[0] != '0' || io_Phone[1]!='5')
            {
                Console.WriteLine("Phone number Invalid (9 digit's in the form 05xxxxxxx)");
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
        private void updateInfo ()
        {

        }
    }
}