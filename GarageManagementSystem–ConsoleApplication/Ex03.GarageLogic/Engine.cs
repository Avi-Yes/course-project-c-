using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Engine
    {
        protected float m_EnergyAmountLeft = 0f;
        protected float m_MaxEngeryAmount;//readOnly?

        public float EnergyAmountLeft
        {
            get { return m_EnergyAmountLeft; }
        }

        public float MaxEnergyAmount
        {
            get { return m_MaxEngeryAmount; }
        }

        protected Engine(float i_MaxEngeryAmount)
        {
            m_MaxEngeryAmount = i_MaxEngeryAmount;
        }

        public virtual void EnergyAdding(params object[] i_EnergyDetailes)
        {

            if ((float)i_EnergyDetailes[0] + m_EnergyAmountLeft <= m_MaxEngeryAmount)
            {
                m_EnergyAmountLeft += (float)i_EnergyDetailes[0];
            }
            else
            {
                throw new ValueOutOfRangeException(0, m_MaxEngeryAmount - m_EnergyAmountLeft, "The amount of energy to add exceeded the maximum!");
            }
        }

        public float GetPercentageEnergyLeft()
        {
            return (m_MaxEngeryAmount / m_EnergyAmountLeft) * 100; 
        }

        public override string ToString()
        {
            return string.Format(
@"Energy amount left: {0:0.00}
Max Energy Amount {1:0.00}", m_EnergyAmountLeft, m_MaxEngeryAmount);
        }
    }
}
