﻿using Avalon.Base.Extension.Data;
using Microsoft.Data.SqlClient;

namespace Avalon.Base.Extension.UT.Data;

/// <summary>
/// String tests.
/// </summary>
[TestClass]
public class ParameterExtensionsTest
{
    /// <summary>
    /// Test the ToSafeSQL extension.
    /// </summary>
    [TestMethod]
    public void TestToSafeSQL()
    {
        List<SqlParameter> parameters = new List<SqlParameter>();
        parameters.Add(new SqlParameter("@Paramater1", -1));
        parameters.Add(new SqlParameter("@Paramater2", "Car"));
        parameters.ToSafeSQL();
        Assert.IsTrue(parameters.Count == 2);
    }

    /// <summary>
    /// Test the ToDBNullIfEmpty extension.
    /// </summary>
    [TestMethod]
    public void TestToDBNullIfEmpty()
    {
        string testValidString = "valid string";
        string invalidString = null;
        Assert.IsTrue(testValidString.ToDBNullIfEmpty().Equals(testValidString));
        Assert.IsTrue(invalidString.ToDBNullIfEmpty().Equals(DBNull.Value));
    }
}
