using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
    public class VagaTecnologia
    {
        [Key]
        public int vaga_tecnologia_id { get; set; }
        public float peso { get; set;}
        public int vaga_id { get; set; }
        public int tecnologia_id { get; set; }
    
    }
}