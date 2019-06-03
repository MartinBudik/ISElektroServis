using ISElektroServis.resursi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ISElektroServis
{
    /// <summary>
    /// Interaction logic for DodajKlijenta.xaml
    /// </summary>
    public partial class DodajKlijenta : Window
    {
        private MainWindow mw;

        public DodajKlijenta(MainWindow w)
        {
            mw = w;
            InitializeComponent();
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int id;
            Int32.TryParse(idKlijentaTextBox.Text, out id);

            Klijent k = new Klijent(id, imeKlijentaTextBox.Text, adresaTextBox.Text, pibTextBox.Text, kontaktBrojTextBox.Text, emailTextBox.Text);

            if (proveri())
            {
          
                if (mw.Pod.Klijenti == null)
                {
                    mw.Pod.Klijenti = new ObservableCollection<Klijent>();
                    mw.Pod.Klijenti.Add(k);
                    this.Close();

                }
                else
                {
                    foreach (Klijent kl in mw.Pod.Klijenti)
                    {
                        if (kl.Id == id)
                        {
                            MessageBox.Show("Klijent sa tim ID-jem vec postoji!");
                            idKlijentaTextBox.Focus();
                            return;
                        }
                    }

                    mw.Pod.Klijenti.Add(k);
                    this.Close();
                }
            }
        }

        private bool proveri()
        {
            string message = "";
            int flag = -1;

            if (String.IsNullOrEmpty(idKlijentaTextBox.Text))
            {
                message += "ID klijenta ne sme biti prazno!\n";
                idKlijentaTextBox.Focus();
                flag = 1;
            }
             if(!String.IsNullOrEmpty(idKlijentaTextBox.Text) && !Int32.TryParse(idKlijentaTextBox.Text, out int id))
            {
                message += "ID klijenta mora biti broj!\n";
                idKlijentaTextBox.Focus();
                flag = 1;
            }
             if (!String.IsNullOrEmpty(idKlijentaTextBox.Text) && (idKlijentaTextBox.Text[0] == '-' || idKlijentaTextBox.Text[0] == '+'))
            {
                message += "ID Klijenta mora biti pozitivan ceo broj!\n";
                idKlijentaTextBox.Focus();
                flag = 1;
            }
            if (idKlijentaTextBox.Text.Length > 1 && idKlijentaTextBox.Text[0] == '0')
            {
                message += "Kapacitet ne moze da pocnje sa 0!\n";
                idKlijentaTextBox.Focus();
                flag = 1;
            }
             if (String.IsNullOrEmpty(imeKlijentaTextBox.Text))
            {
                message += "Ime klijenta ne sme biti prazno!\n";
                imeKlijentaTextBox.Focus();
                flag = 1;
            }
            if (!String.IsNullOrEmpty(pibTextBox.Text) && !Regex.IsMatch(pibTextBox.Text, @"^\d+$"))
            {
                message += "PIB klijenta mora biti broj!\n";
                pibTextBox.Focus();
                flag = 1;
            }
             if (!Regex.Match(kontaktBrojTextBox.Text, "^[+]?[-/0-9]*$").Success)
            {
                message += "Neispravan broj telefona!\n";
                kontaktBrojTextBox.Focus();
                flag = 1;
            }

            if(flag == 1)
            {
                MessageBox.Show(message);
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
