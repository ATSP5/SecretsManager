using SecretsManager.Containers;
using SecretsManager.Interfaces;
using SecretsManager.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SecretsManager.Services
{
    public class MainService
    {
        private EncriptionFasade _encriptionEngine;

        public MainService()
        {
            _encriptionEngine = new EncriptionFasade();
        }

        public IOpenSaveFile OpenObject( string fileName)
        {
            Stream str = null;
            IOpenSaveFile _object;
            try
            {
                str = File.Open(fileName, FileMode.Open);
                var binaryFormatter = new BinaryFormatter();
                _object = (IOpenSaveFile)binaryFormatter.Deserialize(str);
            }
            finally
            {
                if(str!= null)
                {
                    str.Close();
                }
            }
            return _object;
        }


        public void OpenText(BasicText file, string fileName)
        {
            file.Text = File.ReadAllText(fileName);
        }

        public void OpenOFBStreamFile(string fileName)
        {
            _encriptionEngine.SetOFBStream(File.ReadAllText(fileName));
        }

        public void SaveText(IOpenSaveFile file, string fileName)
        {
            File.WriteAllText(fileName, file.GetText());
        }

        public void SaveObject(IOpenSaveFile file, string fileName)
        {
            Stream str = null;
            try
            {
                str = File.Create(fileName);
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(str, file);
            }
            finally
            {
                if(str!= null)
                {
                    str.Close();
                }
                
            }
           
        }

        public void SetPassword(string password)
        {
            _encriptionEngine.SetKey(password);
        }

        public void SetOfbText(string text)
        {
            _encriptionEngine.SetOFBStream(text);
        }

        public void Reset(IOpenSaveFile file)
        {
            file.Reset();
        }

        public bool GetKeyState()
        {
            return _encriptionEngine.GetKeyState();
        }

        public void ResetKey()
        {
            _encriptionEngine.ResetKey();
        }
    }
}
