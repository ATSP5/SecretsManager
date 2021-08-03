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
using SecretsManager.Services;
using Microsoft.Win32;
using SecretsManager.Interfaces;
using SecretsManager.Containers;
using SecretsManager.Views;

namespace SecretsManager
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public static class CustomCommands // For the purpose of commands eg.: Ctrl+E handling!
    {
        public static readonly RoutedUICommand EncryptCMD = new RoutedUICommand
            (
                "EncryptCMD",
                "EncryptCMD",
                typeof(CustomCommands),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.E, ModifierKeys.Control)
                }
            );
    public static readonly RoutedUICommand DecryptCMD = new RoutedUICommand
       (
           "DecryptCMD",
           "DecryptCMD",
           typeof(CustomCommands),
           new InputGestureCollection()
           {
                    new KeyGesture(Key.D, ModifierKeys.Control)
           }
       );

    //Define more commands here, just like the one above
}
public partial class MainWindow : Window
    {

        EncryptionAlgorythm encryptionMode;
        private BasicText _basicText;
        private MainService _mainService;
        public MainWindow()
        {
            InitializeComponent();
            _basicText = new BasicText();
            encryptionMode = EncryptionAlgorythm.AES;
            EncriptionModeTB.Text = "AES";
            MainWorkspace.DataContext = _basicText;
            _mainService = new MainService();
        }

        private void EncryptCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void EncryptCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                switch (tcOperatingMode.SelectedIndex)
                {
                    case 0:
                        _basicText.Encrypt(encryptionMode);
                        break;
                    case 1:
                        break;
                    case 2:
                        break;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
          
        }

        private void DecryptCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void DecryptCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                switch (tcOperatingMode.SelectedIndex)
                {
                    case 0:
                        _basicText.Decrypt(encryptionMode);
                        break;
                    case 1:
                        break;
                    case 2:
                        break;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void mINew_Click(object sender, RoutedEventArgs e)
        {
            _mainService.Reset(_basicText.GetObject());
            _mainService.ResetKey();
            mISeetPasswordCHB.IsChecked = _mainService.GetKeyState();
        }

        private void mIOpenAsText_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _mainService.Reset(_basicText.GetObject());
                Open(OpenSaveMode.AsRegularText); 
            }
            catch (Exception exeption)
            {
                MessageBox.Show(exeption.Message);
            }
        }

        private void mIOpenPublicText_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _mainService.Reset(_basicText.GetObject());
                Open(OpenSaveMode.AsPublicText);
            }
            catch (Exception exeption)
            {
                MessageBox.Show(exeption.Message);
            }
        }

        private void mIOpenSecretText_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _mainService.Reset(_basicText.GetObject());
                Open(OpenSaveMode.AsSecretText);
            }
            catch (Exception exeption)
            {
                MessageBox.Show(exeption.Message);
            }
        }

        private void mISavePublicText_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(_basicText.IsEncrypted == false)
                {
                    Save(OpenSaveMode.AsPublicText);
                }
               else
                {
                    MessageBox.Show("You are trying to save encrypted text as public text" +
                        "Try to decrypt text or save as secret text!");
                }
            }
            catch (Exception exeption)
            {
                MessageBox.Show(exeption.Message);
            }
        }

        private void mISaveSecretText_Click(object sender, RoutedEventArgs e)
        {
              try
            {
                if (_basicText.IsEncrypted == true)
                {
                    Save(OpenSaveMode.AsSecretText);
                }
                else
                {
                    MessageBox.Show("You are trying to save public text as encrypted text" +
                       "Try to encrypt text or save as public text!");
                }    
            }
            catch (Exception exeption)
            {
                MessageBox.Show(exeption.Message);
            }
        }

        private void mISaveAsText_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_basicText.IsEncrypted == true)
                {
                   if( MessageBox.Show("Are you sure you want to save encrypted text as regular text?", "Warning!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        Save(OpenSaveMode.AsRegularText);
                    }
                }
                else
                {
                    Save(OpenSaveMode.AsRegularText);
                }
            }
            catch (Exception exeption)
            {
                MessageBox.Show(exeption.Message);
            }
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
            encryptionMode = EncryptionAlgorythm.AES;
            EncriptionModeTB.Text = "AES";
        }

        private void mIOFBStream_Click(object sender, RoutedEventArgs e)
        {
            encryptionMode = EncryptionAlgorythm.OFB;
            EncriptionModeTB.Text = "OFB";
            var openfileDialog = new OpenFileDialog();
            openfileDialog.Filter = "txt files (*.txt)|*.txt|(*.html)|*.html|(*.xml)|*.xml|Other file format (*.*)|*.*";
            if (openfileDialog.ShowDialog() == true)
            {
                _mainService.OpenOFBStreamFile(openfileDialog.FileName);
            }
        }

        private void mISetPassword_Click(object sender, RoutedEventArgs e)
        {
            var setPasswordWnd = new SetPassword();
            setPasswordWnd.ShowDialog();
            mISeetPasswordCHB.IsChecked = _mainService.GetKeyState();
        }

        private void Open(OpenSaveMode openSaveMode)
        {
            var openFileDialog = new OpenFileDialog();
            IOpenSaveFile readFile = null;
            switch (openSaveMode)
             {
               case OpenSaveMode.AsPublicText:
               openFileDialog.Filter = "Public text files (*.ptf)|*.ptf";
               if (openFileDialog.ShowDialog() == true)
               {
                     readFile = _mainService.OpenObject(openFileDialog.FileName);
               }
               break;
               case OpenSaveMode.AsSecretText:
               openFileDialog.Filter = "Public text files (*.stf)|*.stf";
               if (openFileDialog.ShowDialog() == true)
               {
                     readFile = _mainService.OpenObject(openFileDialog.FileName);
               }
               break;
               case OpenSaveMode.AsRegularText:
               openFileDialog.Filter = "txt files (*.txt)|*.txt|(*.html)|*.html|(*.xml)|*.xml|Other file format (*.*)|*.*";
               if (openFileDialog.ShowDialog() == true)
                {
                  _mainService.OpenText(_basicText, openFileDialog.FileName);
                }
                break;
             }
            ApplyOpenedFile(readFile);
        }

        private void ApplyOpenedFile(IOpenSaveFile file)
        {
            if (file != null)
            {
                switch (file.GetOperatingMode())
                {
                    case OperatingMode.TextMode:
                        _mainService.Reset(_basicText.GetObject());
                        var readFile = (BasicText)file.GetObject();
                        _basicText.Text = readFile.Text;
                        _basicText.IsEncrypted = readFile.IsEncrypted;
                        break;
                    case OperatingMode.TwoDDataGrid:
                        break;
                    case OperatingMode.TreeDDataGrid:
                        break;
                }
            }
        }
        private void Save(OpenSaveMode openSaveMode)
        {
            switch (tcOperatingMode.SelectedIndex)
            {
                case 0: //Text mode
                        var saveFileDialog = new SaveFileDialog();
                    switch (openSaveMode)
                    {
                        case OpenSaveMode.AsPublicText:
                            saveFileDialog.Filter = "Public text files (*.ptf)|*.ptf";
                            if (saveFileDialog.ShowDialog() == true)
                            {
                                _mainService.SaveObject(_basicText.GetObject(), saveFileDialog.FileName);
                            }
                            break;
                        case OpenSaveMode.AsSecretText:
                            saveFileDialog.Filter = "Public text files (*.stf)|*.stf";
                            if (saveFileDialog.ShowDialog() == true)
                            {
                                _mainService.SaveObject(_basicText.GetObject(), saveFileDialog.FileName);
                            }
                            break;
                        case OpenSaveMode.AsRegularText:
                            saveFileDialog.Filter = "txt files (*.txt)|*.txt|(*.html)|*.html|(*.xml)|*.xml|Other file format (*.*)|*.*";
                            if (saveFileDialog.ShowDialog() == true)
                            {
                                _mainService.SaveText(_basicText.GetObject(), saveFileDialog.FileName);
                            }
                            break;
                    }
                    break;
                case 1: // 2D Table mode

                    break;
                case 2: //3D Table mode

                    break;
                default:

                    break;
            }
        }
        
    }
}
