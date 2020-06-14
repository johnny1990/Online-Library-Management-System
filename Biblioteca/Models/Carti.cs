using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Models
{
    public class Carti
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Carti()
        {
            this.ImprumuturiCarti = new HashSet<ImprumuturiCarti>();
        }
        [Key]
        public int IdCarte { get; set; }

        [Required]
        public string Titlu { get; set; }
        [Required]
        public string Cod { get; set; }
        public string Autor { get; set; }
        public string Editura { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ImprumuturiCarti> ImprumuturiCarti { get; set; }
    }
}
