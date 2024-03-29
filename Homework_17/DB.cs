﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Windows;

namespace Homework_17
{
    public class DB
    {
        public static readonly DB Connection = new();
        public static readonly SqlCommand Cmd = Connection.Com();

        public SqlCommand Com()
        {
            string connectString = $"Server=(localdb)\\mssqllocaldb;Database=bank;Trusted_Connection=True;";

            SqlCommand cmd = new();

            try
            {
                cmd.Connection = new SqlConnection(connectString);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "DB error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return cmd;
        }
    }
}
