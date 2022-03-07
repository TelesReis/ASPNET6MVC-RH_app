using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
    public class Relatorio
    {
        public string nome { get; set; }
        public decimal peso { get; set;}
        public int vaga_id { get; set; }
        public int candidato_id { get; set; }
        public int tecnologia_id { get; set; }

    }
}