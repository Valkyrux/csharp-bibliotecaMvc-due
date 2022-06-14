using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca_EntityFramework.Models
{
    [Table("prestiti")]
    public class Prestito
    {
        [Key]
        public long IdPrestito { get; set; }

        public DateTime data { get; set; }
    }
}
