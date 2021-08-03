using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretsManager.Containers
{
    class EncriptionFasade
    {
        private AESService _aesService;
        private OFBServece _ofbService;

        public EncriptionFasade()
        {
            _aesService = AESService.GetInstance();
            _ofbService = OFBServece.GetInstance(ref _aesService);
        }

        public void ResetKey()
        {
            _aesService.SetZeroKey();
        }
        public void SetKey(string key)
        {
            _aesService.SetKey(key);
        }

        public void SetOFBStream(string stream)
        {
            _ofbService.SetTextSreamResource(stream);
        }

        public string Dectypt(string textToDecrypt, EncryptionAlgorythm algorythm)
        {
            string secretText = "";
            if (_aesService.IsKeySet == true)
            {
                switch (algorythm)
                {
                    case EncryptionAlgorythm.AES:
                        secretText = _aesService.DecryptAESManaged(textToDecrypt);
                        break;
                    case EncryptionAlgorythm.OFB:
                        secretText = _ofbService.Run(textToDecrypt);
                        break;
                }
            }
            else
            {
                throw new Exception("You must set key first!");
            }
            return secretText;
        }

        public string Enctypt(string textToEncrypt, EncryptionAlgorythm algorythm)
        {
            string secretText = "";
            if(_aesService.IsKeySet== true)
            {
                switch (algorythm)
                {
                    case EncryptionAlgorythm.AES:
                        secretText = _aesService.EncryptAESManaged(textToEncrypt);
                        break;
                    case EncryptionAlgorythm.OFB:
                        secretText = _ofbService.Run(textToEncrypt);
                        break;
                }
            }
            else
            {
                throw new Exception("You must set key first!");
            }
            return secretText;
        }

        public bool GetKeyState()
        {
            return _aesService.IsKeySet;
        }
    }
}
