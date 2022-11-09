using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class Tipo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Pokemon> PokemonsFirst { get; set; }
        public ICollection<Pokemon> PokemonsSecond { get; set; }
    }
}
