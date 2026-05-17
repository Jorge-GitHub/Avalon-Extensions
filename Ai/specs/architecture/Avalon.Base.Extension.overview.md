# Avalon.Base.Extension Overview

Avalon.Base.Extension is a small .NET extension-method library for common string, object, collection, data, date/time, numeric, reflection, IO, and `StringBuilder` tasks. The production project is `src/Avalon.Base.Extension/Avalon.Base.Extension`; ignore the `.UT` project when learning the library surface.

The package targets `net10.0`, uses `System.Text.Json` for object JSON work, and references `Microsoft.Data.SqlClient` for SQL parameter helpers.

## Namespace Map

Import only the namespaces needed by the type you are extending:

```csharp
using Avalon.Base.Extension.Types;
using Avalon.Base.Extension.Types.StringExtensions;
using Avalon.Base.Extension.Types.DateTimeExtensions;
using Avalon.Base.Extension.Types.DateTimeOffsetExtensions;
using Avalon.Base.Extension.Types.BooleanExtensions;
using Avalon.Base.Extension.Types.IntegerExtensions;
using Avalon.Base.Extension.Types.DecimalExtensions;
using Avalon.Base.Extension.Types.DoubleExtensions;
using Avalon.Base.Extension.Types.EnumExtensions;
using Avalon.Base.Extension.Types.TypeExtensions;
using Avalon.Base.Extension.Collections;
using Avalon.Base.Extension.Collections.ArrayExtensions;
using Avalon.Base.Extension.Collections.ArrayListExtensions;
using Avalon.Base.Extension.Data;
using Avalon.Base.Extension.Data.Conversion;
using Avalon.Base.Extension.System.Text;
using Avalon.Base.Extension.System.IO;
```

## Common Usage

```csharp
using Avalon.Base.Extension.Types;
using Avalon.Base.Extension.Types.StringExtensions;
using Avalon.Base.Extension.Types.DateTimeExtensions;
using Avalon.Base.Extension.Collections;
using Avalon.Base.Extension.Data;

string padded = "42".AppendFront("0", 5);          // "00042"
string title = "hello world".ToTitleCase();        // "Hello World"
int count = "not a number".ToInteger(0);           // 0
bool valid = "name@example.com".IsAValidEmail();   // true

DateTime date = new DateTime(2026, 5, 17);
bool weekend = date.IsWeekend();                   // true
DateTime monthEnd = date.LastDayOfMonth();         // 2026-05-31

var values = new[] { "A", "B", "C" };
bool hasB = values.Contains("b", ignoreCase: true);
string joined = values.Join("|");                  // "A|B|C"

List<User> users = table.ToObjects<User>();        // DataTable rows to objects
string json = users.ToJSON();                      // System.Text.Json serialization
```

## Strings

`Avalon.Base.Extension.Types` contains general string helpers:

- Checks: `IsNullOrEmpty`, `IsNotNullOrEmpty`, `NotEquals`, `IsAnInteger`, `IsADecimal`, `IsNumeric`, `ISABoolean`, `IsAllUpperCase`, `IsAllLowerCase`, `EqualsIfNotNull`.
- Cleanup and shaping: `ToEmptyStringIfNull`, `ToSafeString`, `ToSafeTrim`, `SafeTrim`, `LimitLength`, `Reverse`, `ToTitleCase`, `WordCount`, `Remove`, `Replace`, `RemoveNonASCIICharacters`, `RemoveLastIndexOf`, `GetFirst`, `GetLast`, `Format`.
- Conversions: `ToBoolean`, `ToInteger`, `ToDecimal`, `ToDecimalFromCurrency`, `ToLong`, `FormatToCurrency`, `RemoveNoneNumericValues`, `ToDateTime`, `ToObject<T>`, `ToObjectSafe<T>`, `ToBytes`.
- Other: `ToSHA1`, `Evaluate`.

```csharp
using Avalon.Base.Extension.Types;

string clean = "  Line\r\n".ToSafeTrim(removeBreaks: true);
bool numeric = "123".IsNumeric();
decimal amount = "$12.30".ToDecimalFromCurrency(0m);
DateTime fallback = "bad-date".ToDateTime(DateTime.MinValue);
Order? order = json.ToObjectSafe<Order>();
string sha1 = "abc".ToSHA1();
decimal math = "10 + 5 * 2".Evaluate();
```

`Avalon.Base.Extension.Types.StringExtensions` adds formatting, email, sanitizing, and encryption helpers:

- Formatting: `AppendFront`, `Truncate`, `ToDefaultValueIfEmpty`, `SplitByBlocks`, `ToDigits`, `ToPhoneNumber`, `Sanitize`.
- Email: `IsAValidEmail`, `IsNotAValidEmail`.
- Tag removal: `RemoveBetweenTags`.
- Encoding/encryption: `Encrypt()`, `Decrypt()`, `Encrypt(key)`, `Decrypt(key)`.

```csharp
using Avalon.Base.Extension.Types.StringExtensions;

string phone = "1 (404) 555-0100".ToPhoneNumber(); // "+1 (404) 555-0100"
string slug = "Invoice #123!".Sanitize("-");       // "Invoice--123-"
List<string> chunks = "ABCDEFGHIJ".SplitByBlocks(3);
string encoded = "hello".Encrypt();                // base64, not secure encryption
string restored = encoded.Decrypt();
```

## Objects, JSON, and Enums

`Avalon.Base.Extension.Types` contains object, JSON, and enum helpers:

- Object checks and strings: `IsNull`, `IsNotNull`, `ToString(defaultValue)`, `ToSafeString`, `ToLowerString`, `ToUpperString`.
- JSON copy/map: `ToJSON`, `Copy<T>`, `Map<T>`, `ToDTO<T>`.
- Type checks: `IsDictionary`, `IsList`, `IsGenericList`.
- Enums: `GetDescription`, `ToEnum<T>`, `OrDefaultIfUndefined`.

```csharp
using Avalon.Base.Extension.Types;

object value = DBNull.Value;
bool empty = value.IsNull();                        // true

string json = user.ToJSON(ignoreNull: true);
UserDto dto = user.ToDTO<UserDto>();                // serialize and deserialize
User clone = user.Copy<User>();

Status status = "Active".ToEnum<Status>(Status.Unknown);
status = status.OrDefaultIfUndefined(Status.Unknown);
```

`Avalon.Base.Extension.Types.EnumExtensions` has `ToListString<T>()` for enum lists.

```csharp
using Avalon.Base.Extension.Types.EnumExtensions;

List<string> names = statuses.ToListString();
```

## Collections

`Avalon.Base.Extension.Collections` covers generic arrays, `IEnumerable<T>`, `List<T>`, and dictionaries:

- Arrays: `Contains`, `Join`, `HasElements`, `IsNullOrEmpty`, `Slice`, `Randomize`.
- Enumerables: `HasElements`, `IsNullOrEmpty`, `Each`, `Wave`, `Span`, `ToStringStringBuilder`.
- Lists: `Randomize`, `Span`.
- Dictionaries of arrays: `AddRange`.

```csharp
using Avalon.Base.Extension.Collections;

if (items.HasElements())
{
    items.Each(item => Console.WriteLine(item));
}

StringBuilder lines = items.ToStringStringBuilder();
Dictionary<string, int[]> target = new();
target.AddRange(source, updateValue: false);

List<int> shuffled = numbers.ToList().Randomize(); // consumes the list it is called on
```

For non-generic collection types:

```csharp
using Avalon.Base.Extension.Collections.ArrayExtensions;
using Avalon.Base.Extension.Collections.ArrayListExtensions;

Array names = new[] { "Ada", "Grace" };
bool hasAda = names.Contains("ada", ignoreCase: true);

ArrayList legacy = new() { "A", "B" };
string csv = legacy.Join();
```

## DataSet, DataTable, DataRow, and SQL Parameters

`Avalon.Base.Extension.Data` maps ADO.NET data containers into objects and reads common values:

- `DataSet`: `HasData`, `GetDataTableByNameSafe`, `GetNumberOfRowsOnTable`, `GetFirstValueOnTable`, `GetFirstValueOnTheFirstTable`, `GetFirstValueOnTheFirstTableAsBoolean`, `GetFirstValueOnTheFirstTableAsLong`, `GetFirstValueOnDataSet`, `GetFirstValueOnTableAsInteger`, `ToObjects<T>`, `ToObjectFromFirstUserFromDataSet<T>`.
- `DataTable`: `HasData`, `ToObjects<T>`, `ToObjectFromFirstUserFromDataTable<T>`.
- `DataRowCollection` and rows: `HasRecords`, `ToRows`, `ToObjects<T>`, `ToObject<T>`, `ImportData`.
- Safe columns: `DataColumnToSafeString`, `DataColumnToSafeInteger`.
- SQL parameters: `ToSafeSQL`, `ToDBNullIfEmpty`.

```csharp
using Avalon.Base.Extension.Data;

if (dataSet.HasData())
{
    long id = dataSet.GetFirstValueOnTheFirstTableAsLong();
    List<User> users = dataSet.ToObjects<User>();
}

User user = table.Rows[0].ToObject<User>();
row.ImportData(user);                               // object properties into matching columns
string name = row.DataColumnToSafeString("Name");
object dbName = name.ToDBNullIfEmpty();
```

Data-row object mapping expects a parameterless target type with public writable properties. Column names match property names case-insensitively, and the internal mapper also ignores `_`, `-`, and spaces when matching columns to properties.

`Avalon.Base.Extension.Data.Conversion` has direct row conversion helpers:

```csharp
using Avalon.Base.Extension.Data.Conversion;

string name = row.ToString("Name", "");
int id = row.ToInteger32("Id", -1);
bool enabled = row.ToBoolean("Enabled", false);
bool hasAny = row.Contains(new[] { "Id", "Name" }); // true when any listed column exists
```

## Dates, Numbers, Booleans, and Reflection

Date helpers:

```csharp
using Avalon.Base.Extension.Types.DateTimeExtensions;
using Avalon.Base.Extension.Types.DateTimeOffsetExtensions;

DateTime today = DateTime.Today;
bool isToday = today.IsToday();
bool isWeekday = today.IsWeekday();
DateTime first = today.FirstDayOfMonth();
DateTime last = today.LastDayOfMonth();
string iso = DateTime.Now.ToISOString(DateTimeKind.Local);

DateTimeOffset offsetFirst = DateTimeOffset.Now.FirstDayOfMonth();
```

Numeric and boolean helpers:

```csharp
using Avalon.Base.Extension.Types;
using Avalon.Base.Extension.Types.IntegerExtensions;
using Avalon.Base.Extension.Types.DecimalExtensions;
using Avalon.Base.Extension.Types.DoubleExtensions;
using Avalon.Base.Extension.Types.BooleanExtensions;

string price = 12345.ToCurrency("en-US");           // "$123.45" because int cents are divided by 100
string amount = 123.45m.ToCurrency("en-US");
int negative = 5.Negate();
bool readable = flag.IsTrue();
```

Reflection helpers:

```csharp
using Avalon.Base.Extension.Types.TypeExtensions;

Type itemType = typeof(List<User>).GetElementTypeFromCollection();
bool collectionProperty = propertyInfo.IsACollection();
Type propertyItemType = propertyInfo.GetElementTypeFromCollection();
```

## StringBuilder and IO

```csharp
using Avalon.Base.Extension.System.Text;
using Avalon.Base.Extension.System.IO;

var builder = new StringBuilder("a[start]remove[end]z");
builder.RemoveBetweenTags("[start]", "[end]");
int last = builder.LastIndexOf("z");

string path = "logs".ResolveFolderPath();           // relative to AppContext.BaseDirectory
path.EmptyFolder();                                 // deletes all files and subdirectories inside path
new DirectoryInfo(path).Empty();
```

## Agent Watch Outs

- `StringEncryptionExtensions.Encrypt()` without a key is base64 encoding, not secure encryption. The AES overload requires a key size accepted by `Aes`, normally 16, 24, or 32 bytes.
- `ParameterExtensions.ToSafeSQL()` does not actually sanitize; it assigns `parameter.Value` back to itself. Always use real parameterized SQL and validation.
- `List<T>.Randomize()` removes every item from the source list while building the returned shuffled list.
- `DirectoryInfo.Empty()` and `string.EmptyFolder()` permanently delete folder contents.
- `DataRowConversionExtensions.Contains(row, columnNames)` returns true if any supplied column exists, not all.
- Current implementations of `ToBytes`, `StringBasicExtensions.Format`, `EqualsIfNotNull`, `RemoveLastIndexOf(char)`, and generic array `Slice` appear inconsistent with their names. Prefer built-in .NET alternatives or inspect/fix before relying on them.
