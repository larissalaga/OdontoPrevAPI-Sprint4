using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdontoPrevAPI.Models
{
    [Table("T_OPBD_EXTRATO_PONTOS")]
    public class ExtratoPontos
    {
        [Key]
        [Required]
        [Column("id_extrato_pontos")]
        //[MaxLength(20)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdExtratoPontos { get; set; }

        [Required]
        [Column("dt_extrato")]
        [DataType(DataType.DateTime)]
        public DateTime DtExtrato { get; set; }

        [Required]
        [Column("nr_numero_pontos")]
        //[MaxLength(10)]
        public int NrNumeroPontos { get; set; }

        [Required]
        [Column("ds_movimentacao")]
        [MaxLength(50)]
        public string DsMovimentacao { get; set; } = string.Empty;

        [Required]
        [Column("id_paciente")]
        [ForeignKey("Paciente")]        
        //[MaxLength(20)]
        public int IdPaciente { get; set; }
        public Paciente? Paciente { get; set; } 
    }
}   