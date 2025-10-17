using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TecnoHelp.Models
{
    [Table("Prioridade")]
    public class Prioridade
    {
        [Key]
        [Column("id_prioridade")]
        public int Id { get; set; }

        [Required]
        [Column("nivel")]
        public string Nivel { get; set; }

        [Required]
        [Column("peso")]
        public int Peso { get; set; }
    }
}