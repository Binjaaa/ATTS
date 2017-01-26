using ATTS.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace ATTS.Infrastructure.Extensions
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Extension method to convert IEnumerable<T> to DataTable
        /// </summary>
        /// <typeparam name="T">Type of IEnumeration</typeparam>
        /// <param name="data"></param>
        /// <returns>Converted DataTable</returns>
        public static DataTable AsDataTable<T>(this IEnumerable<T> data)
        {
            var properties = typeof(T).GetProperties()
                                      .Where(pi => !Attribute.IsDefined(pi, typeof(SkipPropertyAttribute)))
                                      .ToArray();

            var table = new DataTable();

            //Init table mapping
            foreach (PropertyInfo prop in properties)
            {
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyInfo propInfo in properties)
                {
                    row[propInfo.Name] = propInfo.GetValue(item) ?? DBNull.Value;
                }

                table.Rows.Add(row);
            }
            return table;
        }
    }
}