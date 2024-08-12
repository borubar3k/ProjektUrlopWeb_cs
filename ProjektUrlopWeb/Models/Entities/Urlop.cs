using System.ComponentModel.DataAnnotations;

namespace ProjektUrlopWeb.Models.Entities
{
    public class Urlop
    {
        [Key]
        public int Id_num { get; set; }
        public Guid Id { get; set; }
        public Pracownik Pracownik { get; set; } = null!;
        public DateTime DataRozp { get; set; }
        public DateTime DataZak { get; set; }
        public int IloscDni { get; set; }
        public string Rodzaj { get; set; } = null!;
        public bool IsAppr {  get; set; }
    }
}
