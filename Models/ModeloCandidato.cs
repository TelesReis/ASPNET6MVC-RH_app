using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
    public class ModeloCandidato
    {
       
             
        public List<int> tecSelecionadas { get; set; }

        public Candidato candidato { get; set; }


    }
}