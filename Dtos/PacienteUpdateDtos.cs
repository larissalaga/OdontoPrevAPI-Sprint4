using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdontoPrevAPI.Dtos
{
    public class PacienteUpdateDtos
    {
        [MaxLength(100)]
        public string? NmPaciente { get; set; }

        //[MaxLength(20)]
        public DateTime? DtNascimento { get; set; }

        [MaxLength(30)]
        public string? NrCpf { get; set; }

        [MaxLength(1)]
        public string? DsSexo { get; set; }

        [MaxLength(30)]
        public string? NrTelefone { get; set; }

        [EmailAddress]
        [MaxLength(70)]
        public string? DsEmail { get; set; }

        [Column("id_plano")]
        [ForeignKey("Plano")]
        public int? IdPlano { get; set; }

        [MaxLength(15)]
        public string DsCodigoPlano { get; set; } = string.Empty;
    }
}
