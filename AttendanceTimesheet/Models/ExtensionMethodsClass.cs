using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AttendanceTimesheet.Models
{
    public static class ExtensionMethodsClass
    {
        public static T?  GetValueOrNull<T>(this string valueAsString) where T:struct
        {
            if (string.IsNullOrEmpty(valueAsString))
                return null;
            return (T)Convert.ChangeType(valueAsString, typeof(T));
        }
    }
}