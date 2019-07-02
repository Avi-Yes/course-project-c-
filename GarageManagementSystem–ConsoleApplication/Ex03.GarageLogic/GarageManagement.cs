using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class GarageManagement
    {
        //key = LicenseNumber , value = VichleInGarage
        private readonly Dictionary<string, VehicleInGarage> r_VehicleInGarage;

        public Dictionary<string, VehicleInGarage> VehicleInGarage
        {
            get
            {
                return r_VehicleInGarage;
            }
        }

        public GarageManagement()
        {
            r_VehicleInGarage = new Dictionary<string, VehicleInGarage>();
        }

        public void AddVehicle(string i_OwnerName, string i_OwnerPhoneNumber, Vehicle i_Vehicle)
        {
            VehicleInGarage garageWorkCardToAdd = new VehicleInGarage(i_OwnerName, i_OwnerPhoneNumber, i_Vehicle);
            r_VehicleInGarage.Add(i_Vehicle.LicenseNumber, garageWorkCardToAdd);
        }

        public List<string> GetAllLicenseNumbers()
        {
            List<string> licenseNumbers = new List<string>(r_VehicleInGarage.Count);
 
            foreach (KeyValuePair<string, VehicleInGarage> entry in r_VehicleInGarage)
            {
                licenseNumbers.Add(entry.Key);
            }

            return licenseNumbers;
        }

        public List<string> GetFilteredLicenseNumbers(eVehicleStatus i_FilterStatus)
        {
            List<string> licenseNumbers = new List<string>(r_VehicleInGarage.Count);
            
            foreach (KeyValuePair<string, VehicleInGarage> entry in r_VehicleInGarage)
            {
                if (entry.Value.VehicleStatus == i_FilterStatus)
                {
                    licenseNumbers.Add(entry.Key);
                }
            }

            return licenseNumbers;
        }

        public bool ChangeVehicleStatus(string i_LicenseNumber, eVehicleStatus i_StatusToChangeTo)
        {
            VehicleInGarage valueToUpDate;
            bool successFlag = false;

            r_VehicleInGarage.TryGetValue(i_LicenseNumber, out valueToUpDate);
            if (valueToUpDate != null)
            {
                r_VehicleInGarage[i_LicenseNumber].VehicleStatus = i_StatusToChangeTo;
                successFlag = true;
            }

            return successFlag;
        }

        public void InflatingAirInWheelsToMax(string i_LicenseNumber)
        {
            float amountOfAirToAdd = r_VehicleInGarage[i_LicenseNumber].Vehicle.Wheels[0].MaxAirPressure - r_VehicleInGarage[i_LicenseNumber].Vehicle.Wheels[0].CurrentAirPressure;

            foreach (Wheel wheel in r_VehicleInGarage[i_LicenseNumber].Vehicle.Wheels)
            {
                wheel.InflatingAir(amountOfAirToAdd);
            }
        }

        //same parameters, can unite
        public void RefuelVehicle(string i_LicenseNumber, params object[] i_FuelDetailes)
        {
            if (r_VehicleInGarage[i_LicenseNumber].Vehicle.Engine is FuelEngine)
            {
                r_VehicleInGarage[i_LicenseNumber].Vehicle.Engine.EnergyAdding(i_FuelDetailes);
            }
            else
            {
                throw new ArgumentException("This vehicle can not be refuled!", "ErrorEnegineType");
            }
        }

        public void RechargeVehicle(string i_LicenseNumber, params object[] i_ElectricDetailes)
        {
            if (r_VehicleInGarage[i_LicenseNumber].Vehicle.Engine is ElectricEngine)
            {
                r_VehicleInGarage[i_LicenseNumber].Vehicle.Engine.EnergyAdding(i_ElectricDetailes);
            }
            else
            {
                throw new ArgumentException("This vehicle can not be charged!", "ErrorEnegineType");
            }
        }

        public string GetFullVehicleDetails(string i_LicenseNumber)
        {
            return r_VehicleInGarage[i_LicenseNumber].ToString();
        }

        public bool IsVehicleExist(string i_LicenseNumber)
        {
            return r_VehicleInGarage.ContainsKey(i_LicenseNumber);
        }
    }
}
