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
                string addr = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(addr) && addr[0] == '1' && enc.IsValid(addr))
                {
                    // A P2PKH is Base58 encoding of Hash160 hash of public key with a version byte in the beginning
                    byte[] hash160 = enc.DecodeWithCheckSum(addr);
                    if (hash160.Length != 21)
                    {
                        continue;
                    }

                    // A P2SH-P2WPKH address is the Hash160 hash of the "witness script".
                    // In this version the script is { witver || size || hash160_of_pubkey }
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
                    // The address is encoded using Base58 encoding with a 4 byte checksum 
                    // and the version byte is the same as P2SH addresses (=5)
                    Console.WriteLine(enc.EncodeWithCheckSum(result.AppendToBeginning(5)));

                    break;
                }
            }

            Console.ReadLine();
        }
    }
}
