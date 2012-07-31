using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Facebook
{
    /// <summary>Provides extended functionality for <see cref="MD5" /> objects.</summary>
    public static class MD5Extensions
    {
        /// <summary>Computes the hash value for the specified <see cref="String" /> <paramref name="input"/> using <see cref="Encoding.UTF8" />.</summary>
        /// <param name="md5">The <see cref="MD5" /> instance that will perform the hash computation.</param>
        /// <param name="input">A <see cref="String" /> value.</param>
        /// <returns>A <see cref="String" /> containing the computed hash of <paramref name="input"/>.</returns>
        public static String ComputeHashString(this MD5 md5, String input)
        {
            return MD5Extensions.ComputeHashString(md5, input, Encoding.UTF8);
        }

        /// <summary>Computes the hash value for the specified <see cref="String" /> <paramref name="input"/>.</summary>
        /// <param name="md5">The <see cref="MD5" /> instance that will perform the hash computation.</param>
        /// <param name="input">A <see cref="String" /> value.</param>
        /// <param name="encoding">An <see cref="Encoding" /> object that will be used to encode <paramref name="input" /> to bytes.</param>
        /// <returns>A <see cref="String" /> containing the computed hash of <paramref name="input"/>.</returns>
        public static String ComputeHashString(this MD5 md5, String input, Encoding encoding)
        {            
            Byte[] inputBytes = encoding.GetBytes(input);
            var hashBuilder = new StringBuilder();
            List<Byte> hashBytes = md5.ComputeHash(inputBytes).ToList();
            hashBytes.ForEach(hashByte => hashBuilder.AppendFormat("{0:x2}", hashByte));
            return hashBuilder.ToString();            
        }
    }
}
