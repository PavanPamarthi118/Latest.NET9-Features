//// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");


using Latest.NET9_Features;

if (Feature.IsFeatureEnabled) // Checks if the feature switch is enabled
{
    Feature.IEnabledFeature(); // Calls the method if the feature is enabled
}