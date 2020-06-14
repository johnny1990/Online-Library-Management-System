using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Models
{
    public class Clienti
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Clienti()
        {
            this.ImprumuturiCarti = new HashSet<ImprumuturiCarti>();
        }
        [Key]
        public int IdClient { get; set; }

        [Required]
        public string Nume { get; set; }

        [Required]
        public string Adresa { get; set; }

        [Required]
        public string Contact { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ImprumuturiCarti> ImprumuturiCarti { get; set; }
    }
}
