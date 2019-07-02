using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public static class Validator
    {
        public static bool ValidateValuePositiveIntNum(string i_Value)
        {
            bool isPositiveNum = false;
            uint valueAsUInt;

            if (uint.TryParse(i_Value, out valueAsUInt) && valueAsUInt > 0)
            {
                isPositiveNum = true;
            }
            else
            {
                isPositiveNum = false;
                throw new ArgumentException(string.Format("Value [{0}] must be positive number!", i_Value));
            }

            return isPositiveNum;
        }

        public static bool ValidateValuePositiveFloatNum(string i_Value)
        {
            bool isPositiveNum = false;
            bool isFloatNum = false;
            float valueAsFloat;


            isFloatNum = float.TryParse(i_Value, out valueAsFloat);
            if (isFloatNum)
            {
                isFloatNum = true;

                if (valueAsFloat > 0)
                {
                    isPositiveNum = true;
                }
                else
                {
                    isPositiveNum = false;
                    throw new ArgumentException(string.Format("Value [{0}] must be positive number!", i_Value));
                }
            }
            else
            {
                isFloatNum = false;
                throw new FormatException(string.Format("Value [{0}] must be float number!", i_Value));
            }

            return isPositiveNum && isFloatNum;
        }

        public static bool ValidateValueBoolean(string i_Value)
        {
            bool valueAsBool;
            bool isValueBool = false;

            if (bool.TryParse(i_Value, out valueAsBool))
            {
                isValueBool = true;
            }
            else
            {
                isValueBool = false;
                throw new ArgumentException(string.Format("Value[{0}] must be True or False!", i_Value));
            }

            return isValueBool;
        }
        
        public static bool InColorTypeValues(string i_Value)
        {
            bool isExist = false;

            if (ValidateValuePositiveIntNum(i_Value))
            {
                if (Enum.IsDefined(typeof(eColorType), int.Parse(i_Value)))
                {
                    isExist = true;
                }
                else
                {
                    isExist = false;
                    throw new ArgumentException(string.Format("ColorType value number [{0}] does not exist!", i_Value));
                }
            }
            
            return isExist;
        }

        public static bool InDoorsAmountValues(string i_Value)
        {
            bool isExist = false;

            if (ValidateValuePositiveIntNum(i_Value))
            {
                if (Enum.IsDefined(typeof(eDoorAmount), int.Parse(i_Value)))
                {
                    isExist = true;
                }
                else
                {
                    isExist = false;
                    throw new ArgumentException(string.Format("DoorAmount value number [{0}] does not exist!", i_Value));
                }
            }

            return isExist;
        }

        public static bool InVehicleTypeValues(string i_Value)
        {
            bool isExist = false;

            if (ValidateValuePositiveIntNum(i_Value))
            {
                if (Enum.IsDefined(typeof(eVehicleType), int.Parse(i_Value)))
                {
                    isExist = true;
                }
                else
                {
                    isExist = false;
                    throw new ArgumentException(string.Format("VehicleType value number [{0}] does not exist!", i_Value));
                }
            }

            return isExist;
        }

        public static bool InFuelTypeValues(string i_Value)
        {
            bool isExist = false;

            if (ValidateValuePositiveIntNum(i_Value))
            {
                if (Enum.IsDefined(typeof(eFuelType), int.Parse(i_Value)))
                {
                    isExist = true;
                }
                else
                {
                    isExist = false;
                    throw new ArgumentException(string.Format("FuelType value number [{0}] does not exist!", i_Value));
                }
            }

            return isExist;
        }

        public static bool InLicenseTypeValues(string i_Value)
        {
            bool isExist = false;

            if (ValidateValuePositiveIntNum(i_Value))
            {
                if (Enum.IsDefined(typeof(eLicenseType), int.Parse(i_Value)))
                {
                    isExist = true;
                }
                else
                {
                    isExist = false;
                    throw new ArgumentException(string.Format("LicenseType value number [{0}] does not exist!", i_Value));
                }
            }

            return isExist;
        }

        public static bool InVehicleStatusValues(string i_Value)
        {
            bool isExist = false;

            if (ValidateValuePositiveIntNum(i_Value))
            {
                if (Enum.IsDefined(typeof(eVehicleStatus), int.Parse(i_Value)))
                {
                    isExist = true;
                }
                else
                {
                    isExist = false;
                    throw new ArgumentException(string.Format("VehicleStatus value number [{0}] does not exist!", i_Value));
                }
            }

            return isExist;
        }
    }
}
