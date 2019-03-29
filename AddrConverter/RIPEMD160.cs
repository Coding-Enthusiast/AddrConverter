using System.Security.Cryptography;

namespace AddrConverter
{
    public abstract class RIPEMD160 : HashAlgorithm
    {
        protected RIPEMD160()
        {
            HashSizeValue = 160;
        }

        new static public RIPEMD160 Create()
        {
            return Create("System.Security.Cryptography.RIPEMD160");
        }

        new static public RIPEMD160 Create(string hashName)
        {
            return (RIPEMD160)CryptoConfig.CreateFromName(hashName);
        }
    }
}
