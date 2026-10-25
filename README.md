# Avalon Extensions

Avalon Extensions is a collection of C# extension methods for common .NET development tasks. It brings together reusable helpers developed for personal projects, with contributions from the community welcome.

[Wiki](https://github.com/Jorge-GitHub/Avalon-Extensions/wiki) | [Report an issue](https://github.com/Jorge-GitHub/Avalon-Extensions/issues)

## What's included

| Area | Helpers |
| --- | --- |
| Strings | Formatting, email format checks, cleanup, and type conversion. |
| Dates and times | Calendar checks, month boundaries, and DateTime and DateTimeOffset conversions. |
| Numbers and booleans | Conversion, currency formatting, and readability helpers. |
| Collections | Helpers for arrays, lists, dictionaries, and enumerable sequences. |
| Objects and JSON | Serialization, deserialization, copying, mapping, and JSON property access. |
| Data | Helpers for DataSet, DataTable, DataRow, and SQL parameters. |
| Reflection | Helpers for inspecting types and properties. |
| Files and text | File, directory, and StringBuilder utilities. |
| HTTP | Request content, response reading, and download helpers. |

## Getting started

The current source targets **.NET 10**. Install the .NET 10 SDK to build the library or run its tests.

Clone this repository, then run the following command from your application's project directory, replacing `<path-to-Avalon-Extensions>` with the checkout location:

```shell
dotnet add reference "<path-to-Avalon-Extensions>/src/Avalon.Base.Extension/Avalon.Base.Extension/Avalon.Base.Extension.csproj"
```

Your application must target a framework compatible with the library's `net10.0` target. The package ID configured in the project is `net.AvalonTeam.Base.Extensions`.

Import the namespaces containing the extensions you need. The examples below include their required imports.

## Usage examples

### Serialize an object to JSON

`ToJson` uses System.Text.Json and omits null properties by default. Pass `ignoreNull: false` to include them.

```csharp
using Avalon.Base.Extension.Types;

var item = new
{
    Name = "Test",
    Description = "Simple Test"
};

string json = item.ToJson();
// {"Name":"Test","Description":"Simple Test"}
```

### Format and convert strings

```csharp
using Avalon.Base.Extension.Types;
using Avalon.Base.Extension.Types.StringExtensions;

string padded = "42".AppendFront("0", 5); // "00042"
int count = "not a number".ToInteger(0);  // 0

bool valid = "name@example.com".IsAValidEmail(); // true
bool invalid = "not-an-email".IsNotAValidEmail(); // true
```

The email helpers check address format; they do not verify that a mailbox exists.

### Work with dates

```csharp
using System;
using Avalon.Base.Extension.Types.DateTimeExtensions;

DateTime date = new DateTime(2026, 5, 17);

DateTime firstDay = date.FirstDayOfMonth(); // 2026-05-01
DateTime lastDay = date.LastDayOfMonth();   // 2026-05-31
bool leapYear = date.IsLeapYear();          // false
bool isToday = DateTime.Today.IsToday();    // true
```

### Search a StringBuilder

```csharp
using System.Text;
using Avalon.Base.Extension.System.Text;

var text = new StringBuilder("Hello World!");

int found = text.LastIndexOf("World"); // 6
int missing = text.LastIndexOf("Bye"); // -1
```

## Documentation

Visit the [project wiki](https://github.com/Jorge-GitHub/Avalon-Extensions/wiki) for documentation as it is added. For the current API and more usage examples, explore:

- [Library source](https://github.com/Jorge-GitHub/Avalon-Extensions/tree/HEAD/src/Avalon.Base.Extension/Avalon.Base.Extension)
- [Unit tests](https://github.com/Jorge-GitHub/Avalon-Extensions/tree/HEAD/src/Avalon.Base.Extension/Avalon.Base.Extension.UT)

## Building and testing

The solution is located at `src/Avalon.Base.Extension/Avalon.Base.Extension.sln`.

The library project currently includes Windows post-build copy commands targeting `C:\Repos\Libraries`. Before building on another machine, review and adapt those commands in `Avalon.Base.Extension.csproj` for your environment.

From the repository root:

```shell
dotnet build src/Avalon.Base.Extension/Avalon.Base.Extension.sln
dotnet test src/Avalon.Base.Extension/Avalon.Base.Extension.sln
```

## SQL parameter helpers

`ToSafeSQL()` currently leaves parameter values unchanged. It does not sanitize input or provide SQL injection protection. Use parameterized commands with bound values rather than constructing SQL from user input.

## Contributing

Have a useful extension, an improvement, or a documentation correction? Contributions are welcome.

- [Open an issue](https://github.com/Jorge-GitHub/Avalon-Extensions/issues) to report a bug or suggest an addition.
- Submit a pull request describing the change and its intended behavior.
- Include relevant unit tests when adding or changing extension behavior.
