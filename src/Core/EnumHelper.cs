#region Imports

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

#endregion Imports

namespace Core
{
    /// <summary>
    /// defines the Enum helper class.
    /// </summary>
    /// <remarks>
    /// see http://blog.waynehartman.com/articles/84.aspx
    /// </remarks>
    public sealed class EnumHelper
    {
        /// <summary>
        /// Strings the value of.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// returns the specific string
        /// </returns>
        public static string StringValueOf(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            if (fi != null)
            {
                DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes.Length > 0)
                {
                    return attributes[0].Description;
                }
                else
                {
                    return value.ToString();
                }
            }

            return string.Format("Organization Property : {0} ", value);
        }

        /// <summary>
        /// Determines whether [is dropdown visibility] [the specified value].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <c>true</c> if [is dropdown visibility] [the specified value]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsDropdownVisibility(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            if (fi != null)
            {
                DropdownEnableAttribute[] attributes = (DropdownEnableAttribute[])fi.GetCustomAttributes(typeof(DropdownEnableAttribute), false);

                if (attributes.Length > 0)
                {
                    return attributes[0].IsVisibleInDropDown;
                }
                else
                {
                    return true;
                }
            }

            return true;
        }


        /// <summary>
        /// Enums the value of.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="enumType">Type of the enum.</param>
        /// <returns>
        /// returns the specific object.
        /// </returns>
        /// <exception cref="ArgumentException">The string is not a   or value of the specified enum.</exception>
        public static object EnumValueOf(string value, Type enumType)
        {
            string[] names = Enum.GetNames(enumType);
            foreach (string name in names)
            {
                if (StringValueOf((Enum)Enum.Parse(enumType, name)).Equals(value))
                {
                    return Enum.Parse(enumType, name);
                }
            }

            throw new ArgumentException("The string is not a   or value of the specified enum.");
        }

        /// <summary>
        /// Gets the enum name list.
        /// </summary>
        /// <param name="enumType">Type of the enum.</param>
        /// <returns>
        /// Attributes name list.
        /// </returns>
        public static string[] GetEnumNameList(Type enumType)
        {
            Array values = Enum.GetValues(enumType);

            string[] atrtributeNameList = new string[values.Length];

            for (int i = 0; i < values.Length; i++)
            {
                Enum item = (Enum)values.GetValue(i);

                atrtributeNameList[i] = StringValueOf(item);
            }

            return atrtributeNameList;
        }

        /// <summary>
        /// Converts the enum to string array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string[] ConvertEnumToStringArray<T>()
        {
            List<string> list = Enum.GetNames(typeof(T)).ToList();

            return list.ToArray();
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <returns>
        /// list with bind to drop down list.
        /// </returns>
        /// <exception cref="ArgumentException">Enumeration type is expected.</exception>
        public static IDictionary<int, string> GetAll<TEnum>() where TEnum : struct
        {
            var enumerationType = typeof(TEnum);

            if (!enumerationType.IsEnum)
            {
                throw new ArgumentException("Enumeration type is expected.");
            }

            var dictionary = new Dictionary<int, string>();

            foreach (int value in Enum.GetValues(enumerationType))
            {
                var name = Enum.GetName(enumerationType, value);
                dictionary.Add(value, name);
            }

            return dictionary;
        }
        
        /// <summary>
        /// Gets the display name.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string GetDisplayName<TEnum>(TEnum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DisplayAttribute[] attributes = (DisplayAttribute[])fi.
                GetCustomAttributes(typeof(DisplayAttribute), false);

            if ((attributes != null) && (attributes.Length > 0))
                return attributes[0].GetName();
            else
                return value.ToString();
        }
    }


}