using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class FuelTruck : Truck
    {
        private const float k_MaxFuelAmount = 105f;
        private const eFuelType k_FuelType = eFuelType.Soler;

        public FuelTruck(string i_ModelName, string i_LicensePlateNumber)
            :base(i_ModelName, i_LicensePlateNumber)
        {
            m_Engine = new FuelEngine(k_MaxFuelAmount, k_FuelType);
        }
    }
}
