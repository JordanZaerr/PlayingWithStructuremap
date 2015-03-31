using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace StructureMapExample.Tests
{
    //This is not a general purpose class and was thrown together quickly for this project.
    public class LogWrapper<T> : DynamicObject
    {
        private readonly T _instance;
        private readonly Type _type;

        public LogWrapper(T instance)
        {
            _instance = instance;
            _type = typeof (T);
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            PropertyInfo prop = _type.GetProperty(binder.Name);
            result = prop.GetValue(_instance);
            return true;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            string name = binder.Name;
            MethodInfo method = _type.GetMethod(name, args.Select(x => x.GetType()).ToArray());
            var id = Guid.NewGuid();

            Console.WriteLine(
                "Invoking method \"{0}\" on type \"{1}\" ID:\"{2}\" \r\nArguments: {3} \r\n\r\n{4}\r\n",
                name,
                FormatTypeName(_type),
                id,
                GetArgumentString(method.GetParameters(), args),
                String.Join(String.Empty, Enumerable.Repeat("=", 125)));

            result = method.Invoke(_instance, args);

            Console.WriteLine(
                "Method \"{0}\" on type \"{1}\" ID:\"{2}\" \r\nReturned{3}: {4}\r\n\r\n{5}\r\n",
                name,
                FormatTypeName(_type),
                id,
                result != null ? String.Format(" ({0})", FormatTypeName(result.GetType())) : String.Empty,
                GetReturnString(result),
                String.Join(String.Empty, Enumerable.Repeat("=", 125)));

            return true;
        }

        private string Flatten(params object[] values)
        {
            string retVal = "";
            IEnumerable iterable = values.Length == 1
                                   && values[0] is IEnumerable
                                   && !(values[0] is string)
                ? ((IEnumerable)values[0])
                : values;
            foreach (var value in iterable)
            {
                var enumerable = value as IEnumerable;
                if (enumerable != null)
                {
                    retVal += !(value is string)
                        ? "\t" + Flatten(enumerable.Cast<object>().ToList()) + ", \r\n"
                        : enumerable + (enumerable.ToString().Length > 100 ? ", \r\n" : ", ");
                }
                else
                {
                    retVal += "\t" + value + ", \r\n";
                }
            }
            return String.Format("{0}", retVal.TrimEnd(',', ' ', '\r', '\n'));
        }

        private IEnumerable<string> FlattenVariable(params object[] args)
        {
            return args.Select(x => Flatten(x));
        }

        private string FormatTypeName(Type t)
        {
            if (t.IsGenericType)
            {
                Type[] args = t.GetGenericArguments();
                string typeName = t.Name.Substring(0, t.Name.IndexOf("`"));
                return String.Format("{0}<{1}>", typeName, String.Join(",", args.Select(FormatTypeName)));
            }
            return t.Name;
        }

        private string GetArgumentString(IEnumerable<ParameterInfo> methodArgs, object[] args)
        {
            var argNames = methodArgs.Select(x => new {x.Name, x.ParameterType});
            var pairs = argNames.Zip(FlattenVariable(args), Tuple.Create);

            return args.Length != 0
                ? pairs.Aggregate(String.Empty,
                    (accum, cur) =>
                        String.Format("{0} \r\n\t{1} ({2}) = {3}",
                            accum,
                            cur.Item1.Name,
                            FormatTypeName(cur.Item1.ParameterType),
                            cur.Item2))
                : "None";
        }

        private string GetReturnString(object result)
        {
            return result != null
                ? String.Format("\r\n\t{0}", String.Join(",", FlattenVariable(result)).Trim('\t', ','))
                : "void";
        }
    }
}