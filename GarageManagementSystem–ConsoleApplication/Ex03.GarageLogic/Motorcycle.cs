using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Motorcycle : Vehicle
    {
        private const int k_NumOfWheels = 2;
        private const float k_MaxAirPressure = 28f;
        protected eLicenseType m_LicenseType;
        protected int m_EngineCapacity;

        public eLicenseType LicenseType
        {
            get
            {
                return m_LicenseType;
            }
        }

        public int EngineCapcity
        {
            get
            {
                return m_EngineCapacity;
            }
        }

        protected Motorcycle(string i_ModelName, string i_LicensePlateNumber)
            : base(i_ModelName, i_LicensePlateNumber, k_MaxAirPressure, k_NumOfWheels)
        {
        }

        public override List<string> GetUniqueProperties()
        {
            List<string> uniqueProperties = new List<string>();
            StringBuilder buildProperty = new StringBuilder();

            buildProperty.AppendLine("License Type (enter the number of the option):");
            for (int i = 0; i < Enum.GetNames(typeof(eLicenseType)).Length; i++)
            {
                buildProperty.AppendLine(string.Format("{0}. {1}", i + 1, Enum.GetNames(typeof(eLicenseType))[i]));
            }

            uniqueProperties.Add(buildProperty.ToString());
            buildProperty.Remove(0, buildProperty.Length);
            buildProperty.AppendLine("Engine Capcity (positive integer number):");
            uniqueProperties.Add(buildProperty.ToString());

            return uniqueProperties;
        }

        public override void SetUniqueProperties(List<string> i_Properties)
        {
            if (Validator.InLicenseTypeValues(i_Properties[0]))
            {
                m_LicenseType = (eLicenseType)Enum.Parse(typeof(eLicenseType), i_Properties[0]);
            }

            Validator.ValidateValuePositiveIntNum(i_Properties[1]);
        }

        public override string ToString()
        {
            return string.Format(base.ToString() +
@"
License Type: {0}
Engine Capcity: {1}", m_LicenseType.ToString(), m_EngineCapacity.ToString());
        }
    }
}
