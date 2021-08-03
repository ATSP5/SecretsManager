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

        private bool _enableWorkspace;

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

        public bool EnableWorkspace
        {
            get
            {
                return _enableWorkspace;
            }
            set
            {
                _enableWorkspace = value;
                this.NotifyPropertyChanged("EnableWorkspace");
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
                EnableWorkspace = !value;
                this.NotifyPropertyChanged("IsEncrypted");
            }
        }

        public BasicText()
        {
            _text = "";
            IsEncrypted = false;
            EnableWorkspace = true;
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
            IsEncrypted = true;
        }
        public void Decrypt(EncryptionAlgorythm algorythm)
        {
            var fasade = new EncriptionFasade();
            Text = fasade.Dectypt(_text, algorythm);
            IsEncrypted = false;
        }
    }
}
