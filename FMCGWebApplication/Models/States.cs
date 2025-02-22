using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FMCGWebApplication.Models
{
    [Table("TISState", Schema = "dbo")]
    public class States
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // Since Id is not auto-increment
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string StateName { get; set; }

        [Required]
        public short CountryId { get; set; }
    }
}
