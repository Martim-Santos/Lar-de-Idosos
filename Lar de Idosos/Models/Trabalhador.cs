namespace Lar_de_Idosos.Models {
    public class Trabalhador {

        public Trabalhador() {
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

        /* ****************************************
        * Construção dos Relacionamentos
        * *************************************** */

        // relacionamento N-M, com atributos no relacionamento
        public ICollection<Idoso> ListaIdosos { get; set; }
    }
}
