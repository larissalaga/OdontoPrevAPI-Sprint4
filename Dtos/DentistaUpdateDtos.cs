using System.ComponentModel.DataAnnotations;

namespace OdontoPrevAPI.Dtos
{
    public class DentistaUpdateDtos
    {
        [MaxLength(100)]
        public string? NmDentista { get; set; }

        [MaxLength(20)]
        public string? DsCro { get; set; }

        [MaxLength(70)]
        public string? DsEmail { get; set; }

        [MaxLength(30)]
        public string? NrTelefone { get; set; }

        [MaxLength(20)]
        public string? DsDocIdentificacao { get; set; }
    }
}
