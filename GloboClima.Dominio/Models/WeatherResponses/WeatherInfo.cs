using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GloboClima.Dominio.Models.WeatherResponses
{
    public class WeatherInfo
    {
        public string Main { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
    }
}
