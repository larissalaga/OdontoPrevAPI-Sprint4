using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdontoPrevAPI.Models
{
    [Table("T_OPBD_CHECK_IN")]
    public class CheckIn
    {
        [Key]
        [Required]
        [Column("id_check_in")]
        //[MaxLength(20)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCheckIn { get; set; }

        [Required]
        [Column("dt_check_in")]
        [DataType(DataType.DateTime)]
        public DateTime DtCheckIn { get; set; }

        [Required]
        [Column("id_paciente")]
        [ForeignKey("Paciente")]
        //[MaxLength(20)]
        public int IdPaciente { get; set; }
        public Paciente? Paciente { get; set; }

        [Required]
        [Column("id_pergunta")]
        [ForeignKey("Perguntas")]
        //[MaxLength(20)]
        public int IdPergunta { get; set; }
        public Perguntas? Perguntas { get; set; }

        [Required]
        [Column("id_resposta")]
        [ForeignKey("Respostas")]
        //[MaxLength(20)]
        public int IdResposta { get; set; }
        public Respostas? Respostas { get; set; }

    }
}
