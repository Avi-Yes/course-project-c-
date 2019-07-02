using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Truck : Vehicle
    {
        private const int k_NumOfWheels = 16;
        private const float k_MaxAirPressure = 32f;
        protected bool m_IsTrunkCooled;
        protected float m_TrunkCapcity;

        public bool IsTrunkCooled
        {
            get
            {
                return m_IsTrunkCooled;
            }
        }

        public float TrunkCapcity
        {
            get
            {
                return m_TrunkCapcity;
            }
        }

        protected Truck(string i_ModelName, string i_LicensePlateNumber)
            :base(i_ModelName, i_LicensePlateNumber, k_MaxAirPressure, k_NumOfWheels)
        {
        }

        public override List<string> GetUniqueProperties()
        {
            List<string> uniqueProperties = new List<string>();

            uniqueProperties.Add(string.Format("Is Truck Cooled (write one of the options) {1}/{2}:{0}", Environment.NewLine, bool.TrueString, bool.FalseString));
            uniqueProperties.Add(string.Format("{0}Truck Capcity(positive float number):{0}", Environment.NewLine));
            return uniqueProperties;
        }

        public override void SetUniqueProperties(List<string> i_Properties)
        {
            Validator.ValidateValueBoolean(i_Properties[0]);
            Validator.ValidateValuePositiveFloatNum(i_Properties[1]);
        }

        public override string ToString()
        {
            return string.Format(base.ToString() +
@"
Is Trunk Cooled : {0}
Trunk Capcity: {1}", m_IsTrunkCooled.ToString(), m_TrunkCapcity.ToString());
        }
    }
}
