using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca_EntityFramework.Models
{
    [Table("utenti")]
    public class Utente
    {
        [Key]
        public long UtenteID { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string? Email { get; set; }

        public ICollection<Prestito>? Prestiti { get; set; }
    }
}
