using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdontoPrevAPI.Models
{
    [Table("T_OPBD_RESPOSTAS")]
    public class Respostas
    {
        [Key] 
        [Required]
        [Column("id_resposta")]
        //[MaxLength(20)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdResposta { get; set; }

        [Required]
        [Column("ds_resposta")]
        [MaxLength(400)]
        public string DsResposta { get; set; } = string.Empty;
    }
}   