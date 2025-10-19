using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using TecnicoHelp.Models;
using TecnoHelp.Data;
using TecnoHelp.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. Adiciona serviços ao contêiner.

// Configuração do DbContext para usar o banco de dados em memória
builder.Services.AddDbContext<ApplicationDbContext>(options => 
       options.UseInMemoryDatabase("TecnoHelpDb") // Usa o banco de dados em memória
                                               //options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")) // Deixe esta linha comentada para portabilidade
);

// Configuração da Autenticação por Cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Página para redirecionar se o usuário não estiver logado
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Tempo de validade do login
        options.AccessDeniedPath = "/Home/AccessDenied"; // Página para acesso negado
    });


builder.Services.AddControllersWithViews();

var app = builder.Build();


// 2. Bloco para popular o banco de dados em memória (Data Seeding)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    // Garante que o banco de dados foi criado
    context.Database.EnsureCreated();

    // Verifica se já existem usuários para não adicionar dados duplicados
    if (!context.Usuarios.Any())
    {
        // Adiciona Status
        var statusAberto = new Status { Nome = "Aberto" };
        var statusAndamento = new Status { Nome = "Em andamento" };
        var statusResolvido = new Status { Nome = "Resolvido" };
        context.Status.AddRange(statusAberto, statusAndamento, statusResolvido);

        // Adiciona Categorias
        var catHardware = new Categoria { Nome = "Hardware" };
        var catSoftware = new Categoria { Nome = "Software" };
        var catRede = new Categoria { Nome = "Rede" };
        context.Categorias.AddRange(catHardware, catSoftware, catRede);

        // Adiciona Prioridades
        var prioridadeBaixa = new Prioridade { Nivel = "Baixa", Peso = 1 };
        var prioridadeMedia = new Prioridade { Nivel = "Média", Peso = 2 };
        var prioridadeAlta = new Prioridade { Nivel = "Alta", Peso = 3 };
        context.Prioridades.AddRange(prioridadeBaixa, prioridadeMedia, prioridadeAlta);

        context.SaveChanges(); // Salva para gerar os IDs

        // Adiciona Usuários
        var adminUser = new Usuario { Nome = "Administrador", Email = "admin@email.com", Senha = "senha123", TipoUsuario = "admin" };
        var tecnicoUser = new Usuario { Nome = "Carlos Dias", Email = "carlos.dias@email.com", Senha = "senha123", TipoUsuario = "técnico" };
        var colabUser = new Usuario { Nome = "Ana Clara", Email = "ana.clara@email.com", Senha = "senha123", TipoUsuario = "colaborador" };
        context.Usuarios.AddRange(adminUser, tecnicoUser, colabUser);

        context.SaveChanges(); // Salva para gerar os IDs dos usuários

        // Adiciona o Técnico
        var tecnico = new Tecnico { UsuarioId = tecnicoUser.Id, Especialidade = "Redes" };
        context.Tecnicos.Add(tecnico);

        context.SaveChanges(); // Salva para gerar o ID do técnico

        // Adiciona Chamados
        context.Chamados.AddRange(
            new Chamado { Titulo = "PC não liga", Descricao = "Computador da recepção não dá sinal de vida.", UsuarioId = colabUser.Id, CategoriaId = catHardware.Id, StatusId = statusAberto.Id, PrioridadeId = prioridadeAlta.Id },
            new Chamado { Titulo = "Internet lenta", Descricao = "A internet do setor financeiro está muito lenta.", UsuarioId = colabUser.Id, CategoriaId = catRede.Id, StatusId = statusAndamento.Id, PrioridadeId = prioridadeMedia.Id, TecnicoId = tecnico.Id }
        );

        context.SaveChanges();
    }
}
// Fim do bloco de popular o banco


// 3. Configura o pipeline de requisições HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Habilita a autenticação e autorização
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();