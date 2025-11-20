using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Para acessar o banco
using System.Security.Claims; // Para criar o "crachá" do usuário
using Microsoft.AspNetCore.Authentication; // Para realizar o login
using Microsoft.AspNetCore.Authentication.Cookies; // Para usar o esquema de cookies
using TecnoHelp.Data; // Para acessar o DbContext
using TecnoHelp.Models; // Para acessar os ViewModels

namespace TecnoHelp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Injeta o DbContext para que possamos usar o banco de dados
        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Procura um usuário no banco com o email fornecido
                var usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.Email == model.Email);

                // ATENÇÃO: Verificação de senha em texto plano.
                // Isso é apenas para fins de aprendizado. Em um sistema real,
                // a senha deve ser criptografada (hashed).
                if (usuario != null && usuario.Senha == model.Senha)
                {
                    // Se o usuário existe e a senha está correta, criamos o "crachá" (Claims)
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                        new Claim(ClaimTypes.Name, usuario.Email),
                        new Claim("FullName", usuario.Nome),
                        new Claim(ClaimTypes.Role, usuario.TipoUsuario),
                    };

                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    // Realiza o login, criando o cookie de autenticação
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity));

                    // Redirecionamento inteligente baseado no perfil
                    if (usuario.TipoUsuario == "admin")
                    {
                        return RedirectToAction("Index", "Dashboard");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Chamados");
                    }
                }

                // Se o login falhar, adiciona uma mensagem de erro e retorna para a tela de login
                ModelState.AddModelError(string.Empty, "Login inválido. Verifique seu e-mail e senha.");
            }
            return View(model);
        }

        // POST: /Account/Logout
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}