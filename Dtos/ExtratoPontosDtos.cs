using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using OdontoPrevAPI.Models;

namespace OdontoPrevAPI.Dtos
{
    public class ExtratoPontosDtos
    {
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DtExtrato { get; set; }

        [Required]
        [MaxLength(10)]
        public int NrNumeroPontos { get; set; }

        [Required]
        [MaxLength(50)]
        public string DsMovimentacao { get; set; } = string.Empty;

        [Required]
        [Column("id_paciente")]
        //[MaxLength(20)]
        public int IdPaciente { get; set; }
        public Paciente? Paciente { get; set; }
    }
}
