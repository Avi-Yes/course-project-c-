using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Car : Vehicle
    {
        private const int k_NumOfWheels = 4;
        private const float k_MaxAirPressure = 30f;
        protected eColorType m_Color;
        protected eDoorAmount m_NumOfDoors;

        public eColorType Color
        {
            get
            {
                return m_Color;
            }
            set
            {
                m_Color = value;
            }
        }

        public eDoorAmount NumOfDoors
        {
            get
            {
                return m_NumOfDoors;
            }
        }


        protected Car(string i_ModelName, string  i_LicensePlateNumber)
            : base(i_ModelName, i_LicensePlateNumber, k_MaxAirPressure, k_NumOfWheels)
        {
 
        }

        public override List<string> GetUniqueProperties()
        {
            List<string> uniqueProperties = new List<string>();
            StringBuilder buildProperty = new StringBuilder();

            buildProperty.AppendLine("Color (enter the number of the option):");
            for (int i = 0; i < Enum.GetNames(typeof(eColorType)).Length; i++)
            {
                buildProperty.AppendLine(string.Format("{0}. {1}", i + 1, Enum.GetNames(typeof(eColorType))[i]));
            }
             
            uniqueProperties.Add(buildProperty.ToString());
            buildProperty.Remove(0, buildProperty.Length);
            buildProperty.AppendLine("Number Of Doors (enter the number of the option):");
            for (int i = 0; i < Enum.GetNames(typeof(eDoorAmount)).Length; i++)
            {
                buildProperty.AppendLine(string.Format("{0}. {1}", i + 1, Enum.GetNames(typeof(eDoorAmount))[i]));
            }
            uniqueProperties.Add(buildProperty.ToString());
            return uniqueProperties;
        }

        public override void SetUniqueProperties(List<string> i_Properties)
        {   
            if (Validator.InColorTypeValues(i_Properties[0]))
            {
                m_Color = (eColorType)Enum.Parse(typeof(eColorType), i_Properties[0]);
            }
            
            if (Validator.InDoorsAmountValues(i_Properties[1]))
            {
                m_NumOfDoors = (eDoorAmount)Enum.Parse(typeof(eDoorAmount), i_Properties[1]);
            }
        }

        public override string ToString()
        {
            return string.Format(base.ToString() + 
@"
Color: {0}
Number Of Doors: {1}", m_Color.ToString(), m_NumOfDoors.ToString());
        }
    }
}
