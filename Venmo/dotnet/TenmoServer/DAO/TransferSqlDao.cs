using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;
using System.Data.SqlClient;
namespace TenmoServer.DAO
{
    public class TransferSqlDao: ITransferDao
    {
        private readonly string connectionString;

        public TransferSqlDao(string dbConnectionString) {
            connectionString = dbConnectionString;
        }

        //add additional functionality for a send/receive AND for pending?
        //return new Transfer info?
        public void CreateTransfer(int accountId, Transfer transfer) {
            //int newTransferId;
            try {
                using (SqlConnection conn = new SqlConnection(connectionString)) {
                    conn.Open();
                    var sql = 
                        "INSERT INTO transfers (transfer_type_id, transfer_status_id, account_from, account_to, amount) " +
                        "VALUES " +
                        "((SELECT transfer_type_id FROM transfer_types WHERE transfer_type_id = @transfer_type_id), " +
                        "(SELECT transfer_status_id FROM transfer_statuses WHERE transfer_status_id = @transfer_status_id), " +
                        "(SELECT account_id FROM accounts WHERE account_id = @account_from), " +
                        "(SELECT account_id FROM accounts WHERE account_id = @account_to), " +
                        "@amount);";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@transfer_type_id", transfer.TransferTypeId);
                    cmd.Parameters.AddWithValue("@transfer_status_id", transfer.TransferStatusId);
                    cmd.Parameters.AddWithValue("@account_from", accountId);
                    cmd.Parameters.AddWithValue("@account_to", transfer.AccountTo);
                    cmd.Parameters.AddWithValue("@amount", transfer.Amount);

                    cmd.ExecuteNonQuery();

                    //newTransferId = Convert.ToInt32(cmd.ExecuteScalar());
                }
            } catch (SqlException) {
                throw;
            }
        }

        /*
         * Use case 5: View transfers
         */
        public IList<ViewTransfer> GetViewTransfers(int accountId) {
            IList<ViewTransfer> viewTransfers = new List<ViewTransfer>();

            try {
                using (SqlConnection conn = new SqlConnection(connectionString)) {
                    conn.Open();
                    var sql =
                        "SELECT t.transfer_id, fu.username AS fromName, tu.username AS toName, t.amount " +
                        "FROM transfers t  " +
                        "INNER JOIN accounts fa ON fa.account_id = t.account_from  " +
                        "INNER JOIN users fu ON fu.user_id = fa.user_id  " +
                        "INNER JOIN accounts ta ON ta.account_id = t.account_to  " +
                        "INNER JOIN users tu ON tu.user_id = ta.user_id  " +
                        "WHERE t.account_from = @accountId OR t.account_to = @accountId;";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@accountId", accountId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read()) {
                        viewTransfers.Add(GetViewTransferFromReader(reader));
                    }
                }
            } catch (SqlException) {
                throw;
            }

            return viewTransfers;
        }

        /*
         * Use case 6: Transfer details
         */
        public TransferDetails GetTransferDetails(int transferId, int accountId) {
            TransferDetails transferDetails = null;

            try {
                using (SqlConnection conn = new SqlConnection(connectionString)) {
                    conn.Open();
                    var sql =
                        "SELECT t.transfer_id, fu.username AS fromName, tu.username AS toName, tt.transfer_type_desc, ts.transfer_status_desc, t.amount " +
                        "FROM transfers t " +
                        "INNER JOIN transfer_types tt ON tt.transfer_type_id = t.transfer_type_id " +
                        "INNER JOIN transfer_statuses ts ON ts.transfer_status_id = t.transfer_status_id " +
                        "INNER JOIN accounts fa ON fa.account_id = t.account_from " +
                        "INNER JOIN users fu ON fu.user_id = fa.user_id " +
                        "INNER JOIN accounts ta ON ta.account_id = t.account_to " +
                        "INNER JOIN users tu ON tu.user_id = ta.user_id " +
                        "WHERE t.transfer_id = @transfer_id AND " +
                        "(account_from = @account_id OR account_to = @account_id);";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@transfer_id", transferId);
                    cmd.Parameters.AddWithValue("@account_id", accountId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read()) {
                        transferDetails = GetTransferDetailsFromReader(reader);
                    }
                }
            } catch (SqlException) {
                throw;
            }

            return transferDetails;
        }

        private Transfer GetTransferFromReader(SqlDataReader reader) {
            Transfer transfer = new Transfer() {
                TransferId = Convert.ToInt32(reader["transfer_id"]),
                TransferTypeId = Convert.ToInt32(reader["transfer_type_id"]),
                TransferStatusId = Convert.ToInt32(reader["transfer_status_id"]),
                AccountFrom = Convert.ToInt32(reader["account_from"]),
                AccountTo = Convert.ToInt32(reader["account_to"]),
                Amount = Convert.ToDecimal(reader["amount"])
            };

            return transfer;
        }

        private ViewTransfer GetViewTransferFromReader(SqlDataReader reader) {
            ViewTransfer viewTransfer = new ViewTransfer() {
                TransferId = Convert.ToInt32(reader["transfer_id"]),
                FromName = Convert.ToString(reader["fromName"]),
                ToName = Convert.ToString(reader["toName"]),
                Amount = Convert.ToDecimal(reader["amount"])
            };

            return viewTransfer;
        }

        private TransferDetails GetTransferDetailsFromReader(SqlDataReader reader) {
            TransferDetails transferDetails = new TransferDetails() {
                TransferId = Convert.ToInt32(reader["transfer_id"]),
                FromName = Convert.ToString(reader["fromName"]),
                ToName = Convert.ToString(reader["toName"]),
                TransferTypeDesc = Convert.ToString(reader["transfer_type_desc"]),
                TransferStatusDesc = Convert.ToString(reader["transfer_status_desc"]),
                Amount = Convert.ToDecimal(reader["amount"])
            };

            return transferDetails;
        }
    }
}
