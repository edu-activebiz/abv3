using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FMCGWebApplication.Models
{
    [Table("TISAccountMst", Schema = "dbo")]
    public class Accounts
    {
        [Key, Column(Order = 0)]
        public short CompanyCode { get; set; }  // Primary Key Part 1

        [Key, Column(Order = 1)]
        [StringLength(6)]
        public string AcCode { get; set; }  // Primary Key Part 2

        [Required]
        [StringLength(100)]
        public string AcName { get; set; }

        [StringLength(6)]
        public string GroupCode { get; set; }

        [StringLength(1)]
        public string PTG { get; set; }  // T = Trading, P = P&L, B = Balance Sheet

        [Required]
        [StringLength(1)]
        public string AcType { get; set; }  // Account Type

        public byte? TrnType { get; set; }  // Transaction Type

        [StringLength(15)]
        public string CreditAbbrv { get; set; }

        [StringLength(15)]
        public string DebitAbbrv { get; set; }

        //public byte? ARAPStatus { get; set; }
        public byte? ARAPStatus { get; set; } = 0; // Default to "None"

        [StringLength(6)]
        public string AreaCode { get; set; }

        public bool OGS { get; set; }  // Opening Goods Stock (bit)

        [StringLength(255)]
        public string Address { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(50)]
        public string Phone_STD { get; set; }

        [StringLength(50)]
        public string Phone_Off { get; set; }

        [StringLength(50)]
        public string Phone_Resi { get; set; }

        [StringLength(30)]
        public string Mobil { get; set; }

        [StringLength(30)]
        public string FaxNo { get; set; }

        [StringLength(100)]
        public string ContactPerson { get; set; }

        [StringLength(50)]
        [EmailAddress]
        public string EMail { get; set; }

        [StringLength(50)]
        public string WWW { get; set; }

        [StringLength(50)]
        public string LST_No { get; set; }

        [StringLength(50)]
        public string CST_No { get; set; }

        [StringLength(50)]
        public string PANNo { get; set; }

        [StringLength(50)]
        public string LicNo { get; set; }

        public short? CrDays { get; set; }

        public decimal? CrAmount { get; set; }  // Money type

        public bool EditLock { get; set; }

        [StringLength(20)]
        public string Machine { get; set; }

        public int? PID { get; set; }

        public int? hWnd { get; set; }

        public byte? BillType { get; set; }

        [StringLength(50)]
        public string LSTTINNO { get; set; }

        [StringLength(50)]
        public string CSTTINNO { get; set; }

        [StringLength(50)]
        public string GSTNO { get; set; }

        [StringLength(20)]
        public string AdharCardNo { get; set; }

        [StringLength(50)]
        public string StateName { get; set; }
    }
}
