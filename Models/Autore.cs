using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca_EntityFramework.Models
{
    public class Autore
    {
        [Column(Order = 1), System.ComponentModel.DataAnnotations.Key]
        public string Cognome { get; set; }
        [Column(Order = 2), System.ComponentModel.DataAnnotations.Key]
        public string Nome { get; set; }
        [Column(Order = 3), System.ComponentModel.DataAnnotations.Key]
        public DateTime DataNascita { get; set; }

        public ICollection<Libro>? Libri { get; set; }

    }
}
