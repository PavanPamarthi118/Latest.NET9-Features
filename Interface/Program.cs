//// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

//An interface is a blueprint for a class that defines a set of methods, properties, or events without implementing them.
//It ensures that any class that implements the interface provides its own version of the defined members.

// Interface (just the rule, no implementation)

interface IAnimal
{
    void MakeSound();  // Any class using this interface MUST have this method
}

// Dog class implementing the interface
class Dog : IAnimal
{
    public void MakeSound()
    {
        Console.WriteLine("Woof! Woof!");
    }
}

// Cat class implementing the interface
class Cat : IAnimal
{
    public void MakeSound()
    {
        Console.WriteLine("Meow!");
    }
}

class Program
{
    static void Main()
    {
        IAnimal myDog = new Dog();
        IAnimal myCat = new Cat();

        myDog.MakeSound();  // Output: Woof! Woof!
        myCat.MakeSound();  // Output: Meow!
    }
}


//Multiple Interfaces


/*using System;

// First interface: Defines walking behavior
interface IWalkable
{
    void Walk();
}

// Second interface: Defines talking behavior
interface ITalkable
{
    void Talk();
}

// Robot class implementing BOTH interfaces
class Robot : IWalkable, ITalkable
{
    public void Walk()
    {
        Console.WriteLine("Robot is walking...");
    }

    public void Talk()
    {
        Console.WriteLine("Robot is talking...");
    }
}

class Program
{
    static void Main()
    {
        Robot myRobot = new Robot();

        myRobot.Walk(); // Output: Robot is walking...
        myRobot.Talk(); // Output: Robot is talking...
    }
}*/
