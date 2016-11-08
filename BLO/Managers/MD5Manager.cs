using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace BLO.Managers
{
    public class MD5Manager
    {
        private MD5 md5;
        public MD5Manager()
        {
            md5 = MD5.Create();
        }

        public string CalculateChecksum(string file)
        {
            using (FileStream stream = File.OpenRead(file))
            {
                if (stream.Length == 0)
                    throw new Exception("A corrupted file (zero byte) has found");
                byte[] checksum = md5.ComputeHash(stream);
                return (BitConverter.ToString(checksum).Replace("-", string.Empty)).ToLower();
            } // End of using fileStream
        } // End of CalculateChecksum 

        public string GetMD5HashFromFile(string FileName)
        {
            return CalculateChecksum(FileName);
        }

        public string GetMd5Hash(string input)
        {
            // Convert the input string to a byte array and compute the hash. 
            byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes 
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data  
            // and format each one as a hexadecimal string. 
            for (int i = 0; i < data.Length; i++)
                sBuilder.Append(data[i].ToString("x2"));

            // Return the hexadecimal string. 
            return sBuilder.ToString();
        }

        // Verify a hash against a string. 
        public bool VerifyMd5Hash(string input, string hash)
        {
            // Hash the input. 
            string hashOfInput = GetMd5Hash(input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            return 0 == comparer.Compare(hashOfInput, hash);
        }
    }
}
