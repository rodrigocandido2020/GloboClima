using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GloboClima.Web.Shared.ViewModels
{
    public class PaisViewModel
    {
        public NameViewModel Name { get; set; }
        public List<string> Capital { get; set; }
        public string Region { get; set; }
        public string Subregion { get; set; }
        public long Population { get; set; }
        public Dictionary<string, string> Languages { get; set; }
        public Dictionary<string, CurrencyViewModel> Currencies { get; set; }
        public FlagsViewModel Flags { get; set; }
    }
}
