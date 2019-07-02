using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private string m_ManufacturerName = string.Empty;
        private float m_CurrentAirPressure = 0;
        private readonly float r_MaxAirPressure;

        public string ManufacturerName
        {
            get 
            { 
                return m_ManufacturerName; 
            }
            set
            {
                m_ManufacturerName = value;
            }
        }

        public float CurrentAirPressure
        {
            get 
            { 
                return m_CurrentAirPressure; 
            }
            set
            {
                m_CurrentAirPressure = value;
            }
        }

        public float MaxAirPressure
        {
            get 
            { 
                return r_MaxAirPressure; 
            }
        }

        public Wheel(float i_MaxAirPressure)
        {
            r_MaxAirPressure = i_MaxAirPressure;
        }

        public void InflatingAir(float i_AmountOFAirToFill)
        {
            if (i_AmountOFAirToFill + m_CurrentAirPressure <= r_MaxAirPressure)
            {
                m_CurrentAirPressure += i_AmountOFAirToFill;
            }
            else
            {
                throw new ValueOutOfRangeException(0, r_MaxAirPressure - m_CurrentAirPressure, "The Amount of Air to Fill is exceeded the maximum!");
            }
        }

        public override string ToString()
        {
            return string.Format(
@"Manufacturer Name: {0}
Air Max Pressure: {1}
Current Air Pressure: {2}", m_ManufacturerName, r_MaxAirPressure, m_CurrentAirPressure);
        }

    }
}
