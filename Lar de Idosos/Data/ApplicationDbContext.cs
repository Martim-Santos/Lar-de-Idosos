using Lar_de_Idosos.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lar_de_Idosos.Data {


    public class ApplicationDbContext : IdentityDbContext {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {
        }

        

        /* ********************************************
         * defenir as 'tabelas' da base de dados
         * ******************************************** */

        public DbSet<Idoso> Idoso { get; set; }

        public DbSet<Guardiao> Guardiao { get; set; }

        public DbSet<Trabalhador> Trabalhador { get; set; }

        public DbSet<Consulta> Consulta { get; set; }

    }
}
