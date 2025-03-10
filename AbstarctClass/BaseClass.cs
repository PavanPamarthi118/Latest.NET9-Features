using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstarctClass
{
    public abstract class BaseClass
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public abstract int GetMonthlySalary(); // Child class will have to provide the implementation, here it will not have body
        public string GetFullName()
        {
            return FirstName + " " + LastName;
        }
    }
}
