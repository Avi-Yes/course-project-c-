using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    class ElectricCar : Car
    {
        private const float k_MaxElectricAmount = 4.8f;

        public ElectricCar(string i_ModelName, string i_LicensePlateNumber)
            :base(i_ModelName, i_LicensePlateNumber)
        {
            m_Engine = new ElectricEngine(k_MaxElectricAmount);
        }

    }
}
