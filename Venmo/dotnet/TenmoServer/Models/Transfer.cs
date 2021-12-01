using System;
using System.ComponentModel.DataAnnotations;

namespace TenmoServer.Models
{
    public class Transfer
    {
        // Properties
        [Required]
        public int TransferId { get; set; }
        [Range(1,2)]
        public int TransferTypeId { get; set; }
        [Range(1,3)]
        public int TransferStatusId { get; set; }
        [Required]
        public int AccountFrom { get; set; }
        [Required]
        public int AccountTo { get; set; }
        [Required]
        public decimal Amount { get; set; }
    }

    public class ViewTransfer
    {
        // Properties
        [Required]
        public int TransferId { get; set; }
        [Required]
        public string FromName { get; set; }
        [Required]
        public string ToName { get; set; }
        [Required]
        public decimal Amount { get; set; }
    }


    public class TransferDetails
    {
        //  Properties
        [Required]
        public int TransferId { get; set; }
        [Required]
        public string FromName { get; set; }
        [Required]
        public string ToName { get; set; }
        [Required]
        public string TransferTypeDesc { get; set; }
        [Required]
        public string TransferStatusDesc { get; set; }
        [Required]
        public decimal Amount { get; set; }
    }
}
