using System.ComponentModel.DataAnnotations;

namespace ProjektUrlopWeb.Models.Entities
{
    public class Log
    {
        [Key]
        public Guid Id { get; set; }
        public Pracownik Pracownik { get; set; } = null!;
        public DateTime Data { get; set; }
    }
}
