using System;
using System.Text;

namespace SecretsManager
{
    class OFBServece
    {
        private AESService aES;
        private string hiddenText = "";
        private OFBServece(ref AESService aesmanager)
        {
            aES = aesmanager;
        }
        private static OFBServece _instance;
        public static OFBServece GetInstance(ref AESService aesmanager)
        {
            if (_instance == null)
            {
                _instance = new OFBServece(ref aesmanager);
            }
            return _instance;
        }
        byte[] STRToBYTE(string txt)
        {
            return Encoding.Default.GetBytes(txt);
        }
        string BYTEToSTR(byte[] bytes)
        {
            return Encoding.Default.GetString(bytes);
        }
        public void SetTextSreamResource(string text)
        {
            hiddenText = text;
        }
        public bool IsTextStreamResourceSet()
        {
            if (hiddenText != "")
                return true;
            else
                return false;
        }
        public string Run(string txt)
        {
            if (hiddenText != "")
            {
                string hiddenResourceText = aES.EncryptAESManaged(hiddenText);
                byte[] hiddenResourceByteTable = STRToBYTE(hiddenResourceText);
                byte[] txtbytetable = STRToBYTE(txt);
                int resultsize = 0;
                if (hiddenResourceByteTable.Length > txtbytetable.Length)
                {
                    resultsize = hiddenResourceByteTable.Length;
                }
                else
                {
                    resultsize = txtbytetable.Length;
                }
                byte[] resultByteTable = new byte[resultsize];
                for (int i = 0; i < resultsize; i++)
                {
                    if (txtbytetable.Length > i)
                    {
                        resultByteTable[i] = (byte)(txtbytetable[i] ^ hiddenResourceByteTable[i % hiddenResourceByteTable.Length]);
                    }
                    else
                    {
                        resultByteTable[i] = (byte)(0 ^ hiddenResourceByteTable[i % hiddenResourceByteTable.Length]);
                    }
                }
                return BYTEToSTR(resultByteTable);
            }
            else
            {
                throw new ArgumentException("Parameter cannot be null", nameof(hiddenText));
            }
        }
    }
}
