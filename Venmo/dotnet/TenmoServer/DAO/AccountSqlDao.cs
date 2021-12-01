using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public class AccountSqlDao : IAccountDao
    {
        private readonly string connectionString;

        public AccountSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public Account GetAccount(int userId) {
            Account returnAccount = null;

            try {
                using (SqlConnection conn = new SqlConnection(connectionString)) {
                    conn.Open();
                    var sql =
                        "SELECT a.account_id, a.user_id, a.balance " +
                        "FROM accounts a " +
                        "JOIN users u ON u.user_id = a.user_id " +
                        "WHERE u.user_id = @user_id";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@user_id", userId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read()) {
                        returnAccount = GetAccountFromReader(reader);
                    }
                }
            } catch (SqlException) {
                throw;
            }

            return returnAccount;
        }

        public Account AccountChecker(int accountId) {
            Account returnAccount = null;

            try {
                using (SqlConnection conn = new SqlConnection(connectionString)) {
                    conn.Open();
                    var sql =
                        "SELECT account_id, user_id, balance " +
                        "FROM accounts a " +
                        "WHERE account_id = @account_id";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@account_id", accountId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read()) {
                        returnAccount = GetAccountFromReader(reader);
                    }
                }
            } catch (SqlException) {
                throw;
            }

            return returnAccount;
        }

        //todo consider combining both add and subtract into one method
        public void AddToBalance(Transfer transfer) {
            try {
                using (SqlConnection conn = new SqlConnection(connectionString)) {
                    conn.Open();

                    var sql = "UPDATE accounts SET balance += @amount WHERE account_id = @account_to;";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@amount", transfer.Amount);                    
                    cmd.Parameters.AddWithValue("@account_to", transfer.AccountTo);
                    

                    cmd.ExecuteNonQuery();
                }
            } catch (SqlException) {
                throw;
            }
        }

        public void SubtractFromBalance(Transfer transfer) {
            try {
                using (SqlConnection conn = new SqlConnection(connectionString)) {
                    conn.Open();

                    var sql = "UPDATE accounts SET balance -= @amount WHERE account_id = @account_from;";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@amount", transfer.Amount);
                    cmd.Parameters.AddWithValue("@account_from", transfer.AccountFrom);
                    

                    cmd.ExecuteNonQuery();
                }
            } catch (SqlException) {
                throw;
            }
        }

        private Account GetAccountFromReader(SqlDataReader reader) {
            Account account = new Account() {
                AccountId = Convert.ToInt32(reader["account_id"]),
                UserId = Convert.ToInt32(reader["user_id"]),
                Balance = Convert.ToDecimal(reader["balance"]),
            };

            return account;
        }

    }
}
