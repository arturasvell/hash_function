using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Reflection;

namespace Hash_function
{
    class Cryptography
    {
        public static string timeScores = "";
        //MD5
        public static string TextToMD5(string text)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            //convert string to byte array
            byte[] inBytes = Encoding.UTF8.GetBytes(text);
            //create MD crypto object
            var md5 = new MD5CryptoServiceProvider();
            //encode bytes using MD5 crypto object
            byte[] outBytes = md5.ComputeHash(inBytes);
            //convert hash bytes to string
            var sb = new StringBuilder();
            foreach (var item in outBytes)
            {
                sb.Append(item.ToString("x2"));
            }
            sw.Stop();
            timeScores+= sw.Elapsed.ToString() + " | MD5 STRING";
            return sb.ToString();
        }

        public static string MD5File(string inFile)
        {
            //convert string to byte array
            Stopwatch sw = new Stopwatch();
            sw.Start();
            byte[] inBytes = File.ReadAllBytes(inFile);
            //create MD5 object
            var md5 = new MD5CryptoServiceProvider();
            //encode byte array
            byte[] outBytes = md5.ComputeHash(inBytes);
            //convert bytes to string
            var sb = new StringBuilder();
            foreach (var item in outBytes)
            {
                sb.Append(item.ToString("x2"));
            }
            sw.Stop();
            timeScores += sw.Elapsed.ToString() + " | MD5 FILE";
            return sb.ToString();
        }

        //SHA256
        public static string TextToSHA256(string text)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            //convert string to byte array
            byte[] inBytes = Encoding.UTF8.GetBytes(text);
            //create sha crypto object
            var sha = new SHA256CryptoServiceProvider();
            //encode bytes using sha crypto object
            byte[] outBytes = sha.ComputeHash(inBytes);
            //convert bytes to string
            var sb = new StringBuilder();
            foreach (var item in outBytes)
            {
                sb.Append(item.ToString("x2"));
            }
            sw.Stop();
            timeScores +=  sw.Elapsed.ToString() + " | SHA-256 STRING";
            return sb.ToString();
        }

        public static string ArturoHash(string inputString)
        {

            int key = 53;
            ulong modPrime = (ulong)(Math.Pow(10,9)+9);
            ulong hashSum = 0;

            for (int i = 0; i < inputString.Length; i++)
            {
                hashSum += (ulong)((inputString[i]-'a'+1)*Math.Pow(key, i)%modPrime);
                
            }

            return hashSum.ToString("X");
        }

        public static string SHA256File(string inFile)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            //convert string to byte array
            byte[] inBytes = File.ReadAllBytes(inFile);
            //cre ate SHA crypto object
            var sha = new SHA256CryptoServiceProvider();
            //compute hash
            byte[] outBytes = sha.ComputeHash(inBytes);
            //convert bytes to string
            var sb = new StringBuilder();
            foreach (var item in outBytes)
            {
                sb.Append(item.ToString("x2"));
            }
            sw.Stop();
            timeScores += sw.Elapsed.ToString() + " | SHA-256 FILE";
            return sb.ToString();
        }

        public static string TDESencodeString(string inText, string SecretKey)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            //convert text to byte array
            byte[] inBytes = Encoding.UTF8.GetBytes(inText);

            //criate encoder object
            var tdes = new TripleDESCryptoServiceProvider();

            //special array for encryption
            byte[] salt = { 12, 54, 75, 12, 222, 65, 78, 12 };

            //Add salt to key
            Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(SecretKey, salt, 50);

            //algorithm setting
            tdes.Key = rfc.GetBytes(16);
            tdes.IV = rfc.GetBytes(8);
            tdes.Mode = CipherMode.CBC;
            tdes.Padding = PaddingMode.PKCS7;

            //encoding
            ICryptoTransform transform = tdes.CreateEncryptor();
            byte[] outBytes = transform.TransformFinalBlock(inBytes, 0, inBytes.Length);
            sw.Stop();
            timeScores += sw.Elapsed.ToString() + " | TDES STRING";
            return Convert.ToBase64String(outBytes);
        }

        public static string TDESencodeFile(string inFile, string secretKey)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            byte[] inBytes = File.ReadAllBytes(inFile);
            var tdes = new TripleDESCryptoServiceProvider();

            //special array for encryption
            byte[] salt = { 12, 54, 75, 12, 222, 65, 78, 12 };

            //add salt to key
            Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(secretKey, salt, 50);

            //algorithm settings
            tdes.Key = rfc.GetBytes(16);
            tdes.IV = rfc.GetBytes(8);
            tdes.Mode = CipherMode.CBC;
            tdes.Padding = PaddingMode.PKCS7;
            ICryptoTransform transform = tdes.CreateEncryptor();
            byte[] outBytes = transform.TransformFinalBlock(inBytes, 0, inBytes.Length);
            sw.Stop();
            timeScores += sw.Elapsed.ToString() + " | TDES FILE";
            return Convert.ToBase64String(outBytes);

            ////encoded file
            //string outFile;

            ////path
            //string dir = Path.GetDirectoryName(inFile);

            //string outName = Path.GetFileNameWithoutExtension(inFile) + "_ENCODED" + Path.GetExtension(inFile);

            //outFile = dir + @"\" + outName;

            ////read file
            //FileStream inStream = new FileStream(inFile, FileMode.Open, FileAccess.Read);

            ////write file
            //FileStream outStream = new FileStream(outFile, FileMode.Create, FileAccess.Write);

            ////create encoder object
            //var tdes = new TripleDESCryptoServiceProvider();



            ////encode
            //ICryptoTransform transform = tdes.CreateEncryptor();

            ////write encoded data
            //CryptoStream cryptoStream = new CryptoStream(outStream, transform, CryptoStreamMode.Write);

            ////array size = stream size
            //byte[] outBytes = new byte[inStream.Length];

            ////read file to byte array
            //inStream.Read(outBytes, 0, outBytes.Length);

            ////copy data & write data to file
            //cryptoStream.Write(outBytes, 0, outBytes.Length);

            ////close all files/streams
            //cryptoStream.Close();
            //inStream.Close();
            //outStream.Close();

            //System.Windows.Forms.MessageBox.Show("Encoded");

        }

        private static string publicKey;
        private static string privateKey;

        public static void GenerateRSAkeys()
        {
            FileStream fStream;
            StreamWriter sWriter;

            //encryption object
            var rsa = new RSACryptoServiceProvider();

            //public key - for general use
            publicKey = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\MyPublicKey.xml";
            fStream = new FileStream(publicKey, FileMode.Create, FileAccess.Write);
            sWriter = new StreamWriter(fStream);
            sWriter.Write(rsa.ToXmlString(false));
            sWriter.Close();
            fStream.Close();

            //private key
            privateKey = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\MyPrivateKey.xml";
            fStream = new FileStream(privateKey, FileMode.Create, FileAccess.Write);
            sWriter = new StreamWriter(fStream);
            sWriter.Write(rsa.ToXmlString(true));
            sWriter.Close();
            fStream.Close();
        }

        public static string RSAencodeString(string inText)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            //convert string to byte array
            byte[] inBytes = Encoding.UTF8.GetBytes(inText);

            //create rsa encoding object
            var rsa = new RSACryptoServiceProvider();

            //import public key from file
            var sReader = new StreamReader(publicKey);
            string keyData = sReader.ReadToEnd();
            sReader.Close();

            //assign key to rsa object
            rsa.FromXmlString(keyData);

            //encode
            byte[] outBytes = rsa.Encrypt(inBytes, true);
            sw.Stop();
            timeScores += sw.Elapsed.ToString()+" | RSA STRING";
            return Convert.ToBase64String(outBytes);
        }
    }
}
