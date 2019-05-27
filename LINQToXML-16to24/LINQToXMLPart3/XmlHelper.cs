using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LINQToXMLPart3
{
    public static class XmlHelper
    {
        public static T GetValueAs<T>(this XElement xElement, T defaultValue = default)
        {
            T returnValue = defaultValue;

            if (xElement != null && !string.IsNullOrWhiteSpace(xElement.Value))
            {
                // Cast to Return Data Type
                // NOTE: ChangeType cannnot cast to a Nullable type
                returnValue = (T)Convert.ChangeType(xElement.Value, typeof(T));
            }

            return returnValue;
        }

        public static T GetValueAs<T>(this XAttribute xAttribute, T defaultValue = default)
        {
            T returnValue = defaultValue;

            if (xAttribute != null && !string.IsNullOrWhiteSpace(xAttribute.Value))
            {
                // Cast to Return Data Type
                // NOTE: ChangeType cannnot cast to a Nullable type
                returnValue = (T)Convert.ChangeType(xAttribute.Value, typeof(T));
            }

            return returnValue;
        }

        public static T GetValueAs<T>(this string text, T defaultValue = default)
        {
            T returnValue = defaultValue;

            if (!string.IsNullOrWhiteSpace(text))
            {
                try
                {
                    switch (typeof(T).Name)
                    {
                        case "Boolean":
                            switch (text.ToUpper())
                            {
                                case "1":
                                case "T":
                                case ".T.":
                                case "TRUE":
                                case "Y":
                                case "YES":
                                    text = "true";
                                    break;
                                case "0":
                                case "F":
                                case ".F.":
                                case "FALSE":
                                case "N":
                                case "NO":
                                    text = "false";
                                    break;
                                default:
                                    throw new Exception($"{text.ToUpper()} is not recognized as a Boolean value.");
                            }
                            break;
                        case "Byte":
                        case "Int16":
                        case "Int32":
                        case "Int64":
                            text = new string(text.TakeWhile(c => char.IsDigit(c)).ToArray());
                            break;
                        //default:
                        //    throw new Exception($"Type {typeof(T).Name} is not implemented yet.");
                    }

                    if (text != null && !string.IsNullOrWhiteSpace(text))
                    {
                        returnValue = (T)Convert.ChangeType(text, typeof(T));
                    }
                }
                catch (FormatException)
                {
                    returnValue = (T)Convert.ChangeType("0", typeof(T));
                }
            }

            return returnValue;
        }

        public static T? GetNullableValueAs<T>(this string text, T? defaultValue = default) where T : struct
        {
            T? returnValue = defaultValue;

            if (!string.IsNullOrWhiteSpace(text))
            {
                try
                {
                    switch (typeof(T).Name)
                    {
                        case "Boolean":
                            switch (text.ToUpper())
                            {
                                case "1":
                                case "T":
                                case ".T.":
                                case "TRUE":
                                case "Y":
                                case "YES":
                                    text = "true";
                                    break;
                                case "0":
                                case "F":
                                case ".F.":
                                case "FALSE":
                                case "N":
                                case "NO":
                                    text = "false";
                                    break;
                                default:
                                    throw new Exception($"{text.ToUpper()} is not recognized as a Boolean value.");
                            }
                            break;
                        case "Byte":
                        case "Int16":
                        case "Int32":
                        case "Int64":
                            text = new string(text.TakeWhile(c => char.IsDigit(c)).ToArray());
                            break;
                        //default:
                        //    throw new Exception($"Type {typeof(T).Name} is not implemented yet.");
                    }

                    if (text != null && !string.IsNullOrWhiteSpace(text))
                    {
                        returnValue = (T)Convert.ChangeType(text, typeof(T));
                    }
                }
                catch (FormatException)
                {
                    // returnValue = (T)Convert.ChangeType("0", typeof(T));
                    returnValue = null;
                }
            }

            return returnValue;
        }

        //public static T? GetValue<T>(object value) where T : struct
        //{
        //    if (value == null || value is DBNull)
        //    {
        //        return null;
        //    }

        //    if (value is T)
        //    {
        //        return (T)value;
        //    }

        //    return (T)Convert.ChangeType(value, typeof(T));
        //}

        public static int StringAsInt(object stringValue, int defaultValue)
        {

            return StringAsInt((stringValue != null ? stringValue.ToString() : string.Empty), defaultValue);
        }

        public static int StringAsInt(string stringValue)
        {
            return StringAsInt(stringValue, 0);
        }

        public static int StringAsInt(string stringValue, int defaultValue)
        {
            int output = defaultValue;

            if (stringValue != null)
            {
                int.TryParse(stringValue.ToString(), out output);
            }
            return output;
        }
    }
}
