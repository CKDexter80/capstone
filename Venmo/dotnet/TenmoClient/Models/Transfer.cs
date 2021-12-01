using System;
using System.Collections.Generic;
using System.Text;

namespace TenmoClient.Models
{
    class Transfer
    {
        // Properties
        public int TransferId { get; set; } = 0;
        public int TransferTypeId { get; set; } = 2;
        public int TransferStatusId { get; set; } = 2;
        public int AccountFrom { get; set; }
        public int AccountTo { get; set; }
        public decimal Amount { get; set; }
    }

    public class ViewTransfer
    {
        // Properties
        public int TransferId { get; set; }
        public string FromName { get; set; }
        public string ToName { get; set; }
        public decimal Amount { get; set; }
    }


    public class TransferDetails
    {
        //  Properties
        public int TransferId { get; set; }
        public string FromName { get; set; }
        public string ToName { get; set; }
        public string TransferTypeDesc { get; set; }
        public string TransferStatusDesc { get; set; }
        public decimal Amount { get; set; }
    }
}
