using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TecnoHelp.Data;
using TecnoHelp.Models;

namespace TecnoHelp.Controllers
{
    [Authorize(Roles = "admin")]
    public class UsuariosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsuariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Função auxiliar para criar a lista de perfis de usuário e evitar repetição de código
        private SelectList GetTiposUsuarioSelectList(string selectedValue = null)
        {
            var tipos = new List<SelectListItem>
            {
                new SelectListItem { Value = "colaborador", Text = "Colaborador" },
                new SelectListItem { Value = "técnico", Text = "Técnico" },
                new SelectListItem { Value = "admin", Text = "Administrador" }
            };
            return new SelectList(tipos, "Value", "Text", selectedValue);
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            return View(await _context.Usuarios.ToListAsync());
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            ViewData["Title"] = "Criar Novo Usuário"; // Traduzido
            ViewBag.TiposUsuario = GetTiposUsuarioSelectList();
            return View();
        }

        // POST: Usuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Email,Senha,TipoUsuario,Ativo")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.DataCadastro = DateTime.Now; // Define a data de cadastro no servidor
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // Se a validação falhar, recarrega a lista de perfis antes de mostrar a página novamente
            ViewBag.TiposUsuario = GetTiposUsuarioSelectList(usuario.TipoUsuario);
            return View(usuario);
        }

        // GET: Usuarios/Edit/5 (CORRIGIDO)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            // Envia a lista de perfis para a página de edição, com o valor atual pré-selecionado
            ViewBag.TiposUsuario = GetTiposUsuarioSelectList(usuario.TipoUsuario);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Email,Senha,TipoUsuario,DataCadastro,Ativo")] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            // Se a validação falhar, recarrega a lista de perfis
            ViewBag.TiposUsuario = GetTiposUsuarioSelectList(usuario.TipoUsuario);
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }
    }
}