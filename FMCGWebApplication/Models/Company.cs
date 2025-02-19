using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FMCGWebApplication.Models
{
    [Table("TISCompanyMst")]
    public class Company
    {
        //[Key, Column(Order = 0)]
        public short CompanyCode { get; set; }

        public required string CompanyName { get; set; }
        public required string Address1 { get; set; }
        public required string Address2 { get; set; }
        public required string? Address3 { get; set; }
        public required string Address4 { get; set; }
        public string? Phone_STD { get; set; }
        public string? Phone_Off { get; set; }
        public string? Phone_Resi { get; set; }
        public string Fax { get; set; }
        public string? LST_No { get; set; }
        public string? CST_No { get; set; }
        public string? PanNo { get; set; }
        public string? EMail { get; set; }
        public string? Www { get; set; }
        public string? BusiType { get; set; }
        public string? LSTTinno { get; set; }
        public string? CSTTinno { get; set; }
        public DateTime? EInvAuthExpTime { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
