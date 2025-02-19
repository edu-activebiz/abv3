// Models/Area.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FMCGWebApplication.Models
{
    [Table("TISAreaMst")]
    public class Area
    {
        [Column(Order = 0)]
        public short CompanyCode { get; set; }

        [Column(Order = 1)]
        [StringLength(6)]
        public string? AreaCode { get; set; }

        [Required]
        [StringLength(50)]
        public string? AreaName { get; set; }

        [StringLength(50)]
        public string? AreaGroup { get; set; }

        public bool EditLock { get; set; }

        [StringLength(20)]
        public string? Machine { get; set; }

        public int? PID { get; set; }
        public int? hWnd { get; set; }

        // Foreign Key to Company Table
        //public required Company Companys { get; set; }
    }
}
