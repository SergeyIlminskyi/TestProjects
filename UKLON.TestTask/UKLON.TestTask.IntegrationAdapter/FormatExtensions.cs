using System.Collections.Generic;
using System.Reflection;

namespace UKLON.TestTask.IntegrationAdapter
{
    public static class Constants
    {
        public const string Separator = " | ";
    }

    public static class FormatExtensions
    {

        public static string FormatData<T>(this T @this)
        {
            var list = new List<string>();

            var type = @this.GetType();

            foreach (var prop in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                if (prop.CanRead)
                    list.Add(string.Format("{0}: {1}", prop.Name, prop.GetValue(@this)));
            }

            return  string.Join(Constants.Separator, list.ToArray());
        }
    }
}
