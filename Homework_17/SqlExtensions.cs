using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Homework_17
{
    public static class SqlExtensions
    {
        public static int ExecNonQuery(this SqlCommand cmd, string query)
        {
            cmd.CommandText = query;
            return cmd.ExecuteNonQuery();
        }

        public static SqlDataReader ExecReader(this SqlCommand cmd, string query)
        {
            cmd.CommandText = query;
            return cmd.ExecuteReader();
        }

        public static object ExecScalar(this SqlCommand cmd, string query)
        {
            cmd.CommandText = query;
            return cmd.ExecuteScalar();
        }
    }
}
