using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Collections;

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


            Console.Out.WriteLine(user.ToJSONReflection());

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
                var arr = string.Join(Constants.Separator, itemsJSON);
                return string.Format(Constants.Array, arr);
            }

            var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            var propertiesArray = from prop in properties
                                  where prop.CanRead
                             select          
                                string.Format(Constants.Property,
                               prop.Name,
                               prop.GetValue(@this).ToJSONReflection());

            var list = new List<string>(propertiesArray).ToArray();

            var result = "{" + string.Join(Constants.Separator, list) + "}";


            return result; 
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
