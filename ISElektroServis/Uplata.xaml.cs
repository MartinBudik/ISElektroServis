using ISElektroServis.resursi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for Uplata.xaml
    /// </summary>
    public partial class Uplata : Window
    {

        private MainWindow mw;

        public Uplata(MainWindow w)
        {
            mw = w;
            InitializeComponent();
            this.DataContext = this;
        }

        private void OtkaziDugme_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            int IdFakture;
            double iznos;

            Int32.TryParse(idFaktura.Text, out IdFakture);
            Double.TryParse(iznosUplate.Text, out iznos);

            foreach (Faktura f in mw.Pod.Fakture)
            {
                if(IdFakture == f.BrojFakture && !String.IsNullOrEmpty(iznosUplate.Text))
                {
                    f.Iznos -= iznos;
                    if (f.Iznos <= 0)
                    {
                        f.Iznos = 0;
                        foreach (Klijent k in mw.Pod.Klijenti)
                        {
                            if (k.Id == f.IdKlijenta)
                            {
                                k.Duzan = false;
                                mw.ListaKlijenta.klijentGrid.Items.Refresh();
                            }
                        }
                        MessageBox.Show("Klijent sa ID-jem " + f.IdKlijenta + " nije vise duzan.");
                    }
                    else
                    {
                        MessageBox.Show("Klijent sa ID-jem " + f.IdKlijenta + " je uplatio " + iznos + " na fakturu " + f.BrojFakture + " i sad je duzan jos " + f.Iznos.ToString());
                    }                  
                }
            }

            iznosUplate.Clear();
            mw.ListaFaktura.fakturaGrid.Items.Refresh();
            
                    
        }
    }
}
