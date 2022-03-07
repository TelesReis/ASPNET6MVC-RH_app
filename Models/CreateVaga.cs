using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
    public class CreateVaga
    {
        
        public Vaga vaga { get; set; }
        public TecVagaNome vagaTec { get; set; }

        public List<TecVagaNome> vagasTec { get; set; }

        public List<Tecnologia> tecs { get; set; }

        

    }
}