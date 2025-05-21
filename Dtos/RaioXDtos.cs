using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using OdontoPrevAPI.Models;
using System.Drawing;
using System.Diagnostics.CodeAnalysis;

namespace OdontoPrevAPI.Dtos
{
    public class RaioXDtos
    {
        [Required]
        [MaxLength(200)]
        public string DsRaioX { get; set; } = string.Empty;

        [AllowNull]
        public byte[]? ImRaioX { get; set; } 

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DtDataRaioX { get; set; }

        [Required]
        [Column("id_paciente")]
        [ForeignKey("Paciente")]
        //[MaxLength(20)]
        public int IdPaciente { get; set; }
        public Paciente? Paciente { get; set; }
    }
}
