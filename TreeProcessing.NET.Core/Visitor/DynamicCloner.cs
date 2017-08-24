using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TreeProcessing.NET
{
    public class DynamicCloner : DynamicVisitor<Node>
    {
        public override Node VisitChildren(Node node)
        {
            if (node == null)
            {
                return null;
            }

            Type type = node.GetType();
            var result = (Node)Activator.CreateInstance(type);
            PropertyInfo[] properties = ReflectionCache.GetClassProperties(type);
            foreach (PropertyInfo prop in properties)
            {
                Type propType = prop.PropertyType;
                TypeInfo typeInfo = propType.GetTypeInfo();
                if (typeInfo.IsValueType)
                {
                    prop.SetValue(result, prop.GetValue(node));
                }
                else if (propType == typeof(string))
                {
                    string stringValue = (string)prop.GetValue(node);
                    prop.SetValue(result, stringValue != null ? (string)prop.GetValue(node) : null);
                }
                else if (typeInfo.IsSubclassOf(typeof(Node)) || propType == typeof(Node))
                {
                    Node getValue = (Node)prop.GetValue(node);
                    Node setValue = getValue != null ? Visit((dynamic)getValue) : null;
                    prop.SetValue(result, setValue);
                }
                else if (typeInfo.ImplementedInterfaces.Contains(typeof(IEnumerable)))
                {
                    Type itemType = typeInfo.GenericTypeArguments[0];
                    var sourceCollection = (IEnumerable<object>)prop.GetValue(node);
                    IList destCollection = null;
                    if (sourceCollection != null)
                    {
                        destCollection = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(itemType));
                        foreach (var item in sourceCollection)
                        {
                            var nodeItem = item as Node;
                            if (nodeItem != null)
                            {
                                destCollection.Add(Visit((dynamic)nodeItem));
                            }
                            else
                            {
                                destCollection.Add(item);
                            }
                        }
                    }
                    prop.SetValue(result, destCollection);
                }
                else
                {
                    throw new NotImplementedException($"Property \"{prop}\" processing is not implemented via reflection");
                }
            }

            return result;
        }
    }
}
