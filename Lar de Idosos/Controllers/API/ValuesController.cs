using Lar_de_Idosos.Data;
using Lar_de_Idosos.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Lar_de_Idosos.Controllers.API {
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase {

        public ApplicationDbContext _context;

        public ValuesController(ApplicationDbContext appDbContext) {
            _context = appDbContext;
        }

        [HttpGet]
        [Route("")]
        public ActionResult Index() {
            var lista = _context.Idoso.ToList();
            return Ok(lista);
        }

        [HttpGet]
        [Route("ola")]
        public ActionResult OlaNome(string nome) {
            if (nome.IsNullOrEmpty()) {
                return BadRequest();
            }

            return Ok("Olá " + nome);
        }

        [HttpPost]
        [Route("CreateIdoso")]
        public ActionResult CreateCurso([FromForm] ) {
            User.IsInRole("");

            _context.Idoso.Add(new Idoso { Nome = "batata" });
            _context.SaveChanges();
            return Ok();
        }
    }
}
