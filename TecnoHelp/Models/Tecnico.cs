using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TecnoHelp.Models;

namespace TecnicoHelp.Models
{
    [Table("Tecnico")]
    public class Tecnico
    {
        [Key]
        [Column("id_tecnico")]
        public int Id { get; set; }

        [Column("id_usuario")]
        public int UsuarioId { get; set; }
        [ForeignKey("UsuarioId")]
        public virtual Usuario Usuario { get; set; }

        [Column("especialidade")]
        public string? Especialidade { get; set; }

        [Column("disponivel")]
        public bool Disponivel { get; set; } = true;
    }
}