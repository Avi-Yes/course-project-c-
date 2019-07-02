using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class FuelCar : Car
    {
        private const float k_MaxFuelAmount = 48f;
        private const eFuelType k_FuelType = eFuelType.Ocatn96;

        public FuelCar(string i_ModelName, string  i_LicensePlateNumber)
            : base(i_ModelName, i_LicensePlateNumber)
        {
            m_Engine = new FuelEngine(k_MaxFuelAmount, k_FuelType); 
        }
    }
}
