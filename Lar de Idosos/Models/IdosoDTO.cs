namespace Lar_de_Idosos.Models {
    public class IdosoDTO {

        public string Nome { get; set; }

        public int Idade { get; set; }
        public int IdGuardiao { get; set; }

        public IFormFile? Foto { get; set; }

    }
}
