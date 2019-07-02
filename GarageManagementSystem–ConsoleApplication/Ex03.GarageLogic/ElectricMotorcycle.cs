using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ElectricMotorcycle : Motorcycle
    {
        private const float k_MaxElectricAmount = 3.2f;

        public ElectricMotorcycle(string i_ModelName, string i_LicensePlateNumber)
            :base(i_ModelName, i_LicensePlateNumber)
        {
            m_Engine = new ElectricEngine(k_MaxElectricAmount);
        }
    }
}
