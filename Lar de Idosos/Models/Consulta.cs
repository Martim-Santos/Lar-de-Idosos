using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Lar_de_Idosos.Models {
    public class Consulta {

        public int Id { get; set; }

        [Required(ErrorMessage = "A {0} é de preenchimento obrigatório.")]
        public string Descricao { get; set; }


        /* ****************************************
        * Construção dos Relacionamentos
        * *************************************** */

        [ForeignKey(nameof(Idoso))]
        public int IdosoFK { get; set; } // FK para o Idoso
        public Idoso? Idoso { get; set; } // FK para o Idoso


        [ForeignKey(nameof(Trabalhador))]
        public int TrabalhadorFK { get; set; } // FK para o Trabalhador
        public Trabalhador? Trabalhador { get; set; } // FK para o Trabalhador

    }
}
