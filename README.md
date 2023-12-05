# Avalon-Extensions

## Extensions for .NET

I love C# [extensions](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/extension-methods). 
This is a library that contains some of the extensions I wrote during my career for my personal projects. 
I hope more people will join and add more extensions that we can share.


### Convert an Object into JSON

The below code will convert an object into a JSON format string.

```cs
    public void TestToJSON()
    {
        TestClass test = new TestClass()
        {
            Name = "Test",
            Description = "Simple Test"
        };
        string json = test.ToJSON();
    }

    internal class TestClass
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
```

### Prevent SQL Injection

The below code will remove any SQL injection on a list of parameters.

```cs
    public void TestToSafeSQL()
    {
        List<SqlParameter> parameters = new List<SqlParameter>();
        parameters.Add(new SqlParameter("@Paramater1", -1));
        parameters.Add(new SqlParameter("@Paramater2", "Car"));
        parameters.ToSafeSQL();
        Assert.IsTrue(parameters.Count == 2);
    }
```

### Check if the Email is Valid or Not.

the below code checks if a string is a valid email or not.

```cs
    public void TestIsAValidEmail()
    {
        Assert.IsTrue("jamesbond@gmail.com".IsAValidEmail());
        Assert.IsFalse("jamesbond".IsAValidEmail());

        Assert.IsFalse("jamesbond@gmail.com".IsNotAValidEmail());
        Assert.IsTrue("jamesbond".IsNotAValidEmail());
    }
```

### StringBuilder LastIndexOf

StringBuilder does not have LastIndexOf method, so I add it one.

```cs
    public void TestLastIndexOf()
    {
        StringBuilder testValue = new StringBuilder("Hello World!");
        Assert.IsTrue(testValue.LastIndexOf("Hello") > -1);
        Assert.IsTrue(testValue.LastIndexOf("Bye") == -1);
    }
```

### DateTime Extensions

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

**Note:** I will document all of the extensions on wiki as soon as I have some free time. Check the code for more extensions.
