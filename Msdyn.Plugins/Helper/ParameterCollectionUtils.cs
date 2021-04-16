
using System;

namespace Microsoft.Xrm.Sdk
{
    /// <summary>
    /// Extention-методы для класса ParameterCollection
    /// </summary>
    public static class ParameterCollectionUtils
    {
        /// <summary>
        /// Аналог GetValue<T> у Entity
        /// </summary>
        public static T GetValue<T>(this ParameterCollection parameters, string name)
        {
            if (parameters.TryGetValue(name, out object objectValue))
            {
                return (T)objectValue;
            }
            else
            {
                return default;
            }
        }

        /// <summary>
        /// Преобразует значения параметра типа OptionSetValue в нужный Nullable Enum
        /// </summary>
        /// <typeparam name="T">Enum-тип к которому нужно привсти OptionSetValue</typeparam>
        /// <param name="name">Имя параметра</param>
        public static Nullable<T> GetOptionSetValue<T>(this ParameterCollection parameters, String name) where T : struct
        {

            if (parameters.TryGetValue(name, out object objectValue))
            {
                if (objectValue is OptionSetValue optionSetValue)
                {
                    /// Случится явная упаковка-распаковка но вариант реализации
                    /// return (T) Enum.Parse(typeof(T), optionSetValue.Value.ToString());
                    /// должен содержать схожий набор преобразований

                    return (T)(object)optionSetValue.Value;
                }
                else
                {
                    throw new ArgumentException(name + " is not OptionSetValue type", "name");
                }
            }

            return null;
        }

        public static T GetValueByDefault<T>(this ParameterCollection parameters
            , string name
            , T defaultValue)
        {
            if (parameters.Keys.Contains(name))
            {
                if (parameters.TryGetValue(name, out object objectValue))
                {
                    return (T)objectValue;
                }
                else
                {
                    return defaultValue;
                }
            }
            return defaultValue;
        }

        public static int GetValueAsInt32(this ParameterCollection parameters
            , string name
            , int defaultValue)
        {
            if (parameters.Keys.Contains(name))
            {
                if (parameters.TryGetValue(name, out object objectValue))
                {
                    return (int)objectValue == 0 ? defaultValue : (int)objectValue;
                }
                else
                {
                    return defaultValue;
                }
            }
            return defaultValue;
        }
    }
}

