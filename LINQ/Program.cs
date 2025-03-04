//// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System;

var totalWatchTime = new List<(long CourseId, long LessonDurationInSeconds)>();

var completedLessons = new List<CompletedLesson>
{
    new(1,1,463),
    new(1,2,369),
    new(2,1,765),
    new(2,3,424),
    new(3,1,86)
};

totalWatchTime = completedLessons
    .Select(cl => (cl.CourseId, cl.LessonDurationInSeconds))
    .ToList();

//Before .NET 9, we had to use GroupBy() and Select() to manually group the data and aggregate it

var courseWatchTimeBefore = totalWatchTime
    .GroupBy(x => x.CourseId) // Group by CourseId
    .Select(group => new
    {
        CourseId = group.Key,
        TotalWatchTime = group.Sum(x => x.LessonDurationInSeconds) // Summing durations
    });

// Using AggregateBy in .NET 9 for efficient aggregation
var courseWatchTime = totalWatchTime.AggregateBy(
    x => x.CourseId,   // Key selector (group by CourseId)
    _ => 0m,          // Initial value for aggregation
    (seconds, item) => seconds + item.LessonDurationInSeconds // Aggregation logic
);

foreach (var item in courseWatchTime)
{
    Console.WriteLine($"Course ID: {item.Key} - Total Watch Time: {item.Value} seconds");
}

//New method CountBy

//foreach (var item in CompletedLesson.CountBy(x=>x.LessonId))
//{
//    Console.WriteLine($"Lession ID: {item.Key} was watched - {item.Value} times");
//}
record CompletedLesson(long CourseId, long LessonId, long LessonDurationInSeconds);


//Why is .AggregateBy() Better ?

//1.More Efficient:
//.AggregateBy() is optimized for performance and avoids creating unnecessary intermediate collections, making it faster and more memory-efficient.
//GroupBy().Select() creates an additional grouping structure that takes extra processing time.

//2. More Readable & Concise:
//.AggregateBy() directly expresses "Group and Sum" logic in a single call.
//No need for .GroupBy() and then a separate.Select(), making it easier to understand.

//3. Thread - Safe & Parallelizable:
//.AggregateBy() works well in multi - threaded or parallel processing scenarios, as it avoids additional collection overhead.

//Less Garbage Collection Pressure:
//.GroupBy() creates additional objects(IEnumerable, Lookup structures) that can increase GC pressure.
//.AggregateBy() directly aggregates into a dictionary - like structure, reducing memory overhead.