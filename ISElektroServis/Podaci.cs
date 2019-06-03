using ISElektroServis.resursi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISElektroServis
{
    public class Podaci
    {
        private ObservableCollection<Klijent> klijenti;
        private ObservableCollection<Faktura> fakture;

        public ObservableCollection<Klijent> Klijenti
        {
            get { return this.klijenti; }
            set { this.klijenti = value; }
        }

        public ObservableCollection<Faktura> Fakture
        {
            get { return this.fakture; }
            set { this.fakture = value; }
        }

    }
}
