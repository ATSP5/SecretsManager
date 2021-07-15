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

namespace SecretsManager.Views
{
    /// <summary>
    /// Logika interakcji dla klasy SetPassword.xaml
    /// </summary>
    public partial class SetPassword : Window
    {
        public SetPassword()
        {
            InitializeComponent();
            _aesService = DataEncryptor.AESService.GetInstance();
        }

        private DataEncryptor.AESService _aesService;


        private void SPWPasswordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                SPWOK_Click(sender, e);
            }
           
        }

        private void SPWCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void SPWOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(SPWPasswordBox.Password!="")
                {
                    _aesService.SetKey(SPWPasswordBox.Password);
                }
                else
                {
                    MessageBox.Show("No password? Are you serious?");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                this.Close();
            }
        }
    }
}
