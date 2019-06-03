using System;
using System.Collections.Generic;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DodajKlijenta dodajKlijenta;
        private IzradaFakture dodajFakturu;
        private Podaci pod;

        public static RoutedCommand cmdDodajKlijenta = new RoutedCommand();
        public static RoutedCommand cmdListaKlijenata = new RoutedCommand();
        public static RoutedCommand cmdProveraDugovanja = new RoutedCommand();
        public static RoutedCommand cmdDodajFakturu = new RoutedCommand();
        public static RoutedCommand cmdListaFaktura = new RoutedCommand();
        public static RoutedCommand cmdUplata = new RoutedCommand();

        public Podaci Pod
        {
            get { return this.pod; }
            set { this.pod = value; }
        }

        public MainWindow()
        {
            pod = new Podaci();
            InitializeComponent();
            initCommands();  
        }

        private void Menu_Izlaz_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Menu_Pomoc_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(Directory.GetCurrentDirectory() + @"\iselektroservis.chm");

           
        }


        private void MenuSvetlaTema_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources.MergedDictionaries[1].Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Light.xaml", UriKind.RelativeOrAbsolute);
        }

        private void MenuTamnaTema_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources.MergedDictionaries[1].Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Dark.xaml", UriKind.RelativeOrAbsolute);
        }

        private void Menu_Faktura_Izrada_Click(object sender, RoutedEventArgs e)
        {

            dodajFakturu = new IzradaFakture(this);
            dodajFakturu.Owner = this;
            dodajFakturu.ShowDialog();

            SlikaLogo.Visibility = Visibility.Visible;
            ListaFaktura.Visibility = Visibility.Hidden;
            ListaKlijenta.Visibility = Visibility.Hidden;
        }

        private void Menu_Lista_faktura_Click(object sender, RoutedEventArgs e)
        {
            ListaFaktura.Visibility = Visibility.Visible;
            ListaFaktura.fakturaGrid.ItemsSource = Pod.Fakture;

            SlikaLogo.Visibility = Visibility.Hidden;
            ListaKlijenta.Visibility = Visibility.Hidden;

        }

        private void Menu_Dodaj_klijenta_Click(object sender, RoutedEventArgs e)
        {
            dodajKlijenta = new DodajKlijenta(this);
            dodajKlijenta.Owner = this;
            dodajKlijenta.ShowDialog();

            ListaFaktura.Visibility = Visibility.Hidden;
            SlikaLogo.Visibility = Visibility.Visible;
            ListaKlijenta.Visibility = Visibility.Hidden;

        }

        private void Menu_Lista_klijenta_Click(object sender, RoutedEventArgs e)
        {
            ListaKlijenta.Visibility = Visibility.Visible;
            ListaKlijenta.klijentGrid.ItemsSource = Pod.Klijenti;

            ListaFaktura.Visibility = Visibility.Hidden;
            SlikaLogo.Visibility = Visibility.Hidden;

        }

        private void Menu_Home_Click(object sender, RoutedEventArgs e)
        {
            SlikaLogo.Visibility = Visibility.Visible;


            ListaFaktura.Visibility = Visibility.Hidden;
            ListaKlijenta.Visibility = Visibility.Hidden;
        }

        private void Menu_Provera_dugovanja_Click(object sender, RoutedEventArgs e)
        {
            ProveraDugovanja proveraDugovanja = new ProveraDugovanja();
            proveraDugovanja.Show();

        }

        private void Menu_Uplata_Click(object sender, RoutedEventArgs e)
        {
            Uplata uplata = new Uplata(this);
            uplata.Show();

        }

        public void initCommands()
        {
            cmdDodajKlijenta.InputGestures.Add(new KeyGesture(Key.K, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(cmdDodajKlijenta, Menu_Dodaj_klijenta_Click));

            cmdListaKlijenata.InputGestures.Add(new KeyGesture(Key.K, ModifierKeys.Control | ModifierKeys.Shift));
            CommandBindings.Add(new CommandBinding(cmdListaKlijenata, Menu_Lista_klijenta_Click));

            cmdProveraDugovanja.InputGestures.Add(new KeyGesture(Key.D, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(cmdProveraDugovanja, Menu_Provera_dugovanja_Click));

            cmdDodajFakturu.InputGestures.Add(new KeyGesture(Key.F, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(cmdDodajFakturu, Menu_Faktura_Izrada_Click));

            cmdListaFaktura.InputGestures.Add(new KeyGesture(Key.F, ModifierKeys.Control | ModifierKeys.Shift));
            CommandBindings.Add(new CommandBinding(cmdListaFaktura, Menu_Lista_faktura_Click));

            cmdUplata.InputGestures.Add(new KeyGesture(Key.U, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(cmdUplata, Menu_Uplata_Click));
        }

    }
}
