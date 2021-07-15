using SecretsManager.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SecretsManager
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        EncryptionMode encryptionMode;
        private BasicText _basicText;
        private AESService _aesService;
        private OFBServece _ofbService;

        public MainWindow()
        {
            InitializeComponent();
            _basicText = new BasicText();
            _aesService = AESService.GetInstance();
            _ofbService = OFBServece.GetInstance(ref _aesService);
            encryptionMode = EncryptionMode.AES;
            EncriptionModeTB.Text = "AES";
        }

        private void mINew_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mIOpenPublicText_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mIOpenSecretText_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mISavePublicText_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mISaveSecretText_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mIClose_Click(object sender, RoutedEventArgs e)
        {
            try 
            {
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void mIAES_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mIOFBStream_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mISetPassword_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MainWorkspace_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
