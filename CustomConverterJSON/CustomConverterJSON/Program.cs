using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Diagnostics;

namespace CustomConverterJSON
{
    class Program
    {
        static void Main(string[] args)
        {
            var adress1 = new Address() { Country = "Ukraine", City = "Kyiv" };
            var adress2 = new Address() { Country = "Ukraine", City = "Lvov" };
            var adress3 = new Address() { Country = "Russia", City = "Moscow" };

            var company1 = new Company() { Name = "Pentegy", Location = adress1 };
            var company2 = new Company() { Name = "EPAM", Location = adress2 };


            var companies = new List<Company>() { company1, company2 };
            var user = new User()
            {
                FirstName = "Sergey",
                LastName = "Ilminskyi",
                Age = 25,
                intArr = new int[] { 1423, 2123, 2344, 23423 },
                strArr = new string[] { "asss", "asff", "asfas" },
                IsResident = true,
                BirthdPlace = adress3,
                Companies = companies
            };

            var usersList = new List<User>();
            for (int i = 0; i < 100000 ; i++)
            {
                usersList.Add(user);
            }

            var stopwatch = Stopwatch.StartNew();
            usersList.ToJSONReflection();
            Console.Out.WriteLine(stopwatch.Elapsed.ToString());

            Console.ReadKey();
        }




    }

    public static class ObjectExtensions
    {
        public static string ToJSONReflection<T>(this T @this)
        {
            if (@this == null)
                return Constants.NULL;

            var type = @this.GetType();

            if (type.Equals(typeof(String)))
                return string.Format(Constants.String, @this.ToString());

            if (type.Equals(typeof(Boolean)))
                return @this.ToString().ToLower();

            if (type.IsValueType)
                return @this.ToString();

            var isiEnum = type.GetInterface("System.Collections.IEnumerable", false)?.IsInterface;
            if (isiEnum.HasValue && (bool)isiEnum)
            {
                var itemsJSON = new List<string>();
                foreach(var item in (IEnumerable)@this)
                {
                    itemsJSON.Add(item.ToJSONReflection());
                }
                return string.Format(Constants.Array, string.Join(Constants.Separator, itemsJSON));
            }

            var list = new List<string>();

            foreach (var prop in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                if (prop.CanRead)
                    list.Add(string.Format(Constants.Property, prop.Name, prop.GetValue(@this).ToJSONReflection()));
            }

            return  "{" + string.Join(Constants.Separator, list.ToArray()) + "}";
        }
    }

    public static class Constants
    {
        public const string Separator = ",";
        public const string Property = "\"{0}\":{1}";
        public const string Array = "[{0}]";
        public const string String = "\"{0}\"";
        public const string NULL = "null";
    }
}
