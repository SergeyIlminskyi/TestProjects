using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Text;
using System.Web;

namespace Moneyveo.TestTask.Helpers
{
    public class CSVParser
    {
        #region Import CVS
        public static void ExportCSV(IMatrixModel matrix, System.IO.StreamWriter file, char separatorCVS)
        {
            for (int i = 0; i < matrix.Size; i++)
            {
                file.WriteLine(string.Join(separatorCVS.ToString(), matrix.Body[i]));
            }
        }
        #endregion

        #region Import CVS
        public static List<T> ImportCSV<T>(HttpPostedFileBase file, List<PrintColumnData> columns, char separator)
            where T : new()
        {
            List<T> returnList = new List<T>();
            string debugMark = String.Empty;
            string lastProcessedProperty = String.Empty;
            try
            {
                MemoryStream stream = new MemoryStream();
                file.InputStream.CopyTo(stream);
                StreamReader sr = new StreamReader(stream, Encoding.UTF8);

                string aa = sr.ReadToEnd();
                stream.Seek(0, SeekOrigin.Begin);
                T element;
                string[] lineArray;

                if (!sr.EndOfStream)
                {
                    var header = sr.ReadLine();//header
                    if (header != null && header.StartsWith("sep"))
                    {
                        sr.ReadLine();//header2
                    }
                }

                string line;
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine() ?? string.Empty;
                    var trimmed = line.Trim('"');
                    lineArray = trimmed.Split(new char[] { separator }, StringSplitOptions.None);
                    element = new T();
                    for (int i = 0; i < columns.Count; i++)
                    {
                        debugMark = String.Format("Row '{0}', Column index '{1}', ColumnName '{2}', ColumnValue '{3}'.", returnList.Count, i, columns[i].ColumnName, lineArray[i].Replace("<ENTER>", "\n"));
                        SetValueFromGenericObject<T>(element, columns[i].ColumnName, lineArray[i].Replace("<ENTER>", "\n"), out lastProcessedProperty);
                    }
                    debugMark = String.Empty;
                    returnList.Add(element);

                }

            }
            catch (Exception ex)
            {
                if (ex is System.FormatException && !String.IsNullOrEmpty(lastProcessedProperty))
                    debugMark = String.Format("{0} Expected type {1}. ", debugMark, lastProcessedProperty);
                //throw new CSVGeneratingException(ex.Message, ex.InnerException, debugMark);
            }

            return returnList;
        }
        #endregion

        #region Helpers

        /// <summary>
        /// Gets the value from generic object using Reflection
        /// Nested object... 
        /// ex: MyClass.DoubleClass.Property 
        /// </summary>
        /// <typeparam name="T">type of object</typeparam>
        /// <param name="item">The item object</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns>value of specific item and propert fieldName</returns>
        private static object GetValueFromGenericObject<T>(T item, string fieldName)
        {
            string[] nestedFields = fieldName.Split('.');
            if (nestedFields.Length == 1)
            {
                MemberInfo member = item.GetType().GetMember(fieldName)[0];
                if (member.MemberType == MemberTypes.Property)
                    return item.GetType().GetProperty(fieldName).GetValue(item, null);
                if (member.MemberType == MemberTypes.Field)
                    return item.GetType().GetField(fieldName).GetValue(item);
                return null;
            }
            else if (nestedFields.Length == 2)
            {
                object nestedType = item.GetType().GetField(nestedFields[0]).GetValue(item);

                if (nestedType.GetType().GetMember(nestedFields[1])[0].MemberType == MemberTypes.Field)
                    return nestedType.GetType().GetField(nestedFields[1]).GetValue(nestedType);
                if (nestedType.GetType().GetMember(nestedFields[1])[0].MemberType == MemberTypes.Property)
                    return nestedType.GetType().GetProperty(nestedFields[1]).GetValue(nestedType, null);
                return null;
            }
            else if (nestedFields.Length == 3)
            {
                object nestedType = item.GetType().GetField(nestedFields[0]).GetValue(item);
                object nestedTypeEx = null;

                if (nestedType.GetType().GetMember(nestedFields[1])[0].MemberType == MemberTypes.Field)
                {
                    nestedTypeEx = nestedType.GetType().GetField(nestedFields[1]).GetValue(nestedType);
                }
                if (nestedType.GetType().GetMember(nestedFields[1])[0].MemberType == MemberTypes.Property)
                {
                    nestedTypeEx = nestedType.GetType().GetProperty(nestedFields[1]).GetValue(nestedType, null);
                }

                if (nestedTypeEx == null)
                    return null;

                if (nestedTypeEx.GetType().GetMember(nestedFields[2])[0].MemberType == MemberTypes.Field)
                    return nestedTypeEx.GetType().GetField(nestedFields[2]).GetValue(nestedTypeEx);
                if (nestedTypeEx.GetType().GetMember(nestedFields[2])[0].MemberType == MemberTypes.Property)
                    return nestedTypeEx.GetType().GetProperty(nestedFields[2]).GetValue(nestedTypeEx, null);
            }

            return new Exception("Too complicated data");

        }

        /// <summary>
        /// Gets the writeable value.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <param name="separatorCVS">The separator CVS.</param>
        /// <returns></returns>
        private static string GetWriteableValue(object o, string separatorCVS)
        {

            return o.ToString() + separatorCVS;
            if (o == null || o == Convert.DBNull)
                return "";
            else if (o.ToString().IndexOf(separatorCVS) == -1)
                return o.ToString().Replace("\n", "<ENTER>");
            else
                return "\'" + o.ToString() + "\'";
        }

        /// <summary>
        /// Gets the value from generic object using Reflection
        /// </summary>
        /// <typeparam name="T">type of object</typeparam>
        /// <param name="item">The item object</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns>value of specific item and propert fieldName</returns>
        private static void SetValueFromGenericObject<T>(T item, string fieldName, object setValue, out string processedPropertyType)
        {
            processedPropertyType = String.Empty;
            if (setValue == System.DBNull.Value)
                return;

            string[] nestedFields = fieldName.Split('.');
            if (nestedFields.Length == 1)
            {
                MemberInfo member = item.GetType().GetMember(fieldName)[0];
                if (member.MemberType == MemberTypes.Property)
                {
                    Type propertyType = item.GetType().GetProperty(fieldName).PropertyType;
                    processedPropertyType = propertyType != null ? propertyType.ToString() : String.Empty;
                    if (propertyType == typeof(string))
                        item.GetType().GetProperty(fieldName).SetValue(item, setValue.ToString(), null);
                    else if (propertyType == typeof(bool))
                        item.GetType().GetProperty(fieldName).SetValue(item, bool.Parse((string)setValue), null);
                    else if (propertyType == typeof(Nullable<int>))
                        item.GetType().GetProperty(fieldName).SetValue(item, String.IsNullOrWhiteSpace((string)setValue) ? (int?)null : int.Parse((string)setValue), null);
                    else if (propertyType == typeof(Nullable<long>))
                        item.GetType().GetProperty(fieldName).SetValue(item, String.IsNullOrWhiteSpace((string)setValue) ? (long?)null : long.Parse((string)setValue), null);
                    else if (propertyType == typeof(long))
                        item.GetType().GetProperty(fieldName).SetValue(item, long.Parse((string)setValue), null);
                    else if (propertyType == typeof(Nullable<decimal>))
                        item.GetType().GetProperty(fieldName).SetValue(item, String.IsNullOrWhiteSpace((string)setValue) ? (decimal?)null : decimal.Parse((string)setValue), null);
                    else if (propertyType == typeof(DateTime))
                        item.GetType().GetProperty(fieldName).SetValue(item, DateTime.Parse((string)setValue), null);
                    else if (propertyType == typeof(Nullable<DateTime>))
                        item.GetType().GetProperty(fieldName).SetValue(item, String.IsNullOrWhiteSpace((string)setValue) ? (DateTime?)null : DateTime.Parse((string)setValue), null);
                    else if (propertyType.IsEnum)
                        item.GetType().GetProperty(fieldName).SetValue(item, Enum.Parse(propertyType, (string)setValue), null);
                    else
                        item.GetType().GetProperty(fieldName).SetValue(item, setValue, null);

                }
                if (member.MemberType == MemberTypes.Field)
                {
                    item.GetType().GetField(fieldName).SetValue(item, setValue);
                }
                return;
            }
            else if (nestedFields.Length == 2)
            {
                object nestedType = item.GetType().GetField(nestedFields[0]).GetValue(item);

                if (nestedType.GetType().GetMember(nestedFields[1])[0].MemberType == MemberTypes.Field)
                    nestedType.GetType().GetField(nestedFields[1]).SetValue(nestedType, setValue);
                if (nestedType.GetType().GetMember(nestedFields[1])[0].MemberType == MemberTypes.Property)
                    nestedType.GetType().GetProperty(nestedFields[1]).SetValue(nestedType, setValue, null);
                return;
            }
            else if (nestedFields.Length == 3)
            {
                object nestedType = item.GetType().GetField(nestedFields[0]).GetValue(item);
                object nestedTypeEx = null;

                if (nestedType.GetType().GetMember(nestedFields[1])[0].MemberType == MemberTypes.Field)
                {
                    nestedTypeEx = nestedType.GetType().GetField(nestedFields[1]).GetValue(nestedType);
                }
                if (nestedType.GetType().GetMember(nestedFields[1])[0].MemberType == MemberTypes.Property)
                {
                    nestedTypeEx = nestedType.GetType().GetProperty(nestedFields[1]).GetValue(nestedType, null);
                }

                if (nestedTypeEx == null)
                    return;

                if (nestedTypeEx.GetType().GetMember(nestedFields[2])[0].MemberType == MemberTypes.Field)
                    nestedTypeEx.GetType().GetField(nestedFields[2]).SetValue(nestedTypeEx, setValue);
                if (nestedTypeEx.GetType().GetMember(nestedFields[2])[0].MemberType == MemberTypes.Property)
                    nestedTypeEx.GetType().GetProperty(nestedFields[2]).SetValue(nestedTypeEx, setValue, null);
                return;
            }

            throw new Exception("Too complicated data");
        }
        #endregion

        public class PrintColumnData
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="PrintColumnData"/> class.
            /// </summary>
            /// <param name="columnName">Name of the column.</param>
            /// <param name="columnString">The column string.</param>
            /// <param name="isNumberValue">if set to "true" [is number value].</param>
            public PrintColumnData(string columnName, string columnString)
            {
                this.ColumnName = columnName;
                this.ColumnString = columnString;
            }
            /// <summary>
            /// ColumnString  - it's the header of column
            /// </summary>
            public string ColumnString;
            /// <summary>
            /// Name of field or property
            /// </summary>
            public string ColumnName;
        }

    }
}