using Microsoft.AspNetCore.Identity;
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
        [Required(ErrorMessage = "A {0} é de preenchimento obrigatório.")]
        public string Nome { get; set; }

        /// <summary>
        /// email do Guardião
        /// </summary>
        [Required(ErrorMessage = "A {0} é de preenchimento obrigatório.")]
        [RegularExpression("[a-z,A-Z,@]")]
        public string Email { get; set; }

        /// <summary>
        /// nº de telemovel do Guardião
        /// </summary>
        [Display(Name = "Número de Telefone")]
        [Required(ErrorMessage = "A {0} é de preenchimento obrigatório.")]
        [StringLength(9)]
        [RegularExpression("[0-9]{9}")]
        public string NumTelemovel { get; set; }



        [ForeignKey(nameof(IdentityUser))]
        public string? IdentityUserFK { get; set; } // FK para o Guardiao
        public IdentityUser? IdentityUser { get; set; }


        /* ****************************************
        * Construção dos Relacionamentos
        * *************************************** */

        // lista dos Idosos 'Pertencentes' a um Guardião
        public ICollection<Idoso> ListaIdosos { get; set; }
    }
}
