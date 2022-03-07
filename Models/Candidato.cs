using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
    public class Candidato
    {
        [Key]
        public int candidato_id { get; set; }
        [Display(Name = "Candidato")]
        public string nome { get; set; }
             
        public int vaga_id { get; set; }

        //public Vaga vaga { get; set; }

    }
}