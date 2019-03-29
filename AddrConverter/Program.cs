using System;
using System.Security.Cryptography;

namespace AddrConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            Base58 enc = new Base58();
            while (true)
            {
                Console.WriteLine("Please enter your P2PKH address (should start with 1):");
                string temp = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(temp) && temp[0] == '1' && enc.IsValid(temp))
                {
                    byte[] hash160 = enc.DecodeWithCheckSum(temp);
                    if (hash160.Length != 21)
                    {
                        continue;
                    }
                    byte[] witnessScript = new byte[] { 0x00, 0x14 }.ConcatFast(hash160.SubArray(1, 20));
                    byte[] result;
                    using (RIPEMD160 rip = new RIPEMD160Managed())
                    {
                        using (SHA256 sha = SHA256.Create())
                        {
                            result = rip.ComputeHash(sha.ComputeHash(witnessScript));
                        }
                    }

                    Console.WriteLine("Your P2SH-P2WPKH address is:");
                    Console.WriteLine(enc.EncodeWithCheckSum(result.AppendToBeginning(5)));

                    break;
                }
            }

            Console.ReadLine();
        }
    }
}
