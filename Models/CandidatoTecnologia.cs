using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
    public class CandidatoTecnologia
    {
        [Key]
        public int candidato_tecnologia_id { get; set; }
        public int candidato_id { get; set; }
        public int tecnologia_id { get; set; }
    
    }
}