#region "USING STATEMENTS"
using System;
using System.Text;
#endregion "USING STATEMENTS"

//  <copyright file="CollectionBasicExtensions.cs" company="Avalon">
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
    /// Collections extension methods.
    /// </summary>
    public static class CollectionBasicExtensions
    {
        #region "METHODS"
        #region "STATIC METHODS"
        /// <summary>
        /// Determinates whether the element is in the array.
        /// </summary>
        /// <param name="array">
        /// Array.
        /// </param>
        /// <param name="item">
        /// Item to locate in the array. The value can be null.
        /// </param>
        /// <returns>
        /// Flag that determinates whether the item is in the array.
        /// </returns>
        public static bool Contains<T>(this T[] array, object item)
        {
            if (array.HasElements())
            {
                return (Array.IndexOf(array, item) > -1);
            }
            return false;
        }
        /// <summary>
        /// Determinates whether the item is in the Array.
        /// </summary>
        /// <param name="array">
        /// Array to search.
        /// </param>
        /// <param name="item">
        /// Item to  locate in the Array.
        /// </param>
        /// <param name="ignoreCase">
        /// Flag that determinates whether to ignore case or not.
        /// </param>
        /// <returns>
        /// Flag that determinates whether the item is within the Array.
        /// </returns>
        public static bool Contains<T>(this T[] array, string item, bool ignoreCase)
        {
            if (ignoreCase)
            {
                return array.Contains(item, StringComparison.InvariantCultureIgnoreCase);
            }
            return array.Contains(item, StringComparison.InvariantCulture);
        }
        /// <summary>
        /// Determinates whether the item is in the Array.
        /// </summary>
        /// <param name="array">
        /// Array to search.
        /// </param>
        /// <param name="item">
        /// Item to  locate in the Array.
        /// </param>
        /// <param name="comparisonType">
        /// Flag that determinates whether to ignore case or not.
        /// </param>
        /// <returns>
        /// Flag that determinates whether the item is within the Array.
        /// </returns>
        public static bool Contains<T>(this T[] array, string item, StringComparison comparisonType)
        {
            if (array.HasElements())
            {
                if (!String.IsNullOrEmpty(item))
                {
                    item = item.Trim();
                    foreach (T element in array)
                    {
                        if (element != null && element.GetType().Equals(typeof(String)))
                        {
                            if (element.ToString().Trim().Equals(item, comparisonType))
                            {
                                return true;
                            }
                        }
                    }
                    return false;
                }
            }
            return false;
        }
        /// <summary>
        /// Concatenates the array's elements by using comma as a separator.
        /// </summary>
        /// <param name="array">
        /// Value to concatenate.
        /// </param>
        /// <returns>
        /// The array elements joined by comma.
        /// </returns>
        public static string Join<T>(this T[] array)
        {
            return array.Join(null);
        }
        /// <summary>
        /// Concatenates the array's elements by using the specified separator.
        /// </summary>
        /// <param name="array">
        /// Value to concatenate.
        /// </param>
        /// <param name="separator">
        /// Optional: The string use as a separator. By default is comma.
        /// </param>
        /// <returns>
        /// The array elements joined by the separator.
        /// </returns>
        public static string Join<T>(this T[] array, string separator)
        {
            if (array.HasElements())
            {
                if (String.IsNullOrEmpty(separator))
                {
                    separator = ",";
                }
                StringBuilder result = new StringBuilder();
                int numberOfElements = array.Length;
                object element;
                for (int i = 0; i < numberOfElements; i++)
                {
                    element = array.GetValue(i);
                    if (element != null)
                    {
                        result.Append(element.ToString() + separator);
                    }
                }
                return result.ToString().Remove(result.Length - separator.Length);
            }
            return "";
        }
        /// <summary>
        /// Determinates whether the Array has elements.
        /// </summary>
        /// <param name="array">
        /// Array to check.
        /// </param>
        /// <returns>
        /// Flag that determinates whether the Array has elements or not.
        /// </returns>
        public static bool HasElements<T>(this T[] array)
        {
            return (array != null && array.Length > 0);
        }
        /// <summary>
        /// Determinates whether the Array is null or empty.
        /// </summary>
        /// <param name="array">
        /// Array to check.
        /// </param>
        /// <returns>
        /// Flag that determinates whether the Array is null or empty.
        /// </returns>
        public static bool IsNullOrEmpty<T>(this T[] array)
        {
            return !array.HasElements();
        }
        /// <summary>
        /// Slice the array between the two indexes.
        /// </summary>        
        /// <param name="array">
        /// Array.
        /// </param>
        /// <param name="start">
        /// An integer that specifies where to start the selection 
        /// (The first element has an index of 0).
        /// </param>
        /// <returns>
        /// A new array, containing the selected elements
        /// Note: The original array will not be changed.
        /// </returns>
        public static T[] Slice<T>(this T[] array, int start)
        {
            if(array.HasElements())
            {
                return array.Slice(start, array.Length - start);
            }
            return new T[0];
        }
        /// <summary>
        /// Slice the array between the two indexes.
        /// </summary>        
        /// <param name="array">
        /// Array.
        /// </param>
        /// <param name="start">
        /// An integer that specifies where to start the selection 
        /// (The first element has an index of 0).
        /// </param>
        /// <param name="count">
        /// An integer that specifies how many items to return.
        /// If the number is equal or less than zero, 
        /// an empty array will be returned.
        /// </param>
        /// <returns>
        /// A new array, containing the selected elements
        /// Note: The original array will not be changed.
        /// </returns>
        public static T[] Slice<T>(this T[] array, int start, int count)
        {
            if (array.HasElements())
            {
                if (count > 0)
                {
                    if (start < 0)
                    {
                        start = 0;
                    }
                    if (count > array.Length)
                    {
                        count = array.Length - start;
                    }
                    int numberOfElements = array.Length;
                    T[] result = new T[count];
                    for (int i = 0; i < count; i++)
                    {
                        if ((i + start) > numberOfElements)
                        {
                            result[i] = array[i + start];
                        }
                        else
                        {
                            break;
                        }

                    }
                    return result;
                }
            }
            return new T[0];
        }
        #endregion "STATIC METHODS"
        #endregion "METHODS"
    }
}