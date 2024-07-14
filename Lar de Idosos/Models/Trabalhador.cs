using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lar_de_Idosos.Models {
    public class Trabalhador {

        public Trabalhador() {
            ListaConsultas = new HashSet<Consulta>();
            ListaIdosos = new HashSet<Idoso>();
        }

        public int Id { get; set; }

        public string Nome { get; set; }

        public string Idade { get; set; }

        public string Email { get; set; }

        public string NumTelemovel { get; set; }

        public string Descricao { get; set; }

        public bool Medico { get; set; }

        public string Tipo { get; set; }

        public string? Foto { get; set; }


        [ForeignKey(nameof(IdentityUser))]
        public string? IdentityUserFK { get; set; } // FK para o Trabalhador
        public IdentityUser? IdentityUser { get; set; }

        /* ****************************************
        * Construção dos Relacionamentos
        * *************************************** */

        // lista das Consultas 'Pertencentes' a um Trabalhador
        public ICollection<Consulta> ListaConsultas { get; set; }

        
        // relacionamento N-M, com atributos no relacionamento
        public ICollection<Idoso> ListaIdosos { get; set; }
    }
}
