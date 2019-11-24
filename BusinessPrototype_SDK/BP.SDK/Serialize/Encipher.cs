using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

using BP.SDK.Interfaces.Serialize;

namespace BP.SDK.Serialize
{
    /// <summary>
    /// Presents Encipher Functionality
    /// 
    /// 2017/07/13 - Created, VTyagunov
    /// </summary>
    public class Encipher : IEncipher
    {
        #region Variables

        /// <summary>Encription Key</summary>
        private byte[] _keyBytes;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="keyBytes"></param>
        public Encipher(byte[] keyBytes)
        {
            if ((keyBytes == null) || (keyBytes.Length < 1))
                throw new ArgumentNullException("keyBytes", "Parameter \"keyBytes\" is null or empty");

            _keyBytes = keyBytes;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Use for Encode object
        /// </summary>
        /// <param name="obj">Object for encode</param>
        /// <returns>Hex-code string</returns>
        public string Encode(object obj)
        {
            if (obj == null)
            {
                // Log... Parameter \"obj\" is null
                return string.Empty;
            }

            try
            {
                var nonEcriptedBytes = Serialize(obj);
                var encriptedBytes = XOREncipher(nonEcriptedBytes);
                var objString = ByteArrayToHexString(encriptedBytes);

                return objString;
            }
            catch (Exception ex)
            {
                // Log...
                return null;
            }
        }

        /// <summary>
        /// Use for Decode object
        /// </summary>
        /// <typeparam name="T">Object Type</typeparam>
        /// <param name="hexString">Encoded Hex-code string</param>
        /// <returns>Decoded T object</returns>
        public object Decode(string hexString)
        {
            if (string.IsNullOrEmpty(hexString))
            {
                // Log... Parameter \"hexString\" is null or empty
                return null;
            }

            try
            {
                var nonEncriptedBytes = HexStringToByteArray(hexString);
                var encriptedBytes = XOREncipher(nonEncriptedBytes);
                object obj = Deserialize(encriptedBytes);

                return obj;
            }
            catch (Exception ex)
            {
                // Log...
                return null;
            }
        }

        /// <summary>
        /// Use for Encipher bytes array
        /// </summary>
        /// <param name="data">Data bytes array</param>
        /// <returns></returns>
        private byte[] XOREncipher(byte[] data)
        {
            if (data == null)
            {
                // Log... Parameter \"data\" is null
                return null;
            }

            try
            {
                int processedByteOfPass = 0;
                var bytesArray = new byte[data.Length];

                for (int i = 0; i < data.Length; i++)
                {
                    bytesArray[i] = (byte)(data[i] ^ _keyBytes[processedByteOfPass]);

                    processedByteOfPass++;
                    if (processedByteOfPass >= _keyBytes.Length)
                        processedByteOfPass = 0;
                }

                return bytesArray;
            }
            catch (Exception ex)
            {
                // Log...
                return null;
            }
        }

        /// <summary>
        /// Use for convert Hex-code string to Bytes Array
        /// </summary>
        /// <param name="hex">Hex-code string</param>
        /// <returns>Converted bytes array</returns>
        public byte[] HexStringToByteArray(string hex)
        {
            if (string.IsNullOrEmpty(hex))
            {
                // Log... Parameter \"hex\" is null or empty
                return null;
            }

            try
            {
                return Enumerable.Range(0, hex.Length / 2)
                    .Select(x => Convert.ToByte(hex.Substring(x * 2, 2), 16))
                    .ToArray();
            }
            catch (Exception ex)
            {
                // Log...
                return null;
            }
        }

        /// <summary>
        /// Use for convert Bytes Array to Hex-code string
        /// </summary>
        /// <param name="bytesArray">Bytes array</param>
        /// <returns>Converted Hex-code string</returns>
        public string ByteArrayToHexString(byte[] bytesArray)
        {
            if (bytesArray == null)
            {
                // Log... Parameter \"bytesArray\" is null
                return string.Empty;
            }

            try
            {
                var hexCodeString = new string(bytesArray.SelectMany(
                    x => x.ToString("X2").ToCharArray()).ToArray());
                return hexCodeString;
            }
            catch (Exception ex)
            {
                // Log...
                return null;
            }
        }

        /// <summary>
        /// Use for serialize object to bytes array
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="t">object</param>
        /// <returns>Serialized bytes array</returns>
        public byte[] Serialize<T>(T t)
        {
            if (t == null)
            {
                // Log... Parameter \"t\" is null
                return null;
            }

            try
            {
                using (var stream = new MemoryStream())
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(stream, t);
                    return stream.ToArray();
                }
            }
            catch (Exception ex)
            {
                // Log...
                return null;
            }
        }

        /// <summary>
        /// Use for Deserialize object from bytes array
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="input">Bytes array</param>
        /// <returns>Deserialized object</returns>
        public object Deserialize(byte[] bytesArray)
        {
            if (bytesArray == null)
            {
                // Log... Parameter \"bytesArray\" is null
                return null;
            }

            try
            {
                using (var stream = new MemoryStream(bytesArray))
                {
                    var formatter = new BinaryFormatter();
                    return formatter.Deserialize(stream);
                }
            }
            catch (Exception ex)
            {
                // Log...
                return null;
            }
        }

        #endregion
    }
}