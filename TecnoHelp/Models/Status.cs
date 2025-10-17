using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TecnoHelp.Models
{
    [Table("Status")]
    public class Status
    {
        [Key]
        [Column("id_status")]
        public int Id { get; set; }

        [Required]
        [Column("nome")]
        public string Nome { get; set; }
    }
}