using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Pokemon
{
    public class PokemonViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public int RegionId { get; set; }
        public string RegionName { get; set; }
        public string FirstTypeName { get; set; }
        public string SecondTypeName { get; set; }
    }
}
