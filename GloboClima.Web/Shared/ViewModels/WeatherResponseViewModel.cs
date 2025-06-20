namespace GloboClima.Web.Shared.ViewModels
{
    public class WeatherResponseViewModel
    {
        public MainInfoViewModel Main { get; set; }
        public List<WeatherInfoViewModel> Weather { get; set; }
        public SysInfoViewModel Sys { get; set; }
        public string Name { get; set; }
    }
}
