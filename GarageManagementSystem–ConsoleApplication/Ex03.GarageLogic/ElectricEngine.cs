using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ElectricEngine : Engine
    {
        public ElectricEngine(float i_MaxBatteryAmount)
            :base(i_MaxBatteryAmount)
        {
        }

        public override string ToString()
        {
            return string.Format(
@"Energy type: Electric
" + base.ToString());
        }
    }
}
