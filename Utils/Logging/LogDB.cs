using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

using Utils.SqlEngine;
using Utils.Extensions;

namespace Utils.Logging
{
    public class LogDB : LogBase, IDisposable
    {
        #region Properties
        public string Table { get; set; }
        public SqlConnection Conn { get; set; }

        public Dictionary<Tuple<int, string>, Func<object, object>> LogMap { get; set; }

        #endregion

        #region Constructors
        public LogDB() => LogMap = new Dictionary<Tuple<int, string>, Func<object, object>>();
        public LogDB(string conn) : this() { Conn = new SqlConnection(conn); }
        public LogDB(SqlConnection conn) : this(conn.ConnectionString) { }

        #endregion

        #region Utilities
        private List<object> ParseMap<T>(T o)
        {
            if (LogMap.Count < 1)
                throw new ArgumentException("LogMap must be setup for simple database logging.", "LogDB.LogMap");
            
            return LogMap.OrderBy(i => i.Key.Item1)
                         .Select(i => i.Value)
                         .Select(f => f.Invoke(o))
                         .ToList();
        }
        private string ParseData(Exception ex)
        {
            var data = ex.Data;
            var result = "{";
            foreach (var k in data.Keys)
                result += $"{k.ToString()}:'{data[k].ToString()}'{(data.Keys.OfType<object>().First() != k ? "," : "")}";

            return result += "}";
        }
        private string FormatQuery(List<object> values)
        {
            if (Table.IsNullOrEmpty())
                throw new ArgumentException("Table variable cannot be empty.", "LogDB.Table");

            var strValues = string.Empty;

            var columns = from col in LogMap
                          orderby col.Key.Item1
                          select $"[{col.Key.Item2}]";

            for (int x = 0; x < values.Count; x++)
                values[x].Use(val => {
                    if (val is null) strValues += "null";
                    else if (val is int) strValues += $"{val.ToString()}";
                    else if (val is DateTime) strValues += $"'{((DateTime)val).ToString()}'";
                    else if (val is bool) strValues += $"{Convert.ToInt16(val)}";
                    else strValues += $"'{(val.ToString()).Replace("'", "''").Trim()}'";

                    if (x < values.Count - 1) strValues += ", ";
                });

            return $"INSERT INTO {Table} ({string.Join(",", columns.ToArray())}) VALUES ({strValues})";
        }

        private void Execute(string sql) =>
            Sql.ExecQuery(Conn, QType.Non, sql);

        #endregion

        #region Writes
        public override void Write(string sql) =>
            Execute(sql);
        public override void Write(Exception ex) =>
            Execute(FormatQuery(ParseMap(ex)));
        public void Write<T>(T o) =>
            Execute(FormatQuery(ParseMap(o)));

        public void Dispose()
        {
            Conn = null;
            Table = null;
            LogMap.Clear();
        }

        #endregion

    }
}
