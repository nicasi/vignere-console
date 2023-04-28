using System.IO;
using System.Security.Cryptography;

namespace Vignere
{
    internal class Program
    {
        private static byte[]? fileByteArray;
        static void Main(string[] args)
        {
            if(args.Length == 0)
            {
                Console.WriteLine("No arguments given, please provide file name.");
            }
            else
            {
                string filePath = args[0];
                try
                {
                    fileByteArray = File.ReadAllBytes(filePath);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }

                Console.WriteLine("Read file succesfully.");

                (byte[] cipherArr, byte[] key) encrypted = encrypt(fileByteArray);

                File.WriteAllBytes("cipher", encrypted.cipherArr);
                File.WriteAllBytes("key", encrypted.key);
            }
            
        }

        static (byte[] cipherArray, byte[] key) encrypt(byte[] fileByteArray)
        {
            RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();

            byte[] keyArray = (byte[])fileByteArray.Clone();
            byte[] cipherArray = new byte[fileByteArray.Length];

            rngCsp.GetBytes(keyArray);

            for(int i = 0; i < fileByteArray.Length; i++)
            {
                cipherArray[i] = (byte)(fileByteArray[i] ^ keyArray[i]);
            }

            return (cipherArray, keyArray);
        }
    }
}