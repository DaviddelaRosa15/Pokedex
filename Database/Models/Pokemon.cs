using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class Pokemon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UrlImage { get; set; }

        public int RegionId { get; set; }
        public Region Region { get; set; }

        public int FirstTypeId { get; set; }
        public Tipo FirstType { get; set; }

        public System.Nullable<int> SecondTypeId { get; set; }
        public Tipo SecondType { get; set; }
    }
}
