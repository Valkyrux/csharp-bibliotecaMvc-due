using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca_EntityFramework.Models
{
    public enum Stato
    {
        Disponibile, 
        InPrestito
    }

    [Table("libri")]
    public class Libro
    {
        [Key]
        public long ID { get; set; }
        public string Titolo { get; set; }
        public DateTime Data { get; set; }

        public Stato stato { get; set; }
        public string ISBN { get; set; }
        public int? numeroDiPagine { get; set; }
        public ICollection<Autore> Autori { get; set; }

        public ICollection<Prestito>? Prestiti { get; set; }
    }
}
