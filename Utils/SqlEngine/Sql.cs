using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

using Microsoft.SqlServer.Server;

namespace Utils.SqlEngine
{
    public enum QType { Non, Scalar, Reader, Set }

    public class Sql : IDisposable
    {
        #region Properties
        protected bool _disposed = false;
        
        public SqlConnection Connection { get; set; }
        public SqlTransaction Transaction { get; set; }
        
        #endregion

        #region Constructors
        public Sql() { }
        public Sql(SqlConnection conn) => Connection = conn;
        public Sql(string connectionString) => Connection = new SqlConnection(connectionString);

        #endregion

        #region Utilities
        public static bool TestConnect(SqlConnection conn)
        {
            try
            {
                if (conn != null) conn.Open();
                else throw new Exception("Connection string is empty.");
                conn.Close();
            }
            catch (Exception)
            { return false; }
            return true;
        }

        private SqlCommand CreateCmd(string qry, CommandType type, params object[] args)
        {
            var cmd = CreateCmd(Connection, qry, type, args);

            if (Transaction != null) cmd.Transaction = Transaction;

            return cmd;
        }
        public static SqlCommand CreateCmd(SqlConnection conn, string qry, CommandType type, params object[] args) =>
            LoadParams(new SqlCommand(qry, conn) {
                CommandTimeout = 300,
                CommandType = type
            }, args);

        public static SqlCommand LoadParams(SqlCommand cmd, object[] args)
        {
            for (int i = 0; i < args.Length; i++)
                if (args[i] is SqlParameter) cmd.Parameters.Add((SqlParameter)args[i]);
                else if (args[i] is string && i < (args.Length - 1)) cmd.Parameters.AddWithValue(args[i].ToString(), args[++i]);
                else throw new ArgumentException("Invalid number or type of arguments supplied");

            return cmd;
        }
        
        #endregion
        
        #region Exec Members

        public object ExecQuery(QType type, string sql, params object[] args)
        {
            using (var cmd = CreateCmd(sql, CommandType.Text, args))
                return Execute(cmd, type);
        }
        public static object ExecQuery(string conn, QType type, string sql, params object[] args) => 
            ExecQuery(new SqlConnection(conn), type, sql, args);
        public static object ExecQuery(SqlConnection sqlconn, QType type, string sql, params object[] args)
        {
            if (type == QType.Reader) //Static execution does not support DataReader
                throw new Exception("Static execution for SqlReader is not supported.");

            using (var conn = new SqlConnection(sqlconn.ConnectionString))
            using (var cmd = CreateCmd(conn, sql, CommandType.Text, args))
                return Execute(cmd, type);
        }

        public object ExecProc(QType type, string proc, params object[] args)
        {
            using (var cmd = CreateCmd(proc, CommandType.StoredProcedure, args))
                return Execute(cmd, type);
        }
        public static object ExecProc(string conn, QType type, string proc, params object[] args) => 
            ExecProc(new SqlConnection(conn), type, proc, args);
        public static object ExecProc(SqlConnection sqlconn, QType type, string proc, params object[] args)
        {
            if (type == QType.Reader) //Static execution does not support DataReader
                throw new Exception("Static execution for SqlReader is not supported.");

            using (var conn = new SqlConnection(sqlconn.ConnectionString))
            using (var cmd = CreateCmd(conn, proc, CommandType.StoredProcedure, args))
                return Execute(cmd, type);
        }

        public static SqlParameter TVP(IEnumerable<SqlDataRecord> tvp, string name) =>
            new SqlParameter {
                SqlDbType = SqlDbType.Structured,
                Value = tvp,
                Direction = ParameterDirection.Input,
                ParameterName = name
            };

        private static object Execute(SqlCommand cmd, QType type)
        {
            if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();

            switch (type)
            {
                case QType.Non: return cmd.ExecuteNonQuery();
                case QType.Scalar: return cmd.ExecuteScalar();
                case QType.Reader: return cmd.ExecuteReader(); ;
                case QType.Set:
                    var set = new DataSet();
                    using (var a = new SqlDataAdapter(cmd)) a.Fill(set);
                    return set;
                default: return null;
            }
        }

        #endregion

        #region Transaction Members

        /// <summary>
        /// Begins a transaction
        /// </summary>
        /// <returns>The new SqlTransaction object</returns>
        public SqlTransaction Begin()
        {
            if (Connection != null)
            {
                if (Connection.State != ConnectionState.Open) Connection.Open();

                Rollback();
                Transaction = Connection.BeginTransaction();
                return Transaction;
            }
            else throw new Exception("Connection not set.");
        }

        /// <summary>
        /// Commits any transaction in effect.
        /// </summary>
        public void Commit()
        {
            if (Transaction != null)
            {
                Transaction.Commit();
                Transaction = null;
            }
        }

        /// <summary>
        /// Rolls back any transaction in effect.
        /// </summary>
        public void Rollback()
        {
            if (Transaction != null)
            {
                Transaction.Rollback();
                Transaction = null;
            }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                // Need to dispose managed resources if being called manually
                if (disposing && Connection != null)
                {
                    Rollback();
                    Connection.Dispose();
                    Connection = null;
                }
                _disposed = true;
            }
        }

        #endregion
    }
}