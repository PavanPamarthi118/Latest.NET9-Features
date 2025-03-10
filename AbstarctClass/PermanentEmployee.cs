using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstarctClass
{
    class PermanentEmployee : BaseClass
    {

        public int AnnualSalary { get; set; }
        public override int GetMonthlySalary() // Using Override we are impelemting the abstact method
        {
            return AnnualSalary / 12;
        }
    }
}