using Microsoft.EntityFrameworkCore;
using TecnicoHelp.Models;
using TecnoHelp.Models; // Precisamos "enxergar" a pasta Models

namespace TecnoHelp.Data
{
    // Nossa classe precisa herdar da classe DbContext do Entity Framework
    public class ApplicationDbContext : DbContext
    {
        // Este construtor é necessário para a configuração que faremos mais tarde
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Cada DbSet representa uma tabela do nosso banco de dados.
        // O nome da propriedade (ex: Usuarios) será usado para acessar a tabela no código C#.
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Chamado> Chamados { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Prioridade> Prioridades { get; set; }
        public DbSet<Tecnico> Tecnicos { get; set; }

        // O Entity Framework usa o nome da propriedade (plural) para se referir à tabela.
        // Por exemplo, a propriedade "Usuarios" vai se conectar à tabela "Usuario"
        // (o EF é inteligente o suficiente para singularizar o nome).
    }
}