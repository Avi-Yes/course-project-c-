using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        protected string m_ModelName;
        protected string m_LicenseNumber;
        protected List<Wheel> m_Wheels;
        protected Engine m_Engine;

        public string ModelName
        {
            get
            {
                return m_ModelName;
            }
        }

        public string LicenseNumber
        {
            get
            {
                return m_LicenseNumber;
            }
        }

        public int NumOfWheels
        {
            get
            {
                return m_Wheels.Count;
            }
        }

        public float MaxAirPressure
        {
            get
            {
                return m_Wheels[0].MaxAirPressure;
            }
        }

        public Engine Engine
        {
            get
            {
                return m_Engine;
            }
        }

        public List<Wheel> Wheels
        {
            get
            {
                return m_Wheels;
            }
        }

        public void SetAllWheels(string i_ManufacturerName)
        {
            for (int i = 0; i < m_Wheels.Count; i++)
            {
                m_Wheels[i].ManufacturerName = i_ManufacturerName;
            }
        }

        protected Vehicle(string i_ModelName, string i_LicenseNumber, float i_MaxAirPressure, int i_NumOfWheels)
        {
            m_ModelName = i_ModelName;
            m_LicenseNumber = i_LicenseNumber;
            m_Wheels = new List<Wheel>(i_NumOfWheels);
            for (int i = 0; i < i_NumOfWheels; i++)
            {
                m_Wheels.Add(new Wheel(i_MaxAirPressure));
            }
        }

        public abstract List<string> GetUniqueProperties();

        public abstract void SetUniqueProperties(List<string> i_Properties);

        public override int GetHashCode()
        {
            return m_LicenseNumber.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            bool equal = false;
            Vehicle toCompareTo = obj as Vehicle;

            if (toCompareTo != null)
            {
                equal = this.GetHashCode() == toCompareTo.GetHashCode();
            }

            return equal;
        }

        public override string ToString()
        {
            return string.Format(
@"Model Name: {0}
License Number: {1}
{2}
{3}", m_ModelName, m_LicenseNumber, m_Engine.ToString(), m_Wheels[0].ToString());
        }
    }
}
