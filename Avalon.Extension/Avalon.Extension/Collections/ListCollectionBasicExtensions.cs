#region "USING STATEMENTS"
using System.Collections.Generic;
#endregion "USING STATEMENTS"

//  <copyright file="ListCollectionBasicExtensions.cs" company="Avalon">
//  Copyright (c) 2015 Avalon All Rights Reserved.
//  Licensed under the MIT license. See LICENSE file in the project root for full license information.
//  http://opensource.org/licenses/MIT
//  </copyright>
//  <author>
//  Jorge Gonzalez
//  </author>
namespace Avalon.Extension.Collections
{
    /// <summary>
    /// List collections extension methods.
    /// </summary>
    public static class ListCollectionBasicExtensions
    {
        #region "METHODS"
        #region "STATIC METHODS"
        /// <summary>
        /// Determinates whether the list has elements.
        /// </summary>
        /// <param name="list">
        /// List to check.
        /// </param>
        /// <returns>
        /// Flag that determinates whether the list has elements or not.
        /// </returns>
        public static bool HasElements<T>(this List<T> list)
        {
            return (list != null && list.Count > 0);
        }
        #endregion "STATIC METHODS"
        #endregion "METHODS"
    }
}