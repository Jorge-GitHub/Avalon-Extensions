# Avalon-Extensions

## Extensions for .NET

I love C# [extensions](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/extension-methods). 
This is a library that contains some of the extensions I wrote during my career for my personal projects. 
I hope more people will join and add more extensions that we can share.


### Basic usage

The following code demonstrates basic usage of Avalon-Extensions.

```cs
using System;
using Avalon.Extension.Types;
public void TestAvalonExtensions()
{
    DateTime now = DateTime.Now;
    // Returns a flag that determinates whether the datetime is today or not.
    bool isToday = now.IsToday(); 
    // Returns the last day of the month from a provider date.
    DateTime lastDayOTheMonth = now.LastDayOfMonth(); 
    // Returns a flag that determinates whether the DateTime year is a leap year or not.
    bool isLeapYear = now.IsLeapYear();
}
```
