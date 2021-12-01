using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface IAccountDao {
        Account GetAccount(int userId);

        Account AccountChecker(int accountId);
        public void SubtractFromBalance(Transfer transfer);
        public void AddToBalance(Transfer transfer);
    }
}
