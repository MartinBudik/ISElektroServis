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
    /// Interaction logic for IzradaFakture.xaml
    /// </summary>
    public partial class IzradaFakture : Window
    {
        private MainWindow mw;

        public ObservableCollection<string> Popravka
        {
            get;
            set;
        }

        public ObservableCollection<string> BrojMesjaci
        {
            get;
            set;
        }

        public IzradaFakture(MainWindow w)
        {

            Popravka = new ObservableCollection<string>();
            Popravka.Add("Elektromotor");
            Popravka.Add("Rucni alati");


            BrojMesjaci = new ObservableCollection<string>();
            BrojMesjaci.Add("12");
            BrojMesjaci.Add("24");
            BrojMesjaci.Add("36");

            mw = w;
            InitializeComponent();
            this.DataContext = this;


        }

        public void ResetFields()
        {
            brojFaktureTextBox.Text = "";
            idKlijentTextBox.Text = "";
            adresaTextBox.Text = "";
            pibTextBox.Text = "";
            popravkaTextBox.Text = "";
            opisTextBox.Text = "";
            brojMeseci.Text = "";
            iznosSlovimaTextBox.Text = "";
            iznosTextBox.Text = "";
            datumFakture.SelectedDate = null;


        }

        private void OcistiDugme_Click(object sender, RoutedEventArgs e)
        {
            ResetFields();
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int brojFakture;
            Int32.TryParse(brojFaktureTextBox.Text, out brojFakture);

            int idKlijent;
            Int32.TryParse(idKlijentTextBox.Text, out idKlijent);

            int pibint;
            Int32.TryParse(pibTextBox.Text, out pibint);

            String popravka = (String)popravkaTextBox.SelectedItem;

            string bMeseci = (String)brojMeseci.SelectedItem;
            int mBroj;
            Int32.TryParse(bMeseci, out mBroj);

            double cena;
            Double.TryParse(iznosTextBox.Text, out cena);

            bool chekirano;
            if (Garancija.IsChecked == true)
            {
                chekirano = true;
            }
            else
            {
                chekirano = false;
            }     

            string dateString;

            if (datumFakture.SelectedDate != null)
            {
                DateTime datum = (DateTime)datumFakture.SelectedDate;
                dateString = datum.ToShortDateString();
            }
            else
            {
                dateString = "";
            }


            Faktura f = new Faktura(brojFakture, idKlijent, adresaTextBox.Text, pibint, popravka, chekirano, mBroj, opisTextBox.Text, iznosSlovimaTextBox.Text, cena, dateString);

            if (provera())
            {
                if (mw.Pod.Fakture == null)
                {
                    int nasao = -1;

                    if(mw.Pod.Klijenti != null)
                    {
                        foreach (Klijent k in mw.Pod.Klijenti)
                        {
                            if(k.Id == idKlijent)
                            {
                                k.Duzan = true;
                                nasao = 1;
                            }
                        }
                        if(nasao == -1)
                        {
                            MessageBox.Show("Ne postoji klijent sa tim ID-jem!");
                            return;
                        }
                    }

                    
                    mw.Pod.Fakture = new ObservableCollection<Faktura>();
                    mw.Pod.Fakture.Add(f);
                    mw.ListaFaktura.fakturaGrid.Items.Refresh();
                    mw.ListaKlijenta.klijentGrid.Items.Refresh();
                    this.Close();

                }
                else
                {
                    int nasao2 = -1;
                    foreach (Faktura fa in mw.Pod.Fakture)
                    {
                        if (fa.BrojFakture == brojFakture)
                        {
                            MessageBox.Show("Faktura sa tim ID-jem vec postoji!");
                            idKlijentTextBox.Focus();
                            return;
                        }
                    }

                    foreach (Klijent k in mw.Pod.Klijenti)
                    {
                        if (k.Id == idKlijent)
                        {
                            k.Duzan = true;
                            nasao2 = 1;
                        }
                    }
                    if (nasao2 == -1)
                    {
                        MessageBox.Show("Ne postoji klijent sa tim ID-jem!");
                        return;
                    }

                    mw.Pod.Fakture.Add(f);
                    mw.ListaFaktura.fakturaGrid.Items.Refresh();
                    mw.ListaKlijenta.klijentGrid.Items.Refresh();
                    this.Close();
                }
            }

        }

        private bool provera()
        {
            string message = "";
            int flag = -1;

            if (String.IsNullOrEmpty(brojFaktureTextBox.Text))
            {
                message += "Broj fakture ne sme biti prazno!\n";
                brojFaktureTextBox.Focus();
                flag = 1;
            }
            if (!String.IsNullOrEmpty(brojFaktureTextBox.Text) && !Int32.TryParse(brojFaktureTextBox.Text, out int id))
            {
                message += "Broj fakture mora biti broj!\n";
                brojFaktureTextBox.Focus();
                flag = 1;
            }
            if (!String.IsNullOrEmpty(brojFaktureTextBox.Text) && (brojFaktureTextBox.Text[0] == '-' || brojFaktureTextBox.Text[0] == '+'))
            {
                message += "Broj fakture mora biti pozitivan ceo broj!\n";
                brojFaktureTextBox.Focus();
                flag = 1;
            }
            if (String.IsNullOrEmpty(idKlijentTextBox.Text))
            {
                message += "ID Klijenta ne sme biti prazan!\n";
                brojFaktureTextBox.Focus();
                flag = 1;
            }
            if (!String.IsNullOrEmpty(idKlijentTextBox.Text) && !Int32.TryParse(idKlijentTextBox.Text, out int idk))
            {
                message += "ID Klijenta mora biti broj!\n";
                idKlijentTextBox.Focus();
                flag = 1;
            }
            if (!String.IsNullOrEmpty(pibTextBox.Text) && !Regex.IsMatch(pibTextBox.Text, @"^\d+$"))
            {
                message += "PIB mora biti broj!\n";
                pibTextBox.Focus();
                flag = 1;
            }
            if (!String.IsNullOrEmpty(iznosTextBox.Text) && !Double.TryParse(iznosTextBox.Text, out double ce))
            {
                message += "Cena mora biti broj!\n";
                iznosTextBox.Focus();
                flag = 1;
            }

            if (flag == 1)
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
