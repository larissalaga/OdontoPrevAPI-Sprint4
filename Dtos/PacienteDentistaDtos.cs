using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using OdontoPrevAPI.Models;

namespace OdontoPrevAPI.Dtos
{
    public class PacienteDentistaDtos
    {
        [Key]
        [Required]
        [Column("id_paciente")]
        [ForeignKey("Paciente")]
        //[MaxLength(20)]
        public int IdPaciente { get; set; }
        public Paciente? Paciente { get; set; }

        [Key]
        [Required]
        [Column("id_dentista")]
        [ForeignKey("Dentista")]
        //[MaxLength(20)]
        public int IdDentista { get; set; }
        public Dentista? Dentista { get; set; }
    }
}
