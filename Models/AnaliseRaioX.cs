using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdontoPrevAPI.Models
{
    [Table("T_OPBD_ANALISE_RAIO_X")]
    public class AnaliseRaioX
    {
        [Key]
        [Required]        
        [Column("id_analise_raio_x")]
        //[MaxLength(20)]        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdAnaliseRaioX { get; set; }

        [Required]
        [Column("ds_analise_raio_x")]
        public string DsAnaliseRaioX { get; set; } = string.Empty;


        [Required]
        [Column("dt_analise_raio_x")]
        [DataType(DataType.DateTime)]
        public DateTime DtAnaliseRaioX { get; set; } 

        [Required]        
        [ForeignKey("RaioX")]
        [Column("id_raio_x")]
        //[MaxLength(20)]
        public int IdRaioX { get; set; }
        public RaioX? RaioX { get; set; }
    }
}