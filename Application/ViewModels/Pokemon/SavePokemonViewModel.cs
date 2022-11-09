using Application.ViewModels.Region;
using Application.ViewModels.Tipo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Pokemon
{
    public class SavePokemonViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe colocar el nombre del pokemon")]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required(ErrorMessage = "Debe colocar una imagen del pokemon")]
        public string Url { get; set; }

        [Range(1, int.MaxValue,ErrorMessage = "Debe colocar la region a la que pertenece el pokemon")]
        public int RegionId { get; set; }

        [Range(1,int.MaxValue,ErrorMessage = "Debe colocar el tipo primario del pokemon")]
        public int FirstTypeId { get; set; }
        public int? SecondTypeId { get; set; }

        public List<RegionViewModel> Regions { get; set; }
        public List<TipoViewModel> Types { get; set; }

    }
}
