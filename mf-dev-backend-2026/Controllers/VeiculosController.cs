using mf_dev_backend_2026.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace mf_dev_backend_2026.Controllers
{
    public class VeiculosController : Controller
    {
        //para fazer a leitura do banco de dados
        private readonly AppDbContext _context;

        public VeiculosController(AppDbContext context)
        {
            _context = context;
        }

        //requisicoes do usuario
        public async Task<IActionResult> Index()
        {
            var dados = await _context.Veiculos.ToListAsync();
            return View(dados);
        }

        //para criar o veiculo
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Veiculo veiculo)
        {
            if (ModelState.IsValid)
            {
                _context.Veiculos.Add(veiculo);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(veiculo);
        }

        //para editar veiculo (GET)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var dados = await _context.Veiculos.FindAsync(id);

            if (dados == null)
                return NotFound();

            return View(dados);
        }

        //para editar veiculo (POST)
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Veiculo veiculo)
        {
            if (id != veiculo.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(veiculo);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Veiculos.Any(e => e.Id == veiculo.Id))
                        return NotFound();
                    else
                        throw;
                }
            }

            return View(veiculo);
        }
        //PARA VISUALIZAR OS DETALHES
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
                return NotFound();

            var dados =await _context.Veiculos.FindAsync(id);

            if (dados == null)
                return NotFound();


            return View(dados);
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
                return NotFound();

            var dados =await _context.Veiculos.FindAsync(id);

            if (dados == null)
                return NotFound();


            return View(dados);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id == null)
                return NotFound();

            var dados =await _context.Veiculos.FindAsync(id);

            if (dados == null)
                return NotFound();

            _context.Veiculos.Remove(dados);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
