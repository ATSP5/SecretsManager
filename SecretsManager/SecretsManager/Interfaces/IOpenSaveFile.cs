using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretsManager.Interfaces
{
    public interface IOpenSaveFile
    {
        //Recive whole object
        IOpenSaveFile GetObject();
        //Recive Text from object
        string GetText();
        //Reset Object
        void Reset();
        //Recive operating mode
        OperatingMode GetOperatingMode();
        //Encrypt
        void Encrypt(EncryptionAlgorythm algorythm);
        //Decrypt
        void Decrypt(EncryptionAlgorythm algorythm);
    }
}
