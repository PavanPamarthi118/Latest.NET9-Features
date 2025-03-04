// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

var dictionary = new Dictionary<string, int>
{
    { "Hello", 69 },
    { "World!", 420 }

};

var helloWorld = "Hello, World!";

//******** Traditional Approach (Before .NET 9)******** //
Console.WriteLine(dictionary[helloWorld.Split(',')[0]]); // Output: 69

// Issues with Traditional Approach:
// 1. `helloWorld.Split(',')` creates anew string array in memory.
// 2. Each word in the array (`"Hello"` and `" World!"`) is stored asa new string instance.
// 3.Unnecessary memory allocations make this inefficient, especially in high-performance applications.

//****************************************************** //

//******** Optimized Approach with ReadOnlySpan<char> (Introduced in .NET 9)******** //

// Memory-Efficient Solution: Using `ReadOnlySpan<char>` to avoid allocations.
ReadOnlySpan<char> helloWorldSpan = "Hello, World!";

// Splitting without creating new string objects (uses spans instead)
var splitRanges = helloWorldSpan.Split(',');

// Using Dictionary Alternate Lookup (New .NET 9 Feature)
var altDictionary = dictionary.GetAlternateLookup<ReadOnlySpan<char>>();

// Efficient Lookup Without Allocations
foreach (var range in splitRanges)
{
    ReadOnlySpan<char> key = helloWorldSpan[range].Trim(); // Extract substring as ReadOnlySpan<char>
    Console.WriteLine(altDictionary[key]); // Output: 69, 420
}

//****************************************************** //

//******** Why is the .NET 9 Approach Better?******** //
// No new string allocations → We work with spans instead of creating new string objects.
// Memory-efficient → No temporary arrays or string instances are created.
// Faster Execution → Direct lookup using `ReadOnlySpan<char>` improves performance.
// Less Garbage Collection (GC) Pressure → Fewer temporary objects = Less GC work.

//****************************************************** //

//******** New ReadOnlySet<T> Feature in .NET 9******** //
var hasSet = new HashSet<string>  { "Sherin", "Jafer", "sai" }; // HashSet<T> is a collection that stores unique elements and provides fast lookups.
var readOnlySet = new ReadOnlySet<string>(hasSet); // Creating a read-only wrapper
Console.WriteLine(readOnlySet.Contains("Sai"));

// What is `ReadOnlySet<T>`?
// - Providesa read-only wrapper around a `HashSet<T>`.
// - Ensuresdata safety by preventing modifications while still allowing lookups.
// - Useful when you want to expose a collection without allowing changes.