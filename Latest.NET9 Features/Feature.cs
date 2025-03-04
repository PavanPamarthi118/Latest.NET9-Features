
//Feature switching is a new capability in .NET 9 that allows developers to manage features dynamically during application runtime. 
//This enhances flexibility by enabling or disabling features based on specific conditions or testing requirements.

//So it's basically feature flagging but with Microsoft builtin support

using System.Diagnostics.CodeAnalysis;

namespace Latest.NET9_Features
{
    public class Feature
    {
        // Defines a feature switch using an attribute.
        // This allows conditional enabling/disabling of a feature at runtime.
        [FeatureSwitchDefinition("Feature.IsEnabled")]
        internal static bool IsFeatureEnabled =>
            // Checks if the feature switch "Feature.IsEnabled" is set in AppContext.
            // If set, assigns its value to 'isEnabled', otherwise defaults to 'true'.
            AppContext.TryGetSwitch("Feature.IsEnabled", out var isEnabled) ? isEnabled : true;

        internal static void IEnabledFeature()
        {
            // Outputs a message to indicate the feature is enabled.
            Console.WriteLine("I enabled the Feature!");
        }
    }
}