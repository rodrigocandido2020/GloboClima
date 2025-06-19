namespace GloboClima.Dominio.Models.PaisResponse
{
    public class PaisResponse
    {
        public Name Name { get; set; }
        public List<string> Capital { get; set; }
        public string Region { get; set; }
        public string Subregion { get; set; }
        public long Population { get; set; }
        public Dictionary<string, string> Languages { get; set; }
        public Dictionary<string, Currency> Currencies { get; set; }
        public Flags Flags { get; set; }
        public Dictionary<string, Translation> Translations { get; set; }
    }
}
