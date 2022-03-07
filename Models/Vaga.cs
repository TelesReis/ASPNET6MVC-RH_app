using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
    public class Vaga
    {
        [Key]
        public int vaga_id { get; set; }
        [Display(Name = "Vagas")]
        public string nome { get; set; }

    }
}