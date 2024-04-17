using System.ComponentModel.DataAnnotations.Schema;

namespace Lar_de_Idosos.Models {
    public class Idoso {

        public Idoso() {
            ListaTrabalhadores = new HashSet<Trabalhador>();
        }

        public int Id { get; set; }

        public string Nome { get; set; }

        public int Idade { get; set; }




        /* ****************************************
        * Construção dos Relacionamentos
        * *************************************** */

        [ForeignKey(nameof(Guardiao))]
        public int GuardiaoFK { get; set; } // FK para o Guardiao
        public Guardiao Guardiao { get; set; } // FK para o Guardiao


        // relacionamento N-M, com atributos no relacionamento
        public ICollection<Trabalhador> ListaTrabalhadores { get; set; }
    }
}
