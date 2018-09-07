using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Extensions
{
    public static class DataTables
    {
        /// <summary>
        /// Convert a DataTable object into a CSV string for file output.
        /// </summary>
        /// <param name="tbl">The DataTable object to be converted.</param>
        /// <returns>A string representing row deliminated by line feeds and cell values deliminated with commas including column headers.</returns>
        public static string ToCSV(this DataTable tbl)
        {
            using (var sw = new StringWriter())
            {
                sw.WriteLine(string.Join(",", tbl.Columns.Cast<DataColumn>().Select(col => col.ColumnName).ToArray()));

                foreach (var row in tbl.Rows.Cast<DataRow>())
                    sw.WriteLine(string.Join(",", row.ItemArray));

                return sw.ToString();
            }
        }
    }
}
