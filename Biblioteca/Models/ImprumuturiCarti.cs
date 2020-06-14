using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Models
{
    public class ImprumuturiCarti
    {   [Key]
        public int IdImprumut { get; set; }

        [Required]
        [Display(Name = "Carte")]
        public int IdCarte { get; set; }

        [Required]
        [Display(Name = "Client")]
        public int IdClient { get; set; }

        [Display(Name = "Data imprumut")]
        public System.DateTime DataImprumut { get; set; }

        [Display(Name = "Data returnare")]
        public Nullable<System.DateTime> DataReturnare { get; set; }

        public virtual Carti Carti { get; set; }
        public virtual Clienti Clienti { get; set; }
    }
}
