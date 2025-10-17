using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TecnoHelp.Data;
using TecnoHelp.Models;

namespace TecnoHelp.Controllers
{
    [Authorize]
    public class ChamadosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChamadosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Chamados
        public async Task<IActionResult> Index()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var userRole = User.FindFirstValue(ClaimTypes.Role);

            var chamadosQuery = _context.Chamados
                .Include(c => c.Solicitante)
                .Include(c => c.Categoria)
                .Include(c => c.Status)
                .Include(c => c.Prioridade)
                .Include(c => c.Tecnico.Usuario)
                .AsQueryable();

            if (userRole == "colaborador")
            {
                chamadosQuery = chamadosQuery.Where(c => c.UsuarioId == userId);
            }
            else if (userRole == "técnico")
            {
                var tecnicoId = await _context.Tecnicos
                                    .Where(t => t.UsuarioId == userId)
                                    .Select(t => t.Id)
                                    .FirstOrDefaultAsync();

                chamadosQuery = chamadosQuery.Where(c => c.TecnicoId == tecnicoId || c.TecnicoId == null);
            }

            var chamados = await chamadosQuery.OrderByDescending(c => c.DataAbertura).ToListAsync();
            return View(chamados);
        }

        // GET: Chamados/Details/5 (CORRETO)
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var chamado = await _context.Chamados
                .Include(c => c.Solicitante).Include(c => c.Categoria).Include(c => c.Status)
                .Include(c => c.Prioridade).Include(c => c.Tecnico.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chamado == null) return NotFound();
            return View(chamado);
        }

        // GET: Chamados/Create (CORRIGIDO E SIMPLIFICADO)
        public IActionResult Create()
        {
            ViewData["Title"] = "Abrir Novo Chamado";
            // A única lista que o usuário precisa escolher é a de Categoria
            ViewBag.CategoriaId = new SelectList(_context.Categorias, "Id", "Nome");
            return View();
        }

        // POST: Chamados/Create (CORRIGIDO E SIMPLIFICADO)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Titulo,Descricao,CategoriaId")] Chamado chamado)
        {
            // Pega o ID do usuário logado
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            // Define os valores padrão no servidor, ignorando o que vem do formulário
            chamado.UsuarioId = userId;
            chamado.DataAbertura = DateTime.Now;
            chamado.StatusId = 1; // ID 1 = "Aberto"
            chamado.PrioridadeId = 2; // ID 2 = "Média"

            // Remove a validação de campos que não estão no formulário para evitar erros
            ModelState.Remove("Solicitante");
            ModelState.Remove("Categoria");
            ModelState.Remove("Status");
            ModelState.Remove("Prioridade");

            if (ModelState.IsValid)
            {
                _context.Add(chamado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Se a validação falhar, recarrega apenas o dropdown necessário
            ViewBag.CategoriaId = new SelectList(_context.Categorias, "Id", "Nome", chamado.CategoriaId);
            return View(chamado);
        }

        // GET: Chamados/Edit/5 (CORRETO)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var chamado = await _context.Chamados.FindAsync(id);
            if (chamado == null) return NotFound();

            ViewBag.UsuarioId = new SelectList(_context.Usuarios, "Id", "Nome", chamado.UsuarioId);
            ViewBag.CategoriaId = new SelectList(_context.Categorias, "Id", "Nome", chamado.CategoriaId);
            ViewBag.StatusId = new SelectList(_context.Status, "Id", "Nome", chamado.StatusId);
            ViewBag.PrioridadeId = new SelectList(_context.Prioridades, "Id", "Nivel", chamado.PrioridadeId);
            ViewBag.TecnicoId = new SelectList(_context.Tecnicos.Include(t => t.Usuario), "Id", "Usuario.Nome", chamado.TecnicoId);

            return View(chamado);
        }

        // POST: Chamados/Edit/5 (CORRETO)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Descricao,DataAbertura,DataFechamento,UsuarioId,TecnicoId,CategoriaId,StatusId,PrioridadeId")] Chamado chamado)
        {
            if (id != chamado.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chamado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChamadoExists(chamado.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            // Recarrega todos os dropdowns em caso de falha na validação
            ViewBag.UsuarioId = new SelectList(_context.Usuarios, "Id", "Nome", chamado.UsuarioId);
            ViewBag.CategoriaId = new SelectList(_context.Categorias, "Id", "Nome", chamado.CategoriaId);
            ViewBag.StatusId = new SelectList(_context.Status, "Id", "Nome", chamado.StatusId);
            ViewBag.PrioridadeId = new SelectList(_context.Prioridades, "Id", "Nivel", chamado.PrioridadeId);
            ViewBag.TecnicoId = new SelectList(_context.Tecnicos.Include(t => t.Usuario), "Id", "Usuario.Nome", chamado.TecnicoId);
            return View(chamado);
        }

        // GET: Chamados/Delete/5 e outros métodos...
        // (O resto do seu código para Delete e ChamadoExists está correto)
        #region Delete Methods
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var chamado = await _context.Chamados
                .Include(c => c.Solicitante).Include(c => c.Categoria).Include(c => c.Status)
                .Include(c => c.Prioridade).Include(c => c.Tecnico.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chamado == null) return NotFound();
            return View(chamado);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chamado = await _context.Chamados.FindAsync(id);
            if (chamado != null) _context.Chamados.Remove(chamado);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool ChamadoExists(int id)
        {
            return _context.Chamados.Any(e => e.Id == id);
        }
        #endregion
    }
}