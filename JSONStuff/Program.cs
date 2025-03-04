// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using System.Text.Json;
using System.Text.Json.Serialization;
using NJsonSchema;

public class Program
{
    public static async Task Main(string[] args)
    {
        // ------------------------ 1. Nullable Reference Type Annotations ------------------------
        // Before (.NET 8 and earlier): The serializer ignored nullable reference annotations.
        // We had to explicitly configure it to write null values.

        var user = new User { Name = null };

        var jsonOld = JsonSerializer.Serialize(user, new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull // Needed to handle null values
        });
        Console.WriteLine(jsonOld); // Output: {}

        // Now (.NET 9): Nullability is respected, no need for extra configuration.
        var jsonNew = JsonSerializer.Serialize(user);
        Console.WriteLine(jsonNew); // Output: {"Name":null}

        // ------------------------ 2. JSON Schema Export ------------------------
        // Before: Needed third-party libraries like NJsonSchema.

        var schemaOld = JsonSchema.FromType<User>();
        Console.WriteLine(schemaOld.ToJson()); // Requires an external package

        // Now: Built-in support with SerializeToSchema.

        //var schemaNew = JsonSerializer.SerializeToSchema<User>();
        //Console.WriteLine(schemaNew); // JSON Schema generated directly from the type

        // Note: SerializeToSchema is not a built-in method in .NET 9. You might need to use a different approach or library for schema generation.

        // ------------------------ 3. Indented JSON Customization ------------------------
        // Before: Limited to WriteIndented = true.
        var jsonIndentedOld = JsonSerializer.Serialize(user, new JsonSerializerOptions { WriteIndented = true });
        Console.WriteLine(jsonIndentedOld);

        // Now: More control over indentation using JsonIndentedOptions.
        // Note: JsonSerializerOptions does not support custom indentation options directly.
        var options = new JsonSerializerOptions
        {
            //Indented = new JsonIndentedOptions
            //{
            //    IndentCharacter = '\t', // Using a tab instead of spaces
            //    IndentSize = 4           // Number of spaces per indentation (if using spaces)
            //}
        };

        var jsonIndentedNew = JsonSerializer.Serialize(user, options);
        Console.WriteLine(jsonIndentedNew);

        // ------------------------ 4. Multiple Root-Level JSON Values ------------------------
        // Before: Required manual parsing of multiple JSON objects from a stream.
        string jsonMultiOld = "{\"Name\":\"Alice\"} {\"Name\":\"Bob\"}";
        using var streamOld = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(jsonMultiOld));
        using var reader = new StreamReader(streamOld);
        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine();
            var userOld = JsonSerializer.Deserialize<User>(line);
            Console.WriteLine(userOld?.Name);
        }

        // Now: Supports reading multiple root-level JSON values directly.
        string jsonMultiNew = "{\"Name\":\"Alice\"} {\"Name\":\"Bob\"}";
        using var streamNew = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(jsonMultiNew));
        await foreach (var userNew in JsonSerializer.DeserializeAsyncEnumerable<User>(streamNew))
        {
            Console.WriteLine(userNew?.Name);
        }
    }
}

public class User
{
    public string? Name { get; set; } // Nullability is now automatically respected
}



//------------------------More notanle Enhancements in .NET 9 ------------------------//

/*
 1.Garbage Collection(GC) Improvements

In.NET 9, the Garbage Collector has been enhanced to dynamically adapt to the application's size, 
replacing the previous Server GC mode. This means the GC now adjusts its behavior based on the application's memory usage patterns, 
leading to optimized performance and reduced latency.


2. Removal of Built-in Swagger Support

Starting with .NET 9, the default Web API templates no longer include built-in support for Swagger --> If you want to use it, ypu have to explicitly add it

In return we got a way more strip down version that only gives you the document itself the open API document 
 https://localhost:7111/openapi/v1.json - now I get the description of my endpoints and description of my data


3. New GUID version --> Earlier its 4, now its 7
*/