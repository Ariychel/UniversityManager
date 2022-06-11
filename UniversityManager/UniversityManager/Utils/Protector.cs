using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace UniversityManager.Utils
{
    public static class Protector
    {
        private static readonly byte[] _enctryptCode = { 9, 8, 7, 6, 5, 0, 1, 5, 6, 2, 1 };

        public static byte[] Protect(byte[] data)
        {
            try
            {
                return ProtectedData.Protect(data, _enctryptCode, DataProtectionScope.CurrentUser);
            }
            catch (CryptographicException)
            {
                return null;
            }
        }

        public static byte[] Unprotect(byte[] data)
        {
            try
            {
                return ProtectedData.Unprotect(data, _enctryptCode, DataProtectionScope.CurrentUser);
            }
            catch (CryptographicException)
            {
                return null;
            }
        }
    }
}
