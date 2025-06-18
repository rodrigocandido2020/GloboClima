namespace GloboClima.Web.Shared.WeatherResponsesViews
{
    public class WeatherResponse
    {
        public MainInfo Main { get; set; }
        public List<WeatherInfo> Weather { get; set; }
        public SysInfo Sys { get; set; }
        public string Name { get; set; }
    }
}
