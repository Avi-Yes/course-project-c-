using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    class FuelEngine : Engine
    {
        private eFuelType m_FuelType;

        public eFuelType FuelType
        {
            get { return m_FuelType; }
        }

        public FuelEngine(float i_MaxFuelAmount, eFuelType i_FuelType)
            :base(i_MaxFuelAmount)
        {
            m_FuelType = i_FuelType;
        }

        public override void EnergyAdding(params object[] i_EnergyDetailes)
        {
            if ((eFuelType)i_EnergyDetailes[1] == m_FuelType)
            {
                base.EnergyAdding(i_EnergyDetailes);
            }
            else
            {
                throw new ArgumentException(string.Format("Not same fuel type! The fuel type {0} fits to this vehicle!", m_FuelType.ToString()));
            }
            
        }

        public override string ToString()
        {
            return string.Format(
@"Energy type: Fuel
Fuel type: {0}
{1}", m_FuelType.ToString(), base.ToString());
        }
    }
}
