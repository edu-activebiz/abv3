using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace FMCGWebApplication.Models
{
    [Table("TISGroupMst", Schema = "dbo")]
    public class Groups
    {
        [Key, Column(Order = 0)]
        public short CompanyCode { get; set; }

        [Key, Column(Order = 1)]
        [StringLength(6)]
        public string GroupCode { get; set; }

        [Required, StringLength(50)]
        public string GroupName { get; set; }

        public byte? CrSeq { get; set; }
        public byte? DrSeq { get; set; }
        public char? PTG { get; set; }
        public bool EditLock { get; set; }

        [StringLength(50)]
        public string? Machine { get; set; }

        public int? PID { get; set; }
        public int? hWnd { get; set; }
    }
}
