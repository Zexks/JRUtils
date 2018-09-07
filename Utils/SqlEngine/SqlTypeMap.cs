using System;
using System.Collections.Generic;
using System.Data;

namespace Utils.SqlEngine
{
    public static class SqlTypeMap
    {
        private static readonly Dictionary<Type, DbType> TypeToDbType
            = new Dictionary<Type, DbType> {
                  {typeof (byte), DbType.Byte},
                  {typeof (sbyte), DbType.SByte},
                  {typeof (short), DbType.Int16},
                  {typeof (ushort), DbType.UInt16},
                  {typeof (int), DbType.Int32},
                  {typeof (uint), DbType.UInt32},
                  {typeof (long), DbType.Int64},
                  {typeof (ulong), DbType.UInt64},
                  {typeof (float), DbType.Single},
                  {typeof (double), DbType.Double},
                  {typeof (decimal), DbType.Decimal},
                  {typeof (bool), DbType.Boolean},
                  {typeof (string), DbType.String},
                  {typeof (char), DbType.StringFixedLength},
                  {typeof (Guid), DbType.Guid},
                  {typeof (DateTime), DbType.DateTime},
                  {typeof (DateTimeOffset), DbType.DateTimeOffset},
                  {typeof (byte[]), DbType.Binary},
                  {typeof (byte?), DbType.Byte},
                  {typeof (sbyte?), DbType.SByte},
                  {typeof (short?), DbType.Int16},
                  {typeof (ushort?), DbType.UInt16},
                  {typeof (int?), DbType.Int32},
                  {typeof (uint?), DbType.UInt32},
                  {typeof (long?), DbType.Int64},
                  {typeof (ulong?), DbType.UInt64},
                  {typeof (float?), DbType.Single},
                  {typeof (double?), DbType.Double},
                  {typeof (decimal?), DbType.Decimal},
                  {typeof (bool?), DbType.Boolean},
                  {typeof (char?), DbType.StringFixedLength},
                  {typeof (Guid?), DbType.Guid},
                  {typeof (DateTime?), DbType.DateTime},
                  {typeof (DateTimeOffset?), DbType.DateTimeOffset}
            };

        private static readonly Dictionary<Type, SqlDbType> TypeToSqlDbType
            = new Dictionary<Type, SqlDbType> {
                  {typeof (byte), SqlDbType.TinyInt},
                  {typeof (sbyte), SqlDbType.TinyInt},
                  {typeof (short), SqlDbType.SmallInt},
                  {typeof (ushort), SqlDbType.SmallInt},
                  {typeof (int), SqlDbType.Int},
                  {typeof (uint), SqlDbType.Int},
                  {typeof (long), SqlDbType.BigInt},
                  {typeof (ulong), SqlDbType.BigInt},
                  {typeof (float), SqlDbType.Float},
                  {typeof (double), SqlDbType.Money},
                  {typeof (decimal), SqlDbType.Decimal},
                  {typeof (bool), SqlDbType.Bit},
                  {typeof (string), SqlDbType.NVarChar},
                  {typeof (char), SqlDbType.NChar},
                  {typeof (Guid), SqlDbType.UniqueIdentifier},
                  {typeof (DateTime), SqlDbType.DateTime},
                  {typeof (DateTimeOffset), SqlDbType.DateTimeOffset},
                  {typeof (byte[]), SqlDbType.Binary},
                  {typeof (byte?), SqlDbType.Binary},
                  {typeof (sbyte?), SqlDbType.TinyInt},
                  {typeof (short?), SqlDbType.SmallInt},
                  {typeof (ushort?), SqlDbType.SmallInt},
                  {typeof (int?), SqlDbType.Int},
                  {typeof (uint?), SqlDbType.Int},
                  {typeof (long?), SqlDbType.BigInt},
                  {typeof (ulong?), SqlDbType.BigInt},
                  {typeof (float?), SqlDbType.Float},
                  {typeof (double?), SqlDbType.Money},
                  {typeof (decimal?), SqlDbType.Decimal},
                  {typeof (bool?), SqlDbType.Bit},
                  {typeof (char?), SqlDbType.NChar},
                  {typeof (Guid?), SqlDbType.UniqueIdentifier},
                  {typeof (DateTime?), SqlDbType.DateTime},
                  {typeof (DateTimeOffset?), SqlDbType.DateTimeOffset}
            };

        private static readonly Dictionary<SqlDbType, Type> SqlDbTypeToType
            = new Dictionary<SqlDbType, Type> {
                  {SqlDbType.BigInt, typeof (long)},
                  {SqlDbType.Binary, typeof (byte[])},
                  {SqlDbType.Image, typeof (byte[])},
                  {SqlDbType.Timestamp, typeof (byte[])},
                  {SqlDbType.VarBinary, typeof (byte[])},
                  {SqlDbType.Bit, typeof (bool)},
                  {SqlDbType.Char, typeof (string)},
                  {SqlDbType.NChar, typeof (string)},
                  {SqlDbType.NText, typeof (string)},
                  {SqlDbType.NVarChar, typeof (string)},
                  {SqlDbType.Text, typeof (string)},
                  {SqlDbType.VarChar, typeof (string)},
                  {SqlDbType.Xml, typeof (string)},
                  {SqlDbType.DateTime, typeof (DateTime)},
                  {SqlDbType.SmallDateTime, typeof (DateTime)},
                  {SqlDbType.Date, typeof (DateTime)},
                  {SqlDbType.Time, typeof (DateTime)},
                  {SqlDbType.DateTime2, typeof (DateTime)},
                  {SqlDbType.Decimal, typeof (decimal)},
                  {SqlDbType.Money, typeof (decimal)},
                  {SqlDbType.SmallMoney, typeof (decimal)},
                  {SqlDbType.Float, typeof (double)},
                  {SqlDbType.Int, typeof (int)},
                  {SqlDbType.Real, typeof (float)},
                  {SqlDbType.UniqueIdentifier, typeof (Guid)},
                  {SqlDbType.SmallInt, typeof (short)},
                  {SqlDbType.TinyInt, typeof (byte)},
                  {SqlDbType.Variant, typeof (object)},
                  {SqlDbType.Udt, typeof (object)},
                  {SqlDbType.Structured, typeof (DataTable)},
                  {SqlDbType.DateTimeOffset, typeof (DateTimeOffset)}
            };

        private static readonly Dictionary<SqlDbType, Type> SqlDbTypeToNullableType
            = new Dictionary<SqlDbType, Type> {
                  {SqlDbType.BigInt, typeof (long?)},
                  {SqlDbType.Binary, typeof (byte[])},
                  {SqlDbType.Image, typeof (byte[])},
                  {SqlDbType.Timestamp, typeof (byte[])},
                  {SqlDbType.VarBinary, typeof (byte[])},
                  {SqlDbType.Bit, typeof (bool?)},
                  {SqlDbType.Char, typeof (string)},
                  {SqlDbType.NChar, typeof (string)},
                  {SqlDbType.NText, typeof (string)},
                  {SqlDbType.NVarChar, typeof (string)},
                  {SqlDbType.Text, typeof (string)},
                  {SqlDbType.VarChar, typeof (string)},
                  {SqlDbType.Xml, typeof (string)},
                  {SqlDbType.DateTime, typeof (DateTime?)},
                  {SqlDbType.SmallDateTime, typeof (DateTime?)},
                  {SqlDbType.Date, typeof (DateTime?)},
                  {SqlDbType.Time, typeof (DateTime?)},
                  {SqlDbType.DateTime2, typeof (DateTime?)},
                  {SqlDbType.Decimal, typeof (decimal?)},
                  {SqlDbType.Money, typeof (decimal?)},
                  {SqlDbType.SmallMoney, typeof (decimal?)},
                  {SqlDbType.Float, typeof (double?)},
                  {SqlDbType.Int, typeof (int?)},
                  {SqlDbType.Real, typeof (float?)},
                  {SqlDbType.UniqueIdentifier, typeof (Guid?)},
                  {SqlDbType.SmallInt, typeof (short?)},
                  {SqlDbType.TinyInt, typeof (byte?)},
                  {SqlDbType.Variant, typeof (object)},
                  {SqlDbType.Udt, typeof (object)},
                  {SqlDbType.Structured, typeof (DataTable)},
                  {SqlDbType.DateTimeOffset, typeof (DateTimeOffset)}
            };

        private static readonly Dictionary<DbType, Type> DbTypeMapToType
            = new Dictionary<DbType, Type> {
                  {DbType.Byte, typeof (byte)},
                  {DbType.SByte, typeof (sbyte)},
                  {DbType.Int16, typeof (short)},
                  {DbType.UInt16, typeof (ushort)},
                  {DbType.Int32, typeof (int)},
                  {DbType.UInt32, typeof (uint)},
                  {DbType.Int64, typeof (long)},
                  {DbType.UInt64, typeof (ulong)},
                  {DbType.Single, typeof (float)},
                  {DbType.Double, typeof (double)},
                  {DbType.Decimal, typeof (decimal)},
                  {DbType.Boolean, typeof (bool)},
                  {DbType.String, typeof (string)},
                  {DbType.StringFixedLength, typeof (char)},
                  {DbType.Guid, typeof (Guid)},
                  {DbType.DateTime, typeof (DateTime)},
                  {DbType.DateTimeOffset, typeof (DateTimeOffset)},
                  {DbType.Binary, typeof (byte[])}
            };

        private static readonly Dictionary<DbType, Type> DbTypeMapToNullableType
            = new Dictionary<DbType, Type> {
                  {DbType.Byte, typeof (byte?)},
                  {DbType.SByte, typeof (sbyte?)},
                  {DbType.Int16, typeof (short?)},
                  {DbType.UInt16, typeof (ushort?)},
                  {DbType.Int32, typeof (int?)},
                  {DbType.UInt32, typeof (uint?)},
                  {DbType.Int64, typeof (long?)},
                  {DbType.UInt64, typeof (ulong?)},
                  {DbType.Single, typeof (float?)},
                  {DbType.Double, typeof (double?)},
                  {DbType.Decimal, typeof (decimal?)},
                  {DbType.Boolean, typeof (bool?)},
                  {DbType.StringFixedLength, typeof (char?)},
                  {DbType.Guid, typeof (Guid?)},
                  {DbType.DateTime, typeof (DateTime?)},
                  {DbType.DateTimeOffset, typeof (DateTimeOffset?)},
                  {DbType.Binary, typeof(byte[])}
            };

        private static T2 GetConversion<T1, T2>(Dictionary<T1, T2> dict, T1 type, string src, string tgt) =>
            (dict.TryGetValue(type, out var dbType)) ? dbType : throw new ArgumentOutOfRangeException(src, type, $"Cannot map the {src} to {tgt}");

        public static DbType ToDbType(this Type type) => GetConversion(TypeToDbType, type, "Type", "DbType");
        public static SqlDbType ToSqlDbType(this Type type) => GetConversion(TypeToSqlDbType, type, "Type", "SqlDbType");
        public static Type ToClrType(this DbType type) => GetConversion(DbTypeMapToType, type, "DbType", "Type");
        public static Type ToNullableClrType(this DbType type) => GetConversion(DbTypeMapToNullableType, type, "DbType", "Nullable Type");
        public static Type ToClrType(this SqlDbType type) => GetConversion(SqlDbTypeToType, type, "SqlDbType", "Type");
        public static Type ToNullableClrType(this SqlDbType type) => GetConversion(SqlDbTypeToNullableType, type, "SqlDbType", "Nullable Type");
    }
}
