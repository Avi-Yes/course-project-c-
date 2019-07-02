using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public  static class VehicleCreator
    {
        public static Vehicle CreateVehicle(eVehicleType i_VehicleType, string i_ModelName, string i_LicenseNumber)
        {
            Vehicle vehicleToCreate = null;

            switch (i_VehicleType)
            {
                case eVehicleType.FuelCar:
                    vehicleToCreate = new FuelCar(i_ModelName, i_LicenseNumber);
                    break;
                
                case eVehicleType.ElectricCar:
                    vehicleToCreate = new ElectricCar(i_ModelName, i_LicenseNumber);
                    break;
                
                case eVehicleType.FuelMotorcycle:
                    vehicleToCreate = new FuelMotorcycle(i_ModelName, i_LicenseNumber);
                    break;
                
                case eVehicleType.ElectricMotorcycle:
                    vehicleToCreate = new ElectricMotorcycle(i_ModelName, i_LicenseNumber);
                    break;
                
                case eVehicleType.FuelTruck:
                    vehicleToCreate = new FuelTruck(i_ModelName, i_LicenseNumber);
                    break;

                default:
                    break;
            }

            return vehicleToCreate;
        }
    }
}
