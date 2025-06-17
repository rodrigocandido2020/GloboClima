using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GloboClima.Dominio.Models.PaisResponse
{
    public class PaisResponse
    {
        public Name Name { get; set; }
        public List<string> Capital { get; set; }
        public string Region { get; set; }
        public string Subregion { get; set; }
        public long Population { get; set; }
        public Flags Flags { get; set; }
    }
}
