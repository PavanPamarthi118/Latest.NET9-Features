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

//🚀 When Should I Use an Abstract Class?
//✅ When you want some shared implementation but also want to force derived classes to implement specific behaviors.
//✅ When you don’t want to allow object creation of the base class.