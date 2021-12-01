using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface ITransferDao
    {
        void CreateTransfer(int accountId ,Transfer transfer);
        IList<ViewTransfer> GetViewTransfers(int accountId);
        TransferDetails GetTransferDetails(int transferId, int accountId);
    }
}
