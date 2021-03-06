﻿using Repository.Repository.DatabaseConnection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public abstract class EntityRepository
    {
        protected SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(DatabaseConfig.connectionString);
            return connection;
        }
        protected SqlCommand CreateCommand(string command, SqlConnection connection)
        {
            SqlCommand sqlCommand = new SqlCommand(command, connection);
            return sqlCommand;
        }

    }
    public static class ReaderExtension
    {
        public static int GetInt32ByName(this SqlDataReader sqlDataReader, string columnName)
        {
            return sqlDataReader.GetInt32(sqlDataReader.GetOrdinal(columnName));
        }
        public static string GetStringByName(this SqlDataReader sqlDataReader, string columnName)
        {
            return sqlDataReader.GetString(sqlDataReader.GetOrdinal(columnName));
        }
        public static bool GetBoolByName(this SqlDataReader sqlDataReader, string columnName)
        {
            return sqlDataReader.GetBoolean(sqlDataReader.GetOrdinal(columnName));
        }
        public static DateTime GetDateTimeByName(this SqlDataReader sqlDataReader, string columnName)
        {
            return sqlDataReader.GetDateTime(sqlDataReader.GetOrdinal(columnName));
        }
        public static byte GetByteByName(this SqlDataReader sqlDataReader, string columnName)
        {
            return sqlDataReader.GetByte(sqlDataReader.GetOrdinal(columnName));
        }

        public static decimal GetDecimalByName(this SqlDataReader sqlDataReader, string columnName)
        {
            return sqlDataReader.GetDecimal(sqlDataReader.GetOrdinal(columnName));
        }
    }
}
