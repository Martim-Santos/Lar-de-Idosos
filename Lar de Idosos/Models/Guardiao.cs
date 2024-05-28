using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lar_de_Idosos.Models {
    public class Guardiao {

        public Guardiao() {
            ListaIdosos = new HashSet<Idoso>();
        }

        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Nome do Guardião
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// email do Guardião
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// nº de telemovel do Guardião
        /// </summary>
        [Display(Name = "Número de Telefone")]
        [Required(ErrorMessage = "A {0} é de preenchimento obrigatório.")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "Deve escrever {1} dígitos no número {0}.")]
        [RegularExpression("9[1236][0-9]{7}")]
        public string NumTelemovel { get; set; }




        /* ****************************************
        * Construção dos Relacionamentos
        * *************************************** */

        // lista dos Idosos 'Pertencentes' a um Guardião
        public ICollection<Idoso> ListaIdosos { get; set; }
    }
}
