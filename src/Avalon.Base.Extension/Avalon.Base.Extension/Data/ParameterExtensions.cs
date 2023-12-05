using Avalon.Base.Extension.Collections;
using Avalon.Base.Extension.Types;
using Microsoft.Data.SqlClient;

namespace Avalon.Base.Extension.Data;

/// <summary>
/// Parameters extensions.
/// </summary>
public static class ParameterExtensions
{
    /// <summary>
    /// Cleans the  parameters for especial 
    /// characters, to avoid SQL injection.
    /// </summary>
    /// <param name="parameters">
    /// Store procedure parameters.
    /// </param>
    public static void ToSafeSQL(this List<SqlParameter> parameters)
    {
        if (parameters.HasElements())
        {
            foreach (SqlParameter parameter in parameters)
            {
                parameter.ToSafeSQL();
            }
        }
    }

    /// <summary>
    /// Cleans the  parameters for especial 
    /// characters, to avoid SQL injection.
    /// </summary>
    /// <param name="parameter">
    /// Store procedure parameter.
    /// </param>
    public static void ToSafeSQL(this SqlParameter parameter)
    {
        parameter.Value = @parameter.Value;
    }
}