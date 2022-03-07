using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
    public class Tecnologia
    {
        [Key]
        public int tecnologia_id { get; set; }
        [Display(Name = "Tecnologias")]
        public string nome { get; set; }

    }
}