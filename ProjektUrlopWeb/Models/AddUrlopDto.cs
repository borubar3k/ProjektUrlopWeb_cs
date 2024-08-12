namespace ProjektUrlopWeb.Models
{
    public class AddUrlopDto
    {
        public DateTime DataRozp {  get; set; }
        public DateTime DataZak { get; set; }
        public string Rodzaj { get; set; } = null!;
    }
}
