using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISElektroServis.resursi
{
    public class Klijent
    {
        private int id;
        private string ime;
        private string adresa;
        private string pib;
        private string kontaktBroj;
        private string email;
        private bool duzan;


        public Klijent(int id, string ime, string adresa, string pib, string kontaktBroj, string email)
        {
            this.id = id;
            this.ime = ime;
            this.adresa = adresa;
            this.pib = pib;
            this.kontaktBroj = kontaktBroj;
            this.email = email;
            this.duzan = false;
        }

        public int Id { get => id; set => id = value; }
        public string Ime { get => ime; set => ime = value; }
        public string Pib { get => pib; set => pib = value; }
        public string Adresa { get => adresa; set => adresa = value; }
        public string KontaktBroj { get => kontaktBroj; set => kontaktBroj = value; }
        public string Email { get => email; set => email = value; }
        public bool Duzan { get => duzan; set => duzan = value; }

        public string ispisDuzan
        {
            get
            {
                if (Duzan)
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
