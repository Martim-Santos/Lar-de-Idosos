using System.ComponentModel.DataAnnotations.Schema;

namespace Lar_de_Idosos.Models {
    public class Idoso {

        public Idoso() {
            ListaConsultas = new HashSet<Consulta>();
            ListaTrabalhadores = new HashSet<Trabalhador>();
        }

        public int Id { get; set; }

        public string Nome { get; set; }

        public int Idade { get; set; }

        public string? Foto { get; set; }


        /* ****************************************
        * Construção dos Relacionamentos
        * *************************************** */

        [ForeignKey(nameof(Guardiao))]
        public int GuardiaoFK { get; set; } // FK para o Guardiao
        public Guardiao Guardiao { get; set; } // FK para o Guardiao



        // lista das Consultas 'Pertencentes' a um Idoso
        public ICollection<Consulta> ListaConsultas { get; set; }

        // relacionamento N-M, com atributos no relacionamento
        public ICollection<Trabalhador> ListaTrabalhadores { get; set; }

    }
}
