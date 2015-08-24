#region "USING STATEMENT"
using System.Data;
#endregion "USING STATEMENT"

//  <copyright file="DataSetExtensions.cs" company="Avalon">
//  Copyright (c) 2015 Avalon All Rights Reserved.
//  Licensed under the MIT license. See LICENSE file in the project root for full license information.
//  http://opensource.org/licenses/MIT
//  </copyright>
//  <author>
//  Jorge Gonzalez
//  </author>
namespace Avalon.Extension.Data
{
    /// <summary>
    /// DataSet extensions.
    /// </summary>
    public static class DataSetExtensions
    {
        #region "METHODS"
        #region "STATIC METHODS"
        /// <summary>
        /// Determinates whether the DataSet contains data.
        /// </summary>
        /// <param name="data">
        /// DataSet to test.
        /// </param>
        /// <returns>
        /// Flag that determinates whether the DataSet contains data.
        /// </returns>
        public static bool HasData(this DataSet data)
        {
            if (data != null && data.Tables != null && data.Tables.Count > 0)
            {
                foreach (DataTable table in data.Tables)
                {
                    if (table.HasData())
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion "STATIC METHODS"
        #endregion "METHODS"
    }
}