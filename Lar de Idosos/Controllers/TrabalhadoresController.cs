using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lar_de_Idosos.Data;
using Lar_de_Idosos.Models;
using Microsoft.AspNetCore.Hosting;

namespace Lar_de_Idosos.Controllers
{
    public class TrabalhadoresController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IWebHostEnvironment _webHostEnvironment;


        public TrabalhadoresController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Trabalhadores
        public async Task<IActionResult> Index()
        {
            return View(await _context.Trabalhador.ToListAsync());
        }

        // GET: Trabalhadores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trabalhador = await _context.Trabalhador
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trabalhador == null)
            {
                return NotFound();
            }

            return View(trabalhador);
        }

        // GET: Trabalhadores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Trabalhadores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Idade,Email,NumTelemovel,Descricao,Medico,Tipo")] Trabalhador trabalhador, IFormFile ImagemFoto)
        {
            if (ModelState.IsValid)
            {
                string nomeImagem = "";
                bool haImagem = false;

                if (ImagemFoto == null)
                {
                    ModelState.AddModelError(string.Empty, "Deve fornecer uma Foto");
                    return View(trabalhador);
                }
                else
                {
                    if (!(ImagemFoto.ContentType == "image/png"
                        || ImagemFoto.ContentType == "image/jpeg"))
                    {
                        trabalhador.Foto = "FotoTrabalhador.png";
                    }
                    else
                    {
                        haImagem = true;
                        Guid g = Guid.NewGuid();
                        nomeImagem = g.ToString();
                        string extensaoImagem = Path.GetExtension(ImagemFoto.ContentType);
                        nomeImagem += extensaoImagem;

                        trabalhador.Foto = nomeImagem;
                    }
                }

                // adiciona à BD os dados vindo da view
                _context.Add(trabalhador);
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

                // redireciona o utilizador para a página de 'início' dos trabalhadores.
                return RedirectToAction(nameof(Index));
            };
            return View(trabalhador);
        }

        // GET: Trabalhadores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trabalhador = await _context.Trabalhador.FindAsync(id);
            if (trabalhador == null)
            {
                return NotFound();
            }
            return View(trabalhador);
        }

        // POST: Trabalhadores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Idade,Email,NumTelemovel,Descricao,Medico,Tipo,Foto")] Trabalhador trabalhador)
        {
            if (id != trabalhador.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trabalhador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrabalhadorExists(trabalhador.Id))
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
            return View(trabalhador);
        }

        // GET: Trabalhadores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trabalhador = await _context.Trabalhador
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trabalhador == null)
            {
                return NotFound();
            }

            return View(trabalhador);
        }

        // POST: Trabalhadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trabalhador = await _context.Trabalhador.FindAsync(id);
            if (trabalhador != null)
            {
                _context.Trabalhador.Remove(trabalhador);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrabalhadorExists(int id)
        {
            return _context.Trabalhador.Any(e => e.Id == id);
        }
    }
}
