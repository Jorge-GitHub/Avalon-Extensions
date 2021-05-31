#region "USING STATEMENTS"
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
#endregion "USING STATEMENTS"

//  <copyright file="ObjectBasicExtensions.cs" company="Avalon">
//  Copyright (c) 2015 Avalon All Rights Reserved.
//  Licensed under the MIT license. See LICENSE file in the project root for full license information.
//  http://opensource.org/licenses/MIT
//  </copyright>
//  <author>
//  Jorge Gonzalez
//  </author>
//  <date>
//  11/29/2013 10:00:00 PM
//  </date>
namespace Avalon.Extension.Types
{
    /// <summary>
    /// Basic object extensions.
    /// </summary>
    public static class ObjectBasicExtensions
    {
        #region "METHODS"
        #region "STATIC METHODS"
        /// <summary>
        /// Indicates whether the specified object is null.
        /// </summary>
        /// <param name="value">
        /// Object to test.
        /// </param>
        /// <returns>
        /// True if the object is null.
        /// </returns>
        public static bool IsNull(this object value)
        {
            if (value == null || DBNull.Value.Equals(value) || Convert.IsDBNull(value))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Indicates whether the specified object is null.
        /// </summary>
        /// <param name="value">
        /// Object to test.
        /// </param>
        /// <returns>
        /// True if the object is null.
        /// </returns>
        public static bool IsNotNull(this object value)
        {
            return !value.IsNull();
        }
        /// <summary>
        /// Copy the object.
        /// </summary>
        /// <typeparam name="T">
        /// Type of the object to return.
        /// </typeparam>
        /// <param name="objectToCopy">
        /// Object to be copied.
        /// </param>
        /// <returns>
        /// Copy of the object.
        /// </returns>
        public static T Copy<T>(this object objectToCopy)
        {
            if (objectToCopy.IsNotNull())
            {
                BinaryFormatter data = new BinaryFormatter();
                MemoryStream stream = new MemoryStream();
                data.Serialize(stream, objectToCopy);
                stream.Seek(0, SeekOrigin.Begin);
                T copy = (T)data.Deserialize(stream);
                stream.Close();
                return copy;
            }
            return default(T);
        }
        #endregion "STATIC METHODS"
        #endregion "METHODS"
    }
}