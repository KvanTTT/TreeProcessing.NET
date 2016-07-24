using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TreesProcessing.NET
{
    public static class StaticHelper
    {
        public static int Compare(this Node node1, Node node2)
        {
            if (node1 == null && node2 == null)
            {
                return 0;
            }
            else if (node1 != null && node2 == null)
            {
                return (int)node1.NodeType;
            }
            else if (node1 == null && node2 != null)
            {
                return -(int)node2.NodeType;
            }

            if (node1.NodeType != node2.NodeType)
            {
                return node1.NodeType - node2.NodeType;
            }

            int result;
            Type type = node1.GetType();
            PropertyInfo[] properties = ReflectionCache.GetClassProperties(type);
            foreach (PropertyInfo prop in properties)
            {
                result = Compare(prop.GetValue(node1), prop.GetValue(node2));
                if (result != 0)
                {
                    return result;
                }
            }

            return 0;
        }

        private static int Compare(object obj1, object obj2)
        {
            if (obj1 == null && obj1 == null)
            {
                return 0;
            }
            else if (obj1 != null && obj2 == null)
            {
                return 1;
            }
            else if (obj1 == null && obj2 != null)
            {
                return -1;
            }

            Type obj1Type = obj1.GetType();
            Type obj2Type = obj2.GetType();
            TypeInfo typeInfo1 = obj1Type.GetTypeInfo();
            TypeInfo typeInfo2 = obj2Type.GetTypeInfo();

            if (!(typeInfo1.ImplementedInterfaces.Contains(typeof(IList)) && typeInfo2.ImplementedInterfaces.Contains(typeof(IList))))
            {
                if (obj1Type != obj2Type)
                {
                    return 1;
                }
            }
            
            int result = 0;
            if (typeInfo1.IsValueType || obj1Type == typeof(string))
            {
                var comparable1 = (IComparable)obj1;
                var comparable2 = (IComparable)obj2;
                result = comparable1.CompareTo(comparable2);
                if (result != 0)
                {
                    return result;
                }
            }
            else if (typeInfo1.IsSubclassOf(typeof(Node)) || obj1Type == typeof(Node))
            {
                result = Compare((Node)obj1, (Node)obj2);
                if (result != 0)
                {
                    return result;
                }
            }
            else if (typeInfo1.ImplementedInterfaces.Contains(typeof(IList)))
            {
                var node1List = (IList)obj1;
                var node2List = (IList)obj2;

                if (node1List.Count != node2List.Count)
                {
                    return node1List.Count - node2List.Count;
                }

                for (int i = 0; i < node1List.Count; i++)
                {
                    result = Compare(node1List[i], node2List[i]);
                    if (result != 0)
                    {
                        return result;
                    }
                }
            }
            else
            {
                throw new NotImplementedException($"Type \"{obj1Type}\" comparison is not implemented via reflection");
            }

            return result;
        }

        public static IEnumerable<Node> GetDescendants(this Node node)
        {
            var result = new List<Node>();
            node.GetDescendants(result);
            return result;
        }

        private static void GetDescendants(this Node node, List<Node> descendants)
        {
            PropertyInfo[] properties = ReflectionCache.GetClassProperties(node.GetType());
            foreach (PropertyInfo prop in properties)
            {
                var propValue = prop.GetValue(node);
                if (propValue != null)
                {
                    var propValueType = propValue.GetType();
                    TypeInfo typeInfo = propValueType.GetTypeInfo();
                    if (typeInfo.IsSubclassOf(typeof(Node)) || propValueType == typeof(Node))
                    {
                        var child = (Node)prop.GetValue(node);
                        descendants.Add(child);
                        child.GetDescendants(descendants);
                    }
                    else if (typeInfo.ImplementedInterfaces.Contains(typeof(IList)))
                    {
                        var childs = (IList)prop.GetValue(node);
                        foreach (var child in childs)
                        {
                            descendants.Add((Node)child);
                            ((Node)child).GetDescendants(descendants);
                        }
                    }
                }
            }
        }
    }
}
