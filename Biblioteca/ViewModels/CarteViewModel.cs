using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca.ViewModels
{
    public class CarteViewModel
    {
        public int IdCarte { get; set; }

        public string Titlu { get; set; }

        public string Cod { get; set; }

        public string Autor { get; set; }

        public string Editura { get; set; }

        public bool Disponibil { get; set; }
    }
}
