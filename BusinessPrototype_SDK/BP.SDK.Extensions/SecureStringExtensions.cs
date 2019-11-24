using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;

using BP.SDK.Log;

namespace BP.SDK.Extensions
{
    /// <summary>
    /// Implements SecureString Extensions functionality
    /// 
    /// 2018/11/04 - Created, VTyagunov
    /// </summary>
    public static class SecureStringExtensions
    {
        /// <summary>
        /// Use for convert SecureString object to Hash string
        /// </summary>
        /// <param name="secureString">Secure String</param>
        /// <returns></returns>
        public static string SecureStringToHash(this SecureString secureString)
        {
            Logger.Log.Debug("SecureStringExtensions. SecureStringToHash");

            var buffer = new byte[secureString.Length * 2];
            IntPtr ptr = Marshal.SecureStringToGlobalAllocUnicode(secureString);

            try
            {
                ptr = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                Marshal.Copy(ptr, buffer, 0, buffer.Length);
                using (var sha256 = SHA256.Create())
                {
                    byte[] hash = sha256.ComputeHash(buffer);
                    string hashStr = Convert.ToBase64String(hash);
                    return hashStr;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Array.Clear(buffer, 0, buffer.Length);
                Marshal.ZeroFreeGlobalAllocUnicode(ptr);
            }
        }
    }
}