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

        public static object ExScalar(this SqlCommand cmd, string query)
        {
            cmd.CommandText = query;
            return cmd.ExecuteScalar();
        }

        public static string ExecScalarString(this SqlCommand cmd, string query)
        {
            cmd.CommandText = query;
            var res = cmd.ExecuteScalar();
            return res.ToString();
        }

        /// <summary>
        /// Convert SqlDataReader to List
        /// </summary>
        /// <param name="data"></param>
        /// <param name="attribute">attribute name</param>
        /// <returns></returns>
        public static List<string> SqlDataToList(SqlDataReader data, string attribute)
        {
            List<string> result = new();

            if (data.HasRows)
            {
                while (data.Read())
                {
                    result.Add(data[attribute].ToString());
                }
            }
            return result;
        }

        public static Dictionary<string, string> SqlDataToDict(SqlDataReader data, string key, string value)
        {
            Dictionary<string, string> dict = new();
            string keyTmp, valueTmp;

            if (data.HasRows)
            {
                while (data.Read())
                {
                    keyTmp = data[key].ToString();
                    valueTmp = data[value].ToString();

                    dict.Add(keyTmp, valueTmp);
                }
            }
            return dict;
        }

        public static List<(string, string, string)> SqlDataTo3TupleList(SqlDataReader data, string att1, string att2, string att3)
        {
            List<(string, string, string)> dict = new();

            if (data.HasRows)
            {
                while (data.Read())
                {
                    dict.Add((data[att1].ToString(), data[att2].ToString(), data[att3].ToString()));
                }
            }

            return dict;
        }

        public static string SqlDataToString(SqlDataReader data, string attribute)
        {
            string result = string.Empty;

            if (data.HasRows)
            {
                result = data[attribute].ToString();
            }
            return result;
        }

    }
}
