using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class GarageUI
    {
        private readonly GarageManagement r_GarageManagement;

        public GarageUI()
        {
            r_GarageManagement = new GarageManagement();
        }

        private string getUserMainMenu()
        {
            return string.Format(
@"Please choose one of the options below:
 
1 - Insert a new car into the garage
2 - Display the list of vehicle license numbers in the garage
3 - Change the status of a car in the garage
4 - Inflate the air in the wheels of a vehicle to the maximum
5 - Refuel a Fuel vehicle 
6 - Recharge an electric vehicle
7 - View full detailes of a vehicle
8 - Quit
");
        }

        public void InsertionHandling()//1
        {
            string licenseNumber;

            System.Console.WriteLine("Enter the licesne Number:");
            licenseNumber = System.Console.ReadLine();
            if (r_GarageManagement.IsVehicleExist(licenseNumber))
            {
                r_GarageManagement.ChangeVehicleStatus(licenseNumber, eVehicleStatus.InRepair);
                System.Console.WriteLine("{0}Vehicle already in the garage!",Environment.NewLine);
            }
            else
            {
                insertNewVehicle(licenseNumber);
            }

            System.Console.WriteLine("{0}Press any key to go back to main menu...", Environment.NewLine);
            System.Console.ReadLine();
        }

        private void insertNewVehicle(string i_LicenseNumber)
        {
            eVehicleType userVehicleTypeChoice;
            Vehicle vehicleToCreate;
            string vehicleModelName;
            string wheelManufacturerName;
            string ownerName;
            string ownerPhoneNumber;
            
            System.Console.WriteLine("{0}--Vehicle Details--", Environment.NewLine);
            System.Console.WriteLine("{0}Enter vehicle model name:", Environment.NewLine);
            vehicleModelName = System.Console.ReadLine();
            userVehicleTypeChoice = getUserRequestToVehicleTypeChoice();
            vehicleToCreate = VehicleCreator.CreateVehicle(userVehicleTypeChoice, vehicleModelName, i_LicenseNumber);
            setUserVehicleProperties(vehicleToCreate);
            System.Console.WriteLine("{0}Enter Owner name:", Environment.NewLine);
            ownerName = System.Console.ReadLine();
            System.Console.WriteLine("{0}Enter Owner phone number name:", Environment.NewLine);
            ownerPhoneNumber = System.Console.ReadLine();
            System.Console.WriteLine("{0}Enter wheel manufacturer name:", Environment.NewLine);
            wheelManufacturerName = System.Console.ReadLine();
            vehicleToCreate.SetAllWheels(wheelManufacturerName);
            r_GarageManagement.AddVehicle(ownerName, ownerPhoneNumber, vehicleToCreate);
            System.Console.WriteLine("{0}Vehicle added successfully!", Environment.NewLine);

        }

        private eVehicleType getUserRequestToVehicleTypeChoice()
        {
            bool isValid = false;
            string userChoice = string.Empty;
            string[] enumValues;

            System.Console.WriteLine("{0}Vehicle Type (enter the number of the option):", Environment.NewLine);
            enumValues = Enum.GetNames(typeof(eVehicleType));
            for (int i = 0; i < Enum.GetNames(typeof(eVehicleType)).Length; i++)
            {
                System.Console.WriteLine("{0}. {1}", i + 1, enumValues[i]);
            }

            do
            {
                try
                {
                    //System.Console.WriteLine("{0}Your choice:", Environment.NewLine);
                    userChoice = System.Console.ReadLine();
                    Validator.InVehicleTypeValues(userChoice);
                    isValid = true;
                }
                catch (ArgumentException ae)
                {
                    System.Console.WriteLine(string.Format(ae.Message + " Try to enter valid option"));
                    isValid = false;
                }
                catch (FormatException fe)
                {
                    System.Console.WriteLine(string.Format(fe.Message + " Try to enter valid option"));
                    isValid = false;
                }
            }
            while (!isValid);

            return (eVehicleType)Enum.Parse(typeof(eVehicleType), enumValues[int.Parse(userChoice) - 1]);
        }

        private void setUserVehicleProperties(Vehicle i_Vehicle)
        {
            List<string> vehicleProperties = i_Vehicle.GetUniqueProperties();
            List<string> userVehicleProperties;
            string userChoice;
            bool isValid = false;

            do
            {
                userVehicleProperties = new List<string>();
                foreach (string vehiclePropery in vehicleProperties)
                {
                    System.Console.Write(vehiclePropery);
                    //System.Console.WriteLine("{0}Your Choice:", Environment.NewLine);
                    userChoice = System.Console.ReadLine();
                    userVehicleProperties.Add(userChoice);
                }
                try
                {
                    i_Vehicle.SetUniqueProperties(userVehicleProperties);
                    isValid = true;
                }
                catch (ArgumentException ae)
                {
                    System.Console.WriteLine(ae.Message + " try to enter valid option");
                    isValid = false;
                }
                catch (FormatException fe)
                {
                    System.Console.WriteLine(fe.Message + " try to enter valid option");
                    isValid = false;
                }
            }
            while (!isValid);
        }

        public void DisplayVehicleLicsensNumbersHandling()//2
        {
            bool toFiltered = false;
            eVehicleStatus filterBy;
            List<string> licenseNumbers = new List<string>();

            toFiltered = GetUserRequestToFilter();
            if (!toFiltered)
            {
                licenseNumbers = r_GarageManagement.GetAllLicenseNumbers();
            }
            else
            {
                filterBy = getUserRequestOfVehicleStatus();
                licenseNumbers = r_GarageManagement.GetFilteredLicenseNumbers(filterBy);
            }

            if (licenseNumbers.Count == 0)
            {
                System.Console.WriteLine("There are no vehicles in the garage or there are no vehicles with this status!");
            }
            else
            {
                System.Console.WriteLine("{0}License Numbers:", Environment.NewLine);
                foreach (string licenseNumber in licenseNumbers)
                {
                    System.Console.WriteLine(licenseNumber.ToString());
                }
            }

            System.Console.WriteLine("{0}Press any key to go back to main menu...", Environment.NewLine);
            System.Console.ReadLine();
        }

        public bool GetUserRequestToFilter()
        {
            string userChoice;
            bool isValidOption = false;
            
            System.Console.WriteLine(string.Format(
@"License numbers to display(enter option number):
1. All license numbers
2. Filtered license numbers by the vehicle status
"));
            do
            {
                //System.Console.WriteLine("Your Choice:");
                userChoice = System.Console.ReadLine();
                try
                {
                    Validator.ValidateValuePositiveIntNum(userChoice);
                    if (userChoice == "1" || userChoice == "2")
                    {
                        isValidOption = true;
                    }
                    else
                    {
                        System.Console.WriteLine("{0}Your choice must be bewtween 1-2!{0}", Environment.NewLine);
                        isValidOption = false;
                    }
                }
                catch(ArgumentException ae)
                {
                    System.Console.WriteLine(ae.Message);
                    isValidOption = false;
                }
            }
            while (!isValidOption);

            return  userChoice == "1" ? false : true;
        }

        private eVehicleStatus getUserRequestOfVehicleStatus()
        {
            string userVehicleStatusOption;
            bool isValidOption = false;
            string[] enumValues;

            System.Console.WriteLine("{0}Choose Vehicle Status (enter the number of the option):", Environment.NewLine);
            enumValues = Enum.GetNames(typeof(eVehicleStatus));
            for (int i = 0; i < enumValues.Length; i++)
            {
                System.Console.WriteLine("{0}. {1}", i + 1, enumValues[i]);
            }

            do
            {
                System.Console.WriteLine("{0}Your choice:",Environment.NewLine);
                userVehicleStatusOption = System.Console.ReadLine();
                try
                {
                    Validator.InVehicleStatusValues(userVehicleStatusOption);
                    isValidOption = true;
                }
                catch(ArgumentException fe)
                {
                    System.Console.WriteLine(string.Format(fe.Message + " Try to enter valid option"));
                }
                catch(FormatException fe)
                {
                    System.Console.WriteLine(string.Format(fe.Message + " Try to enter valid option"));
                }
            }
            while(!isValidOption);

            return (eVehicleStatus)Enum.Parse(typeof(eVehicleStatus), enumValues[int.Parse(userVehicleStatusOption) - 1]);;
        }
        

        public void ChangeVehicleStatusHandling() //3
        {
            string licenseNumber;
            eVehicleStatus newVehicleStatus;

            System.Console.WriteLine("Enter the licesne number:");
            licenseNumber = System.Console.ReadLine();
            if (r_GarageManagement.IsVehicleExist(licenseNumber))
            {
                newVehicleStatus = getUserRequestOfVehicleStatus();
                r_GarageManagement.ChangeVehicleStatus(licenseNumber, newVehicleStatus);
                System.Console.WriteLine("Vehicle Status changed successfully!");
            }
            else
            {
                System.Console.WriteLine("There is no vehicle in the garage with this license number!");
            }

            System.Console.WriteLine("{0}Press any key to go back to main menu...", Environment.NewLine);
            System.Console.ReadLine();
        }

        public void InflateWheelsToMaxHandling()//4
        {
            string licenseNumber;

            System.Console.WriteLine("Enter the licesne number:");

            licenseNumber = System.Console.ReadLine();
            if (r_GarageManagement.IsVehicleExist(licenseNumber))
            {
                r_GarageManagement.InflatingAirInWheelsToMax(licenseNumber);
                System.Console.WriteLine("{0}Vehicle wheels inflated to max successfully!", Environment.NewLine);
            }
            else
            {
                System.Console.WriteLine("There is no vehicle in the garage with this license number!");
            }

            System.Console.WriteLine("{0}Press any key to go back to main menu...", Environment.NewLine);
            System.Console.ReadLine();
        }

        public void RefuelHandling()//5
        {
            string licenseNumber;
            eFuelType fuelType;
            float fuelAmount;
            bool isValidProperties = false;

            
            System.Console.WriteLine("Enter the licesne Number:");
            licenseNumber = System.Console.ReadLine();

            if (r_GarageManagement.IsVehicleExist(licenseNumber))
            {
                do
                {
                    try
                    {
                        fuelType = GetUserFuelType();
                        fuelAmount = getEnergyAmount("Fuel");
                        r_GarageManagement.RefuelVehicle(licenseNumber, fuelAmount, fuelType);
                        isValidProperties = true;
                        System.Console.WriteLine("{0}The vehicle was refueled successfully!", Environment.NewLine);
                    }
                    catch (ArgumentException ae)
                    {
                        System.Console.WriteLine(ae.Message);
                        //go back to main menu(exception is thrown because not a Fuel Vehicle) 
                        if (ae.ParamName == "ErrorEnegineType")
                        {
                            System.Console.WriteLine(ae.Message);
                            break;
                        }
                        else
                        {
                            isValidProperties = false;
                        }
                        
                    }
                    catch (ValueOutOfRangeException vre)
                    {
                        System.Console.WriteLine(string.Format(vre.Message + " The max fuel to add is: {0}.", vre.MaxValue));
                        isValidProperties = false;
                    }
                }
                while (!isValidProperties);
                
            }
            else
            {
                System.Console.WriteLine("There is no vihicle in the garage with this license number!");
            }

            System.Console.WriteLine("{0}Press any key to go back to main menu...", Environment.NewLine);
            System.Console.ReadLine();
        }

        private eFuelType GetUserFuelType()
        {
            string userFuelTypeOption;
            bool isValidOption = false;
            string[] enumValues;

            System.Console.WriteLine("{0}Choose Fuel type (enter the number of the option):", Environment.NewLine);
            enumValues = Enum.GetNames(typeof(eFuelType));
            for (int i = 0; i < enumValues.Length; i++)
            {
                System.Console.WriteLine("{0}. {1}", i + 1, enumValues[i]);
            }
            
            do
            {
                //System.Console.WriteLine("{0}Your choice:", Environment.NewLine);
                userFuelTypeOption = System.Console.ReadLine();
                try
                {
                    Validator.InFuelTypeValues(userFuelTypeOption);
                    isValidOption = true;
                }
                catch (ArgumentException fe)
                {
                    System.Console.WriteLine(string.Format(fe.Message + " Try to enter valid option"));
                }
                catch (FormatException fe)
                {
                    System.Console.WriteLine(string.Format(fe.Message + " Try to enter valid option"));
                }
            }
            while (!isValidOption);

            return (eFuelType)Enum.Parse(typeof(eFuelType), enumValues[int.Parse(userFuelTypeOption) - 1]); ;
        }

        private float getEnergyAmount(string i_EnergyTypeStr)
        {
            bool isValid = false;
            string userEnergyAmountChoiceAsStr;
            
            do
            {
                System.Console.WriteLine(string.Format("{0}Amount of " + i_EnergyTypeStr + " to add:", Environment.NewLine));
                userEnergyAmountChoiceAsStr = System.Console.ReadLine();
                try
                {
                    isValid = Validator.ValidateValuePositiveFloatNum(userEnergyAmountChoiceAsStr);
                }
                catch(ArgumentException ae)
                {
                    System.Console.WriteLine(string.Format(ae.Message + " Try to enter valid value"));
                    isValid = false;
                }
                catch(FormatException fe)
                {
                    System.Console.WriteLine(string.Format(fe.Message + " Try to enter valid value"));
                    isValid = false;
                }
               
            }
            while(!isValid);

            return float.Parse(userEnergyAmountChoiceAsStr);
        }

        public void RechargeHandling()//6
        {
            string licenseNumber;
            float timeAmount;
            bool isValidProperty;

            System.Console.WriteLine("Enter the licesne Number:");
            licenseNumber = System.Console.ReadLine();
            if (r_GarageManagement.IsVehicleExist(licenseNumber))
            {
                do
                {
                    try
                    {
                        timeAmount = getEnergyAmount("Time");
                        r_GarageManagement.RechargeVehicle(licenseNumber, timeAmount);
                        isValidProperty = true;
                        System.Console.WriteLine("{0}The vehicle was recharged successfully!", Environment.NewLine);
                    }
                    catch (ValueOutOfRangeException vre)
                    {
                        System.Console.WriteLine(string.Format(vre.Message + " The max time to add is: {0}.", vre.MaxValue));
                        isValidProperty = false;
                    }
                    catch (ArgumentException ae)
                    {
                        //go back to main menu(exception is thrown because not a Electric Vehicle)
                        if (ae.ParamName == "ErrorEnegineType")
                        {
                            System.Console.WriteLine(ae.Message);
                            break;
                        }
                        else
                        {
                            System.Console.WriteLine(ae.Message);
                            isValidProperty = false;
                        }
                    }
                }
                while (!isValidProperty);
            }
            else
            {
                System.Console.WriteLine("There is no vehicle in the garage with this license number!");
            }

            System.Console.WriteLine("{0}Press any key to go back to main menu...", Environment.NewLine);
            System.Console.ReadLine();
        }

        public void DisplayVehicleDetailsHandling()//7
        {
            string licenseNumber;

            System.Console.WriteLine("Enter the licesne Number:");
            licenseNumber = System.Console.ReadLine();
            if (r_GarageManagement.IsVehicleExist(licenseNumber))
            {
                System.Console.WriteLine(r_GarageManagement.GetFullVehicleDetails(licenseNumber));
            }
            else
            {
                System.Console.WriteLine("There is no vihicle in the garage with this license number!");
            }

            System.Console.WriteLine("{0}Press any key to go back to main menu...", Environment.NewLine);
            System.Console.ReadLine();

        }

        private void actionToPerform(int i_ActionNumber)
        {
             switch (i_ActionNumber)
            {
                case 1: 
                    InsertionHandling();
                    System.Console.Clear();
                    break;
                case 2:
                    DisplayVehicleLicsensNumbersHandling();
                    System.Console.Clear();
                    break;
                case 3:
                    ChangeVehicleStatusHandling();
                    System.Console.Clear();
                    break;
                case 4:
                    InflateWheelsToMaxHandling();
                    System.Console.Clear();
                    break;
                case 5:
                    RefuelHandling();
                    System.Console.Clear();
                    break;
                case 6:
                    RechargeHandling();
                    System.Console.Clear();
                    break;
                case 7:
                    DisplayVehicleDetailsHandling();
                    System.Console.Clear();
                    break;
                default:
                    break;
            }
        }

        public void Run()
        {
            string userChoice;
            int choiceNum; 
            bool toContinue = true;

            do
            {
                System.Console.WriteLine(getUserMainMenu());
                userChoice = System.Console.ReadLine();
                try
                {
                    Validator.ValidateValuePositiveIntNum(userChoice);
                    choiceNum = int.Parse(userChoice);
                    if (choiceNum == 8)
                    {
                        toContinue = false;
                    }
                    else
                    {
                        System.Console.Clear();
                        actionToPerform(choiceNum);
                        toContinue = true;
                    }
                }
                catch (ArgumentException ae)
                {
                    System.Console.WriteLine(ae.Message + " and bewtween 1-8");
                    toContinue = true;
                    System.Console.Clear();
                }
            }
            while (toContinue);

            System.Console.WriteLine("You choosed to quit Bye Bye!{0}press any key to Exit..", Environment.NewLine);
            System.Console.ReadLine();

        }
    }
}