using Lar_de_Idosos.Data;
using Lar_de_Idosos.Migrations;
using Lar_de_Idosos.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Plugins;
using System.Linq;

namespace Lar_de_Idosos.Controllers.API {
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase {

        public ApplicationDbContext _context;

        public UserManager<IdentityUser> _userManager;

        public SignInManager<IdentityUser> _signInManager;


        /// <summary>
        /// objecto que contém os dados do Servidor
        /// </summary>
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ValuesController(ApplicationDbContext appDbContext, IWebHostEnvironment webHostEnvironment,
            UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager) {
            _context = appDbContext;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        [Route("")]
        public ActionResult Index() {
            var listaG = _context.Guardiao.ToList();


            return Ok(listaG);
        }

        [HttpGet]
        [Route("ola")]
        [Authorize]
        public ActionResult OlaNome(string nome) {
            if (nome.IsNullOrEmpty()) {
                return BadRequest();
            }

            return Ok("Olá " + nome);
        }

        /// <summary>
        /// Criar um Idoso -> o user envia o pedido para criar um idoso, o estado deste é posto em pendente.
        /// Quando um Trabalhador alterar o estádo do idoso para:
        /// aceite -> o idoso é criado e salvo na bd.
        /// rejeitado -> o idoso não é criado e os seu dados não são salvos.
        /// </summary>
        /// <param name="idoso"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateIdoso")]
        public async Task<ActionResult> CreateIdosoAsync([FromForm] IdosoDTO idosoDto) {
            string email = User.Identity.Name;

            User.IsInRole("");
            Idoso idoso = new Idoso { Nome = idosoDto.Nome, Idade = idosoDto.Idade, estado = EstadoIdoso.Pendente, GuardiaoFK = idosoDto.IdGuardiao };

            //search idoso where estado = 0, linq
            if (
            _context.Idoso.Where(i => i.estado == 0 && i.GuardiaoFK == idosoDto.IdGuardiao).Count() > 2) {
                return BadRequest("Nao pode ter mais 2 idosos em estado pendente por guardiao"); //estatus 200 OK estatus 400 BadRequest
            }

            /* Guardar a imagem no disco rígido do Servidor
          * Algoritmo
          * 1- há ficheiro?
          *    1.1 - não
          *          devolvo controlo ao browser
          *          com mensagem de erro
          *    1.2 - sim
          *          Será imagem (JPG,JPEG,PNG)?
          *          1.2.1 - não
          *                  uso logótipo pre-definido
          *          1.2.2 - sim
          *                  - determinar o nome da imagem
          *                  - guardar esse nome na BD
          *                  - guardar o ficheir no disco rígido
          */

            // vars auxiliares
            string nomeImagem = "";
            bool haImagem = false;


            // há ficheiro, mas é uma imagem?
            if (!(idosoDto.Foto.ContentType == "image/png" ||
                    idosoDto.Foto.ContentType == "image/jpeg"
                )) {
                // não
                // vamos usar uma imagem pre-definida
                idoso.Foto = "logoCurso.jpg";
            } else {
                // há imagem
                haImagem = true;
                // gerar nome imagem
                Guid g = Guid.NewGuid();
                nomeImagem = g.ToString();
                string extensaoImagem = Path.GetExtension(idosoDto.Foto.FileName).ToLowerInvariant();
                nomeImagem += extensaoImagem;
                // guardar o nome do ficheiro na BD
                idoso.Foto = nomeImagem;

            }


            _context.Idoso.Add(idoso);

            // guardar a imagem do logótipo
            if (haImagem) {
                // encolher a imagem ao tamanho certo --> fazer pelos alunos
                // procurar no NuGet

                // determinar o local de armazenamento da imagem
                string localizacaoImagem = _webHostEnvironment.WebRootPath;
                // adicionar à raiz da parte web, o nome da pasta onde queremos guardar as imagens
                localizacaoImagem = Path.Combine(localizacaoImagem, "Imagens");

                // será que o local existe?
                if (!Directory.Exists(localizacaoImagem)) {
                    Directory.CreateDirectory(localizacaoImagem);
                }

                // atribuir ao caminho o nome da imagem
                localizacaoImagem = Path.Combine(localizacaoImagem, nomeImagem);

                // guardar a imagem no Disco Rígido
                using var stream = new FileStream(
                   localizacaoImagem, FileMode.Create
                   );
                await idosoDto.Foto.CopyToAsync(stream);
            }

            _context.SaveChanges();
            return Ok();
        }

        /// <summary>
        /// registar um utilizador/Guardião
        /// </summary>
        /// <param name="guardiao"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Registar")]
        public async Task<ActionResult> RegistarAsync([FromBody] GuardiaoDTO guardiaoDTO) {
            _userManager.FindByEmailAsync(guardiaoDTO.Email);

            if (await _userManager.FindByEmailAsync(guardiaoDTO.Email) != null) {
                return BadRequest("Este utilizador já existe, use um email diferente!");
            }

            IdentityUser user = new IdentityUser();
            user.Email = guardiaoDTO.Email;
            user.NormalizedEmail = guardiaoDTO.Email.ToUpper();

            user.UserName = guardiaoDTO.Email;
            user.NormalizedUserName = guardiaoDTO.Email.ToUpper();

            user.EmailConfirmed = true;

            user.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, guardiaoDTO.Password);


            await _userManager.CreateAsync(user);


            // TODO: CRIAR O GUARDIAO COM FK DO IDENTITYUSER -> GUARDIAO.IDENTITYUSERFK = USER.ID


            return Ok();
        }

        /// <summary>
        /// login a um utilizador/Guardião
        /// </summary>
        /// <param name="guardiao"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> LoginAsync([FromBody] UserDTO userDTO) {
            IdentityUser user = await _userManager.FindByEmailAsync(userDTO.Email);

            if (user == null) {
                return BadRequest("este utilizador não existe!");
            }

            PasswordVerificationResult result = new PasswordHasher<IdentityUser>().VerifyHashedPassword(null, user.PasswordHash, userDTO.Password);
            if (result == PasswordVerificationResult.Failed)
                return BadRequest("PASSWORD errada!");

            await _signInManager.SignInAsync(user, false);

            return Ok();
        }

        [HttpGet]
        [Route("Logout")]
        [Authorize]
        public async Task<ActionResult> Logout() {

            await _signInManager.SignOutAsync();
            return Ok("Olá");
        }
    }
}
