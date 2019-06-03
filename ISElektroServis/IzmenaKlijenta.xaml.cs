using ISElektroServis.resursi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ISElektroServis
{
    /// <summary>
    /// Interaction logic for IzmenaKlijenta.xaml
    /// </summary>
    public partial class IzmenaKlijenta : Window
    {
        private Klijent klijent;
        private MainWindow mw;

        public IzmenaKlijenta(Klijent selektovani_klijent, MainWindow window)
        {
            InitializeComponent();
            this.DataContext = this;

            klijent = selektovani_klijent;
            mw = window;

            idKlijentaTextBox.Text = selektovani_klijent.Id.ToString();
            imeKlijentaTextBox.Text = selektovani_klijent.Ime;
            adresaTextBox.Text = selektovani_klijent.Adresa;
            pibTextBox.Text = selektovani_klijent.Pib;
            kontaktBrojTextBox.Text = selektovani_klijent.KontaktBroj;
            emailTextBox.Text = selektovani_klijent.Email;

        }

        public void ResetFields()
        {
            idKlijentaTextBox.Text = "";
            imeKlijentaTextBox.Text = "";
            adresaTextBox.Text = "";
            pibTextBox.Text = "";
            kontaktBrojTextBox.Text = "";
            emailTextBox.Text = "";

        }

        private void OcistiDugme_Click(object sender, RoutedEventArgs e)
        {
            ResetFields();
        }

        private bool proveri()
        {
            if (String.IsNullOrEmpty(idKlijentaTextBox.Text))
            {
                MessageBox.Show("ID klijenta ne sme biti prazno!");
                idKlijentaTextBox.Focus();
                return false;
            }
            else if (!String.IsNullOrEmpty(idKlijentaTextBox.Text) && !Int32.TryParse(idKlijentaTextBox.Text, out int id))
            {
                MessageBox.Show("ID klijenta mora biti broj!");
                idKlijentaTextBox.Focus();
                return false;
            }
            else if (!String.IsNullOrEmpty(idKlijentaTextBox.Text) && (idKlijentaTextBox.Text[0] == '-' || idKlijentaTextBox.Text[0] == '+'))
            {
                MessageBox.Show("ID Klijenta mora biti pozitivan ceo broj!");
                idKlijentaTextBox.Focus();
                return false;
            }
            else if (idKlijentaTextBox.Text.Length > 1 && idKlijentaTextBox.Text[0] == '0')
            {
                MessageBox.Show("Kapacitet ne moze da pocnje sa 0!");
                idKlijentaTextBox.Focus();
                return false;
            }
            else if (String.IsNullOrEmpty(imeKlijentaTextBox.Text))
            {
                MessageBox.Show("Ime klijenta ne sme biti prazno!");
                imeKlijentaTextBox.Focus();
                return false;
            }
            else if (!String.IsNullOrEmpty(pibTextBox.Text) && !Regex.IsMatch(pibTextBox.Text, @"^\d+$"))
            {
                MessageBox.Show("PIB klijenta mora biti broj!");
                pibTextBox.Focus();
                return false;
            }
            else if (!Regex.Match(kontaktBrojTextBox.Text, "^[+]?[-/0-9]*$").Success)
            {
                MessageBox.Show("Neispravan broj telefona!");
                kontaktBrojTextBox.Focus();
                return false;
            }

            return true;
        }

        private void BtnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            foreach (Klijent k in mw.Pod.Klijenti)
            {
                if (k.Id == klijent.Id)
                {
                    if (proveri())
                    {
                        k.Ime = imeKlijentaTextBox.Text;
                        k.Adresa = adresaTextBox.Text;
                        k.Pib = pibTextBox.Text;
                        k.KontaktBroj = kontaktBrojTextBox.Text;
                        k.Email = emailTextBox.Text;
                        mw.ListaKlijenta.klijentGrid.Items.Refresh();
                        this.Close();
                    }                
                }
            }
        }
    }
}
