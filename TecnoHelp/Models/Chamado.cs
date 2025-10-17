using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TecnicoHelp.Models;

namespace TecnoHelp.Models
{
    [Table("Chamado")]
    public class Chamado
    {
        [Key]
        [Column("id_chamado")]
        public int Id { get; set; }

        [Required]
        [Column("titulo")]
        public string Titulo { get; set; }

        [Required]
        [Column("descricao")]
        public string Descricao { get; set; }

        [Column("data_abertura")]
        public DateTime DataAbertura { get; set; } = DateTime.Now;

        [Column("data_fechamento")]
        public DateTime? DataFechamento { get; set; }

        [Column("id_usuario")]
        public int UsuarioId { get; set; }
        [ForeignKey("UsuarioId")]
        public virtual Usuario Solicitante { get; set; }

        [Column("id_tecnico")]
        public int? TecnicoId { get; set; }
        [ForeignKey("TecnicoId")]
        public virtual Tecnico? Tecnico { get; set; }

        [Column("id_categoria")]
        public int CategoriaId { get; set; }
        [ForeignKey("CategoriaId")]
        public virtual Categoria Categoria { get; set; }

        [Column("id_status")]
        public int StatusId { get; set; }
        [ForeignKey("StatusId")]
        public virtual Status Status { get; set; }

        [Column("id_prioridade")]
        public int PrioridadeId { get; set; }
        [ForeignKey("PrioridadeId")]
        public virtual Prioridade Prioridade { get; set; }
    }
}