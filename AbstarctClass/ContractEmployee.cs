using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstarctClass
{
    class ContractEmployee : BaseClass
    {
        //In C#, get and set are accessors used in properties to control access to a class's fields.
        //They allow you to read (get) and modify (set) the value of a property.
        //get → Returns the value of the property.
        //set → Assigns a new value to the property.

        public int Hourlypay { get; set; }
        public int HoursWorked { get; set; }

        public override int GetMonthlySalary()
        {
            return Hourlypay * HoursWorked;
        }
    }
}
