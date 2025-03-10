//// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

/* An abstract class in C# is a class that cannot be instantiated and is meant to be inherited by other classes. 
It can have abstract methods (without implementation) and non-abstract methods (with implementation). 

 The purpose of a abstarct class is to provide a common definition of a base class that multiple derived classes can share
 */
using AbstarctClass;

namespace AbstarctClass
{
    class Program
    {
        static void Main(string[] args)
        {
            //BaseClass bc = new BaseClass()
            //{
            //    ID = 1,
            //    FirstName = "ABC",
            //    LastName = "XYZ"
            //}; // Can't instanciate it, coz it's abstarct
            ContractEmployee cc = new ContractEmployee()
            {
                ID = 1,
                FirstName = "kld",
                LastName = "jvi",
                Hourlypay = 556,
                HoursWorked = 160
            };
            Console.WriteLine( cc.GetFullName());
            Console.WriteLine(cc.GetMonthlySalary());
            PermanentEmployee pc = new PermanentEmployee()
            {
                ID = 1,
                FirstName = "odk",
                LastName = "ppp",
                AnnualSalary = 1250000
            };
            Console.WriteLine(pc.GetFullName());
            Console.WriteLine(pc.GetMonthlySalary());
        }
    }
}