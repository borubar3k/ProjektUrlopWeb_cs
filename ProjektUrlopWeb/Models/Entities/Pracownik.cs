using System.ComponentModel.DataAnnotations;

namespace ProjektUrlopWeb.Models.Entities
{
    public class Pracownik
    {
        [Key]
        public int Id_num { get; set; }
        public Guid Id { get; set; }
        public string Imie { get; set; } = null!;
        public string Nazwisko { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Haslo { get; set; } = null!;
        public bool IsAdmin { get; set; }
        public bool IsArch { get; set; }
        public DateTime DataZatr { get; set; }
        public int DniUrlopu { get; set; }

    }
}
