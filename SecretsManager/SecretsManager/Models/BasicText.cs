using SecretsManager.Containers;
using SecretsManager.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretsManager.Models
{
    [Serializable]
    public class BasicText : INotifyPropertyChanged, IOpenSaveFile
    {
        private string _text;

        private bool _isEncrypted;

        public readonly OperatingMode operatingMode;

        [field: NonSerialized()]
        public event PropertyChangedEventHandler PropertyChanged;

        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                this.NotifyPropertyChanged("Text");
            }
        }

        public bool IsEncrypted
        {
            get
            {
                return _isEncrypted;
            }
            set
            {
                _isEncrypted = value;
                this.NotifyPropertyChanged("IsEncrypted");
            }
        }

        public BasicText()
        {
            _text = "";
            _isEncrypted = false;
            operatingMode = OperatingMode.TextMode;
        }
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        //Interface implementation for this class:

        public IOpenSaveFile GetObject()
        {
            return this;
        }

        public string GetText()
        {
            return _text;
        }

        public void Reset()
        {
            _text = "";
            _isEncrypted = false;
        }

        public OperatingMode GetOperatingMode()
        {
            return operatingMode;
        }

        public void Encrypt(EncryptionAlgorythm algorythm)
        {
            var fasade = new EncriptionFasade();
            Text = fasade.Enctypt(_text, algorythm);
          
        }
        public void Decrypt(EncryptionAlgorythm algorythm)
        {
            var fasade = new EncriptionFasade();
            Text = fasade.Dectypt(_text, algorythm);
        }
    }
}
