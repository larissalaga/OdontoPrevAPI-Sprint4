using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdontoPrevAPI.Models
{
    [Table("T_OPBD_PLANO")]
    public class Plano
    {
        [Key]
        [Required]
        [Column("id_plano")]
        //[MaxLength(20)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPlano { get; set; }

        [Required]
        [Column("ds_codigo_plano")]
        [MaxLength(15)]
        public string DsCodigoPlano { get; set; } = string.Empty;

        [Required]
        [Column("nm_plano")]
        [MaxLength(60)]
        public string NmPlano { get; set; } = string.Empty;
    }
}