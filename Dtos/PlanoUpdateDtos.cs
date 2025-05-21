using System.ComponentModel.DataAnnotations;

namespace OdontoPrevAPI.Dtos
{
    public class PlanoUpdateDtos
    {
        [MaxLength(15)]
        public string? DsCodigoPlano { get; set; }

        [MaxLength(60)]
        public string? NmPlano { get; set; }
    }
}
