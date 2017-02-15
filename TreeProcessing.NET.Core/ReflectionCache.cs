using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TreeProcessing.NET
{
    public static class ReflectionCache
    {
        private static Dictionary<Type, PropertyInfo[]> nodeProperties = new Dictionary<Type, PropertyInfo[]>();

        public static PropertyInfo[] GetClassProperties(Type objectType)
        {
            PropertyInfo[] result = null;
            if (!nodeProperties.TryGetValue(objectType, out result))
            {
                result = objectType.GetRuntimeProperties().Where(prop => prop.CanWrite && prop.CanRead && !prop.Name.EndsWith("Serializable")).ToArray();
                nodeProperties[objectType] = result;
            }
            return result;
        }
    }
}
