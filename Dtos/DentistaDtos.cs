using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OdontoPrevAPI.Dtos
{
    public class DentistaDtos
    {
        [Required]
        [MaxLength(100)]
        public string NmDentista { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string DsCro { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(70)]
        public string DsEmail { get; set; } = string.Empty;

        [Required]
        [MaxLength(30)]
        public string NrTelefone { get; set; } = string.Empty;

        [Required]
        [MaxLength(14)]
        public string DsDocIdentificacao { get; set; } = string.Empty;
    }
}
