using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdontoPrevAPI.Models
{
    [Table("T_OPBD_PACIENTE_DENTISTA")]
    [PrimaryKey("IdPaciente", "IdDentista")]
    public class PacienteDentista
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
