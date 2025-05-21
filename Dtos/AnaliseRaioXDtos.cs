using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using OdontoPrevAPI.Models;

namespace OdontoPrevAPI.Dtos
{
    public class AnaliseRaioXDtos
    {
        [Required]
        public string DsAnaliseRaioX { get; set; } = string.Empty;

        [Required]
        [Column("dt_analise_raio_x")]
        [DataType(DataType.DateTime)]
        public DateTime DtAnaliseRaioX { get; set; } 

        [Required]
        [Column("id_raio_x")]
        [ForeignKey("RaioX")]
        //[MaxLength(20)]
        public int IdRaioX { get; set; }
        public RaioX? RaioX { get; set; }  
    }
}
