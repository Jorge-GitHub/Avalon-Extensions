#region "USING STATEMENT"
using System.Data;
#endregion "USING STATEMENT"

//  <copyright file="DataTableExtensions.cs" company="Avalon">
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
    /// DataTable extensions.
    /// </summary>
    public static class DataTableExtensions
    {
        #region "METHODS"
        #region "STATIC METHODS"
        /// <summary>
        /// Determinates whether the DataSet contains data.
        /// </summary>
        /// <param name="table">
        /// DataSet to test.
        /// </param>
        /// <returns>
        /// Flag that determinates whether the DataSet contains data.
        /// </returns>
        public static bool HasData(this DataTable table)
        {
            if (table != null && table.Rows != null && table.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }
        #endregion "STATIC METHODS"
        #endregion "METHODS"
    }
}