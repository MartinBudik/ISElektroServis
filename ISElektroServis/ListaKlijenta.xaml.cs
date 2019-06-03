using ISElektroServis.resursi;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ISElektroServis
{
    /// <summary>
    /// Interaction logic for ListaKlijenta.xaml
    /// </summary>
    public partial class ListaKlijenta : UserControl
    {
        private Window parentWindow;
        private MainWindow mw;

        public ListaKlijenta()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(MainView_Loaded);
   
        }

        void MainView_Loaded(object sender, RoutedEventArgs e)
        {
            parentWindow = Window.GetWindow(this);
            mw = parentWindow as MainWindow;
        }

        private void BtnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            int index = klijentGrid.SelectedIndex;

            if(index != -1)
            {
                Klijent k = (Klijent)klijentGrid.SelectedItem;
                IzmenaKlijenta iwindow = new IzmenaKlijenta(k, mw);
                iwindow.Owner = mw;
                iwindow.ShowDialog();
                return;
            }

        }

        private void BtnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "JSON file (*.json)|*.json";
            sfd.Title = "Sacuvaj listu klijenata";

            if (sfd.ShowDialog() == true)
            {
                using (StreamWriter file = File.CreateText(sfd.FileName))
                {
                    JsonSerializer serializer = new JsonSerializer();

                    serializer.Serialize(file, mw.Pod.Klijenti);
                }
            }
        }

        private void BtnUcitaj_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Odaberite listu klijenata";
            op.Filter = "JSON file (*.json)|*.json";

            if (op.ShowDialog() == true)
            {
                StreamReader file = File.OpenText(op.FileName);

                using (file)
                {
                    JsonSerializer serializer = new JsonSerializer();
                    mw.Pod.Klijenti = (ObservableCollection<Klijent>)serializer.Deserialize(file, typeof(ObservableCollection<Klijent>));
                    klijentGrid.ItemsSource = mw.Pod.Klijenti;
                }
            }
        }
    }
}
