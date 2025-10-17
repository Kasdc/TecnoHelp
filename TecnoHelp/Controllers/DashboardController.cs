using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TecnoHelp.Data;

namespace TecnoHelp.Controllers
{
    [Authorize(Roles = "admin")] // Apenas administradores podem acessar
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Contar o total de chamados
            ViewBag.TotalChamados = await _context.Chamados.CountAsync();

            // Contar chamados abertos (Status ID = 1)
            ViewBag.ChamadosAbertos = await _context.Chamados.CountAsync(c => c.StatusId == 1);

            // Contar chamados em andamento (Status ID = 2)
            ViewBag.ChamadosEmAndamento = await _context.Chamados.CountAsync(c => c.StatusId == 2);

            // Contar chamados resolvidos (Status ID = 3)
            ViewBag.ChamadosResolvidos = await _context.Chamados.CountAsync(c => c.StatusId == 3);

            return View();
        }
    }
}