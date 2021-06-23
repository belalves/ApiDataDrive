using System.ComponentModel.DataAnnotations;

namespace Shop.Models{
    public class Categoria
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Esse campo é obrigatório")]
        [MaxLength(60, ErrorMessage ="Este campo deve conter entre 3 e 60 caracteres")]
        [MinLength(3, ErrorMessage ="Este campo deve conter entre 3 e 60 caracteres")]
        public string Titulo { get; set; }
    }
}
