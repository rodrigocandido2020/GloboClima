using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GloboClima.Dominio.Models.WeatherResponses
{
    public class MainInfo
    {
        public float Temp { get; set; }
        public float Feels_like { get; set; }
        public float Temp_min { get; set; }
        public float Temp_max { get; set; }
        public int Humidity { get; set; }
    }

}
