using System.ComponentModel.DataAnnotations.Schema;

namespace Lar_de_Idosos.Models {
    public class Guardiao {

        public Guardiao() {
            ListaIdosos = new HashSet<Idoso>();
        }

        public int Id { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string NumTelemovel { get; set; }




        /* ****************************************
        * Construção dos Relacionamentos
        * *************************************** */

        // lista dos Idosos 'Pertencentes' a um Guardião
        public ICollection<Idoso> ListaIdosos { get; set; }
    }
}
