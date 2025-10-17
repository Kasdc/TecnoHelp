using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TecnoHelp.Models
{
    [Table("Usuario")] // Mapeia a classe para a tabela "Usuario"
    public class Usuario
    {
        [Key]
        [Column("id_usuario")] // Mapeia a propriedade para a coluna "id_usuario"
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome pode ter no máximo 100 caracteres.")]
        [Column("nome")] // Mapeia para a coluna "nome"
        public string Nome { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de e-mail inválido.")]
        [Column("email")] // Mapeia para a coluna "email"
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [Column("senha")] // Mapeia para a coluna "senha"
        public string Senha { get; set; }

        [Required]
        [Column("tipo_usuario")] // Mapeia para a coluna "tipo_usuario"
        public string TipoUsuario { get; set; }

        [Column("data_cadastro")] // Mapeia para a coluna "data_cadastro"
        public DateTime DataCadastro { get; set; } = DateTime.Now;

        [Column("ativo")] // Mapeia para a coluna "ativo"
        public bool Ativo { get; set; } = true;
    }
}