using Domain.Shared.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace Infrastructure.Configuration
{
    public class Cryptography: ICryptography
    {
        private readonly RandomNumberGenerator _rng = RandomNumberGenerator.Create();

        public string HashPassword(string password)
        {
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            return HashPasswordInternal(password);
        }

        public bool VerifyHashedPassword(string hashedPassword, string password)
        {
            if (hashedPassword == null)
            {
                throw new ArgumentNullException("hashedPassword");
            }

            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            return VerifyHashedPasswordInternal(hashedPassword, password);
        }

        private string HashPasswordInternal(string password)
        {
            byte[] inArray = HashPasswordInternal(password, KeyDerivationPrf.HMACSHA256, 10000, 16, 32);
            return Convert.ToBase64String(inArray);
        }

        private byte[] HashPasswordInternal(string password, KeyDerivationPrf prf, int iterCount, int saltSize, int numBytesRequested)
        {
            byte[] array = new byte[saltSize];
            _rng.GetBytes(array);
            byte[] array2 = KeyDerivation.Pbkdf2(password, array, prf, iterCount, numBytesRequested);
            byte[] array3 = new byte[13 + array.Length + array2.Length];
            array3[0] = 1;
            WriteNetworkByteOrder(array3, 1, (uint)prf);
            WriteNetworkByteOrder(array3, 5, (uint)iterCount);
            WriteNetworkByteOrder(array3, 9, (uint)saltSize);
            Buffer.BlockCopy(array, 0, array3, 13, array.Length);
            Buffer.BlockCopy(array2, 0, array3, 13 + saltSize, array2.Length);
            return array3;
        }

        private bool VerifyHashedPasswordInternal(string hashedPassword, string password)
        {
            byte[] array = Convert.FromBase64String(hashedPassword);
            if (array.Length == 0)
            {
                return false;
            }

            try
            {
                if (array[0] != 1)
                {
                    return false;
                }

                KeyDerivationPrf prf = (KeyDerivationPrf)ReadNetworkByteOrder(array, 1);
                int iterationCount = (int)ReadNetworkByteOrder(array, 5);
                int num = (int)ReadNetworkByteOrder(array, 9);
                if (num < 16)
                {
                    return false;
                }

                byte[] array2 = new byte[num];
                Buffer.BlockCopy(array, 13, array2, 0, array2.Length);
                int num2 = array.Length - 13 - array2.Length;
                if (num2 < 16)
                {
                    return false;
                }

                byte[] array3 = new byte[num2];
                Buffer.BlockCopy(array, 13 + array2.Length, array3, 0, array3.Length);
                byte[] a = KeyDerivation.Pbkdf2(password, array2, prf, iterationCount, num2);
                return ByteArraysEqual(a, array3);
            }
            catch
            {
                return false;
            }
        }

        private uint ReadNetworkByteOrder(byte[] buffer, int offset)
        {
            return (uint)((buffer[offset] << 24) | (buffer[offset + 1] << 16) | (buffer[offset + 2] << 8) | buffer[offset + 3]);
        }

        private void WriteNetworkByteOrder(byte[] buffer, int offset, uint value)
        {
            buffer[offset] = (byte)(value >> 24);
            buffer[offset + 1] = (byte)(value >> 16);
            buffer[offset + 2] = (byte)(value >> 8);
            buffer[offset + 3] = (byte)value;
        }

        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        private bool ByteArraysEqual(byte[] a, byte[] b)
        {
            if (a == b)
            {
                return true;
            }

            if (a == null || b == null || a.Length != b.Length)
            {
                return false;
            }

            bool flag = true;
            for (int i = 0; i < a.Length; i++)
            {
                flag &= a[i] == b[i];
            }

            return flag;
        }
    }
}
