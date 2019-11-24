namespace BP.SDK.Interfaces.Serialize
{
    /// <summary>
    /// Presents Encipher Functionality interface
    /// 
    /// 2017/07/13 - Created, VTyagunov
    /// </summary>
    public interface IEncipher
    {
        /// <summary>
        /// Use for Encode object
        /// </summary>
        /// <param name="obj">Object for encode</param>
        /// <returns>Hex-code string</returns>
        string Encode(object obj);

        /// <summary>
        /// Use for Decode object
        /// </summary>
        /// <typeparam name="T">Object Type</typeparam>
        /// <param name="hexString">Encoded Hex-code string</param>
        /// <returns>Decoded T object</returns>
        object Decode(string hexString);

        /// <summary>
        /// Use for convert Hex-code string to Bytes Array
        /// </summary>
        /// <param name="hex">Hex-code string</param>
        /// <returns>Converted bytes array</returns>
        byte[] HexStringToByteArray(string hex);

        /// <summary>
        /// Use for convert Bytes Array to Hex-code string
        /// </summary>
        /// <param name="bytesArray">Bytes array</param>
        /// <returns>Converted Hex-code string</returns>
        string ByteArrayToHexString(byte[] bytesArray);

        /// <summary>
        /// Use for serialize object to bytes array
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="t">object</param>
        /// <returns>Serialized bytes array</returns>
        byte[] Serialize<T>(T t);

        /// <summary>
        /// Use for Deserialize object from bytes array
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="input">Bytes array</param>
        /// <returns>Deserialized object</returns>
        object Deserialize(byte[] bytesArray);
    }
}