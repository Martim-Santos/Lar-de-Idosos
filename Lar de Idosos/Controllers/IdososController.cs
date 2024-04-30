using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lar_de_Idosos.Data;
using Lar_de_Idosos.Models;

namespace Lar_de_Idosos.Controllers
{
    public class IdososController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public IdososController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Idosos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Idoso.Include(i => i.Guardiao);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Idosos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var idoso = await _context.Idoso
                .Include(i => i.Guardiao)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (idoso == null)
            {
                return NotFound();
            }

            return View(idoso);
        }

        // GET: Idosos/Create
        public IActionResult Create()
        {
            ViewData["GuardiaoFK"] = new SelectList(_context.Guardiao, "Id", "Id");
            return View();
        }

        // POST: Idosos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Idade,Foto,GuardiaoFK")] Idoso idoso, IFormFile ImagemFoto)
        {
            if (ModelState.IsValid)
            {
                string nomeImagem = "";
                bool haImagem = false;

                if (ImagemFoto == null)
                {
                    ModelState.AddModelError(string.Empty, "Deve fornecer uma Foto");
                    return View(idoso);
                }
                else
                {
                    if (!(ImagemFoto.ContentType == "image/png"
                        || ImagemFoto.ContentType == "image/jpeg"))
                    {
                        idoso.Foto = "FotoIdoso.png";
                    }
                    else
                    {
                        haImagem = true;
                        Guid g = Guid.NewGuid();
                        nomeImagem = g.ToString();
                        string extensaoImagem = Path.GetExtension(ImagemFoto.ContentType);
                        nomeImagem += extensaoImagem;

                        idoso.Foto = nomeImagem;
                    }
                }

                // adiciona à BD os dados vindo da view
                _context.Add(idoso);
                // Commit
                await _context.SaveChangesAsync();

                // guardar a imagem da foto
                if (haImagem)
                {
                    //encolher a imagem ao tamanho certo.
                    //determinar o local de armazenamento da imagem
                    string localizacaoImagem = _webHostEnvironment.WebRootPath;
                    localizacaoImagem = Path.Combine(localizacaoImagem, "Imagens");
                    // será que o local existe?
                    if (!Directory.Exists(localizacaoImagem))
                    {
                        Directory.CreateDirectory(localizacaoImagem);
                    }

                    localizacaoImagem = Path.Combine(localizacaoImagem, "Imagens");
                    // guardar a imagem no disco rigido
                    using var stream = new FileStream(
                        localizacaoImagem, FileMode.Create);
                    await ImagemFoto.CopyToAsync(stream);

                }

                // redireciona o utilizador para a página de 'início' dos idosos.
                return RedirectToAction(nameof(Index));
            };
            ViewData["GuardiaoFK"] = new SelectList(_context.Guardiao, "Id", "Id", idoso.GuardiaoFK);
            return View(idoso);
        }

        // GET: Idosos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var idoso = await _context.Idoso.FindAsync(id);
            if (idoso == null)
            {
                return NotFound();
            }
            ViewData["GuardiaoFK"] = new SelectList(_context.Guardiao, "Id", "Id", idoso.GuardiaoFK);
            return View(idoso);
        }

        // POST: Idosos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Nome,Idade,Foto,GuardiaoFK")] Idoso idoso)
        {
            if (id != idoso.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(idoso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IdosoExists(idoso.Id))
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
            ViewData["GuardiaoFK"] = new SelectList(_context.Guardiao, "Id", "Id", idoso.GuardiaoFK);
            return View(idoso);
        }

        // GET: Idosos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var idoso = await _context.Idoso
                .Include(i => i.Guardiao)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (idoso == null)
            {
                return NotFound();
            }

            return View(idoso);
        }

        // POST: Idosos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var idoso = await _context.Idoso.FindAsync(id);
            if (idoso != null)
            {
                _context.Idoso.Remove(idoso);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IdosoExists(int id)
        {
            return _context.Idoso.Any(e => e.Id == id);
        }
    }
}
