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
             ulong[] k = {
    0xd76aa478, 0xe8c7b756, 0x242070db, 0xc1bdceee ,
    0xf57c0faf, 0x4787c62a, 0xa8304613, 0xfd469501 ,
    0x698098d8, 0x8b44f7af, 0xffff5bb1, 0x895cd7be ,
    0x6b901122, 0xfd987193, 0xa679438e, 0x49b40821 ,
    0xf61e2562, 0xc040b340, 0x265e5a51, 0xe9b6c7aa ,
    0xd62f105d, 0x02441453, 0xd8a1e681, 0xe7d3fbc8 ,
    0x21e1cde6, 0xc33707d6, 0xf4d50d87, 0x455a14ed ,
    0xa9e3e905, 0xfcefa3f8, 0x676f02d9, 0x8d2a4c8a ,
    0xfffa3942, 0x8771f681, 0x6d9d6122, 0xfde5380c ,
    0xa4beea44, 0x4bdecfa9, 0xf6bb4b60, 0xbebfbc70 ,
    0x289b7ec6, 0xeaa127fa, 0xd4ef3085, 0x04881d05 ,
    0xd9d4d039, 0xe6db99e5, 0x1fa27cf8, 0xc4ac5665 ,
    0xf4292244, 0x432aff97, 0xab9423a7, 0xfc93a039 ,
    0x655b59c3, 0x8f0ccc92, 0xffeff47d, 0x85845dd1 ,
    0x6fa87e4f, 0xfe2ce6e0, 0xa3014314, 0x4e0811a1 ,
    0xf7537e82, 0xbd3af235, 0x2ad7d2bb, 0xeb86d391
};
            Stopwatch sw = new Stopwatch();
            sw.Start();
            int[] keys = { 3, 5, 7, 11, 13, 17, 19, 23 };
            ulong sum = 0x54e55;
            for (int i = 0; i < inputString.Length; i++)
            {
                sum ^= ((sum << keys[i % 8]) + inputString[i] + (sum >> keys[(i+1) % 8] ));
            }
            sum = ((sum >> 8) ^ sum) * k[0];
            sum = ((sum << 8) ^ sum) * k[1];
            sum = (sum >> 8) ^ sum;
            string result = "";
            for (int i = 0; i < 4; i++)
            {
                result += (sum * k[i]).ToString("x");
            }
            int j = 0;
            while (result.Length<64)
            {
                result += (sum * k[j % 64]).ToString("x");
            }
            while (result.Length > 64)
            {
                result = result.Substring(0, result.Length - 1);
            }
            sw.Stop();
            timeScores += sw.Elapsed.ToString() + " | ARTURO STRING";
            return result;
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
