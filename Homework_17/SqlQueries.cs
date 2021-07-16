using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Homework_17
{
    public class SqlQueries
    {
        static readonly DB db = new();
        private static SqlCommand cmd = db.Com();

        public static string GetClientNameById(int clientId)
        {
            cmd.Connection.Open();

            string result = cmd.ExecScalarString("SELECT Name " +
                                                 "FROM Clients " +
                                                 $"WHERE ClientId = '{clientId}';");
            cmd.Connection.Close();
            return result;
        }

        /// <summary>
        /// Get departments list
        /// </summary>
        /// <returns></returns>
        public static List<string> DepartmentsList()
        {
            cmd.Connection.Open();
            var rawDepList = cmd.ExecReader("SELECT Name FROM Departments;");
            List<string> depList = SqlExtensions.SqlDataToList(rawDepList, "Name");
            cmd.Connection.Close();

            return depList;
        }

        /// <summary>
        /// Get clients from selected department
        /// </summary>
        /// <param name="depName">Department's name</param>
        /// <returns></returns>
        public static Dictionary<string, string> ClientsList(string depName)
        {
            cmd.Connection.Open();
            var rawClientList = cmd.ExecReader("SELECT c.Name, m.Funds " +
                                               "FROM Clients AS c " +
                                               "JOIN Departments AS d ON c.DepartId = d.Id " +
                                               "JOIN Money AS m ON m.ClientId = c.ClientId " +
                                               $"WHERE d.Name = '{depName}';");

            Dictionary<string, string> clients = SqlExtensions.SqlDataToDict(rawClientList, "Name", "Funds");

            cmd.Connection.Close();
            return clients;
        }

        /// <summary>
        /// Get clients data from selected department
        /// </summary>
        /// <param name="depName">Department's name</param>
        /// <returns></returns>
        public static List<(string, string, string)> ClientsListExtend(string depName)
        {
            cmd.Connection.Open();
            var rawClientList = cmd.ExecReader("SELECT c.ClientId, c.Name, m.Funds " +
                                               "FROM Clients AS c " +
                                               "JOIN Departments AS d ON c.DepartId = d.Id " +
                                               "JOIN Money AS m ON m.ClientId = c.ClientId " +
                                               $"WHERE d.Name = '{depName}';");

            List<(string, string, string)> clients = SqlExtensions.SqlDataTo3TupleList(rawClientList, "ClientId" ,"Name", "Funds");
            cmd.Connection.Close();

            return clients;
        }


        /// <summary>
        /// Get client's information
        /// </summary>
        /// <param name="clientName">Client's name</param>
        /// <returns></returns>
        public static List<string> DepartmentsLists(int clientId)
        {
            cmd.Connection.Open();
            var rawClientInfo = cmd.ExecReader("SELECT c.Name AS Client, d.Name AS Department, d.LoanRate, " +
                                               "d.DepositRate, m.Funds, m.Loan, m.Deposit, dt.Type AS DepositType " +
                                               "FROM Clients AS c " +
                                               "JOIN Departments AS d ON d.Id = c.DepartId " +
                                               "JOIN Money AS m ON m.ClientId = c.ClientId " +
                                               "LEFT JOIN DepositType AS dt ON dt.Id = m.DepositType " +
                                               $"WHERE c.ClientId = '{clientId}';");

            string clientsName = SqlExtensions.SqlDataToString(rawClientInfo, "Client");
            List<string> clientsNames = SqlExtensions.SqlDataToList(rawClientInfo, "Client");
            cmd.Connection.Close();

            return clientsNames;
        }

        public static int GetClientId(string clientName)
        {
            cmd.Connection.Open();

            string result = cmd.ExecScalarString("SELECT ClientId " +
                                                 "FROM Clients " +
                                                 $"WHERE Name = '{clientName}';");
            cmd.Connection.Close();

            return Convert.ToInt32(result);
        }

        public static int GetClientDepId(int clientId)
        {
            cmd.Connection.Open();

            string result = cmd.ExecScalarString("SELECT d.Id " +
                                                 "FROM Clients AS c " +
                                                 "JOIN Departments AS d ON d.Id = c.DepartId " +
                                                 $"WHERE c.ClientId = '{clientId}';");
            cmd.Connection.Close();

            return Convert.ToInt32(result);
        }

        public static string GetClientDepName(int clientId)
        {
            cmd.Connection.Open();

            string result = cmd.ExecScalarString("SELECT d.Name " +
                                                 "FROM Clients AS c " +
                                                 "JOIN Departments AS d ON d.Id = c.DepartId " +
                                                 $"WHERE c.ClientId = '{clientId}';");
            cmd.Connection.Close();

            return result;
        }

        public static double GetFundsAmount(int clientId)
        {
            cmd.Connection.Open();

            string result = cmd.ExecScalarString("SELECT m.Funds " +
                                                 "FROM Clients AS c " +
                                                 "JOIN Money AS m ON m.ClientId = c.ClientId " +
                                                 $"WHERE c.ClientId = '{clientId}';");
            cmd.Connection.Close();

            return Convert.ToDouble(result);
        }

        public static double GetFundsAmountByName(string clientName)
        {
            cmd.Connection.Open();

            string result = cmd.ExecScalarString("SELECT m.Funds " +
                                                 "FROM Clients AS c " +
                                                 "JOIN Money AS m ON m.ClientId = c.ClientId " +
                                                 $"WHERE c.Name = '{clientName}';");
            cmd.Connection.Close();

            return Convert.ToDouble(result);
        }

        /// <summary>
        /// Get loan rate of department
        /// </summary>
        /// <param name="clientName"></param>
        /// <returns></returns>
        public static int GetLoanRate(int clientId)
        {
            cmd.Connection.Open();

            string result = cmd.ExecScalarString("SELECT d.LoanRate " +
                                                 "FROM Clients AS c " +
                                                 "JOIN Departments AS d ON d.Id = c.DepartId " +
                                                 $"WHERE c.ClientId = '{clientId}';");
            cmd.Connection.Close();

            return Convert.ToInt32(result);
        }

        /// <summary>
        /// Get deposit name of department
        /// </summary>
        /// <param name="clientName"></param>
        /// <returns></returns>
        public static int GetDepositRate(int clientId)
        {
            cmd.Connection.Open();

            string result = cmd.ExecScalarString("SELECT d.DepositRate " +
                                                 "FROM Clients AS c " +
                                                 "JOIN Departments AS d ON d.Id = c.DepartId " +
                                                 $"WHERE c.ClientId = '{clientId}';");
            cmd.Connection.Close();

            return Convert.ToInt32(result);
        }


        public static double GetLoanAmount(int clientId)
        {
            cmd.Connection.Open();

            string result = cmd.ExecScalarString("SELECT m.Loan " +
                                                 "FROM Clients AS c " +
                                                 "JOIN Money AS m ON m.ClientId = c.ClientId " +
                                                 $"WHERE c.ClientId = '{clientId}';");
            cmd.Connection.Close();

            return Convert.ToDouble(result);
        }

        public static double GetDepositAmount(int clientId)
        {
            cmd.Connection.Open();

            string result = cmd.ExecScalarString("SELECT m.Deposit " +
                                                 "FROM Clients AS c " +
                                                 "JOIN Money AS m ON m.ClientId = c.ClientId " +
                                                 $"WHERE c.ClientId = '{clientId}';");
            cmd.Connection.Close();

            return Convert.ToDouble(result);
        }

        public static string GetDepositType(int clientId)
        {
            cmd.Connection.Open();

            string result = cmd.ExecScalarString("SELECT dt.Type " +
                                                 "FROM Clients AS c " +
                                                 "JOIN Money AS m ON m.ClientId = c.ClientId " +
                                                 "LEFT JOIN DepositType AS dt ON dt.Id = m.DepositType " +
                                                 $"WHERE c.ClientId = {clientId};");
            cmd.Connection.Close();

            return result;
        }

        public static void MakeDeposit(int clientId, double amount)
        {
            cmd.Connection.Open();

            var result = cmd.ExecNonQuery("UPDATE Money " +
                                          $"SET Deposit = '{amount}' " +
                                          $"WHERE ClientId = {clientId};");
            cmd.Connection.Close();
        }

        public static void GetLoan(int clientId, double amount)
        {
            cmd.Connection.Open();

            var result = cmd.ExecNonQuery("UPDATE Money " +
                                          $"SET Loan = '{amount}' " +
                                          $"WHERE ClientId = {clientId};");
            cmd.Connection.Close();
        }

        public static void ChangeFundsAmount(int clientId, double amount)
        {
            cmd.Connection.Open();

            var result = cmd.ExecNonQuery("UPDATE Money " +
                                          $"SET Funds = '{amount}' " +
                                          $"WHERE ClientId = {clientId};");
            cmd.Connection.Close();
        }

        public static string CheckDepositType(int clientId)
        {
            cmd.Connection.Open();

            var result = cmd.ExecScalarString("SELECT DepositType " +
                                              "FROM Money AS m " +
                                              "JOIN Clients AS c ON c.ClientId = m.ClientId " +
                                              $"WHERE c.ClientId = {clientId};");
            cmd.Connection.Close();

            return result;
        }

        public static void ChangeDepositType(int clientId, int depType)
        {
            cmd.Connection.Open();

            cmd.ExecNonQuery("UPDATE Money " +
                              $"SET DepositType = {depType} " +
                              $"WHERE ClientId = '{clientId}';");
            cmd.Connection.Close();
        }

        public static void AddTransaction(int clientId, string operation)
        {
            cmd.Connection.Open();

            cmd.ExecNonQuery("INSERT INTO Transactions (Operation, ClientId) " +
                             $"VALUES ('{operation}', {clientId});");
            cmd.Connection.Close();
        }
    }
}
