using System;
using System.Xml;

namespace BP.SDK.Interfaces.Serialize
{
    /// <summary>
    /// Presents IO File Manager functionality interface
    /// 
    /// 2017/08/03 - Created, VTyagunov
    /// </summary>
    public interface IFileManager : IDisposable
    {
        /// <summary>
        /// Use for Write Data to file
        /// </summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="t">Data object</param>
        /// <param name="filePath">File path with file name for save</param>
        void WriteData<T>(T t, string filePath);

        /// <summary>
        /// Use for Read Data from file
        /// </summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="filePath">File path with file name</param>
        /// <returns></returns>
        T ReadData<T>(string filePath) where T : new();

        /// <summary>
        /// Use for Write encoded Data to file
        /// </summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="t">Data object</param>
        /// <param name="filePath">File path with file name for save</param>
        void WriteDataEncipher<T>(T t, string filePath);

        /// <summary>
        /// Use for Read Data from file
        /// </summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="filePath">File path with file name</param>
        /// <returns></returns>
        T ReadDataEncipher<T>(string filePath) where T : new();

        /// <summary>
        /// Use for Write Xml to file
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="t">Object</param>
        /// <param name="filePath">File path with file name for save</param>
        void WriteToXmlFile<T>(T t, string filePath);

        /// <summary>
        /// Use for Read data from Xml file
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="filePath">File path with file name</param>
        /// <returns></returns>
        T ReadFromXmlFile<T>(string filePath);

        /// <summary>
        /// Use for read Xml file to XmlDocument object
        /// </summary>
        /// <param name="filePath">File Path with file name</param>
        /// <returns></returns>
        XmlDocument ReadXmlDocument(string filePath);
    }
}