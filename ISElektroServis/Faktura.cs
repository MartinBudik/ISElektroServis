using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISElektroServis
{
   public class Faktura
    {
        private int brojFakture;
        private int idKlijenta;
        private string adresa;
        private int pib;
        private string popravka;
        private bool garancija;
        private int brojMeseci;
        private string opis;
        private string iznosSlovima;
        private double iznos;
        private string datumFakturisanja;

        public Faktura(int brojFakture, int idKlijenta, string adresa, int pib, string popravka, bool gar, int bm, string opis, string iznosS, double iznos, string datumFakturisanja)
        {
            this.brojFakture = brojFakture;
            this.idKlijenta = idKlijenta;
            this.adresa = adresa;
            this.pib = pib;
            this.popravka = popravka;
            this.garancija = gar;
            this.brojMeseci = bm;
            this.opis = opis;
            this.iznosSlovima = iznosS;
            this.iznos = iznos;
            this.datumFakturisanja = datumFakturisanja;
        }

        public int BrojFakture { get => brojFakture; set => brojFakture = value; }
        public int IdKlijenta { get => idKlijenta; set => idKlijenta = value; }
        public int Pib { get => pib; set => pib = value; }
        public int BrojMeseci { get => brojMeseci; set => brojMeseci = value; }
        public string Adresa { get => adresa; set => adresa = value; }
        public string Opis { get => opis; set => opis = value; }
        public string Popravka { get => popravka; set => popravka = value; }
        public double Iznos { get => iznos; set => iznos = value; }
        public string IznosSlovima { get => iznosSlovima; set => iznosSlovima = value; }
        public string DatumFakturisanja { get => datumFakturisanja; set => datumFakturisanja = value; }
        public bool Garancija { get => garancija; set => garancija = value; }

        public string ispisGarancija
        {
           get            
            {
                if (Garancija)
                {
                    return "Da";
                }
                else
                {
                    return "Ne";
                }
            }
        }

    }
}
