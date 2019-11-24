using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;

using BP.SDK.Common;
using BP.SDK.Interfaces.Common;
using BP.SDK.Interfaces.Serialize;
using BP.SDK.Log;

namespace BP.SDK.Serialize
{
    /// <summary>
    /// Implements IO File Manager functionality
    /// 
    /// 2017/08/03 - Created, VTyagunov
    /// </summary>
    public class FileManager : IFileManager
    {
        #region Variables

        /// <summary>Flag for IDisposable pattern</summary>
        private bool _disposed = false;
        /// <summary>Object for work with Properties</summary>
        private IPropertyUtils _objectProperties;
        /// <summary>Encipher for encode/decode data</summary>
        private IEncipher _encipher;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public FileManager()
        {
            var keyBytes = new byte[5] { 222, 111, 164, 225, 45 };
            _objectProperties = new PropertyUtils();
            _encipher = new Encipher(keyBytes);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public FileManager(byte[] keyBytes)
        {
            if (keyBytes == null)
                throw new ArgumentNullException("keyBytes");

            _objectProperties = new PropertyUtils();
            _encipher = new Encipher(keyBytes);
        }

        #endregion

        #region Methods

        #region IDisposable implementation

        /// <summary>
        /// Implements IDisposable interface
        /// </summary>
        public void Dispose()
        {
            Logger.Log.Debug("FileManager. Dispose");

            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Implements IDisposable interface
        /// </summary>
        /// <param name="disposing">Flag shows is need dispose or not need</param>
        protected virtual void Dispose(bool disposing)
        {
            Logger.Log.Debug("FileManager. Dispose");

            if (!_disposed)
            {
                if (disposing)
                {

                }

                _disposed = true;
            }
        }

        #endregion

        #region Write/Read File Data

        /// <summary>
        /// Use for Write Data to file
        /// </summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="t">Data object</param>
        /// <param name="filePath">File path with file name for save</param>
        public void WriteData<T>(T t, string filePath)
        {
            if (t == null)
                throw new ArgumentNullException("t");

            try
            {
                using (var TempFileStream = File.Create(filePath))
                {
                    var serializer = new BinaryFormatter();
                    serializer.Serialize(TempFileStream, t);
                }
            }
            catch (Exception ex)
            {
                // Log...
            }
        }

        /// <summary>
        /// Use for Read Data from file
        /// </summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="filePath">File path with file name</param>
        /// <returns></returns>
        public T ReadData<T>(string filePath) where T : new()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    using (var TempFileStream = File.OpenRead(filePath))
                    {
                        var deserializer = new BinaryFormatter();
                        T t = (T)deserializer.Deserialize(TempFileStream);

                        return t;
                    }
                }
                return default(T);
            }
            catch (Exception ex)
            {
                // Log...        
                return default(T);
            }
        }

        /// <summary>
        /// Use for Write encoded Data to file
        /// </summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="t">Data object</param>
        /// <param name="fileName">File path for save</param>
        public void WriteDataEncipher<T>(T t, string filePath)
        {
            if (t == null)
                throw new ArgumentNullException("t");

            try
            {
                var encodedDataList = new List<string>();
                var orderedTypeProperties = _objectProperties.GetOrderedProperties(t);

                foreach (PropertyInfo property in orderedTypeProperties)
                {
                    var propertyValue = _objectProperties.GetPropertyValue(t, property.Name);
                    var encoded = _encipher.Encode(propertyValue);
                    encodedDataList.Add(encoded);
                }

                using (var TempFileStream = File.Create(filePath))
                {
                    var serializer = new BinaryFormatter();
                    serializer.Serialize(TempFileStream, encodedDataList);
                }
            }
            catch (Exception ex)
            {
                // Log...
            }
        }

        /// <summary>
        /// Use for Read Data from file
        /// </summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="t">Data object</param>
        /// <param name="fileName">File name</param>
        /// <returns></returns>
        public T ReadDataEncipher<T>(string filePath) where T : new()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    using (Stream TempFileStream = File.OpenRead(filePath))
                    {
                        T t = new T();
                        var deserializer = new BinaryFormatter();
                        var encodedDataList = (List<string>)deserializer.Deserialize(TempFileStream);
                        var orderedTypeProperties = _objectProperties.GetOrderedProperties(t);
                        int counter = 0;

                        foreach (PropertyInfo property in orderedTypeProperties)
                        {
                            var decoded = _encipher.Decode(encodedDataList[counter]);
                            counter++;
                            _objectProperties.SetPropertyValue(t, property.Name, decoded);
                        }
                        return t;
                    }
                }
                return default(T);
            }
            catch (Exception ex)
            {
                // Log...        
                return default(T);
            }
        }

        #endregion

        #region Write/Read Xml

        /// <summary>
        /// Use for Write Xml to file
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="t">Object</param>
        /// <param name="filePath">File path</param>
        public void WriteToXmlFile<T>(T t, string filePath)
        {
            if (t == null)
                throw new ArgumentNullException("t");

            try
            {
                var formatter = new XmlSerializer(typeof(T));
                using (var fs = new FileStream(filePath, FileMode.Create))
                    formatter.Serialize(fs, t);
            }
            catch (Exception ex)
            {
                // Log...
            }
        }

        /// <summary>
        /// Use for Read data from Xml file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public T ReadFromXmlFile<T>(string filePath)
        {
            try
            {
                var formatter = new XmlSerializer(typeof(T));
                using (var fs = new FileStream(filePath, FileMode.OpenOrCreate))
                {
                    var obj = (T)formatter.Deserialize(fs);
                    return obj;
                }
            }
            catch (Exception ex)
            {
                // Log...
                return default(T);
            }
        }

        /// <summary>
        /// Use for read Xml file to XmlDocument object
        /// </summary>
        /// <param name="filePath">File Path</param>
        /// <returns></returns>
        public XmlDocument ReadXmlDocument(string filePath)
        {
            try
            {
                using (var fs = new FileStream(filePath, FileMode.OpenOrCreate))
                {
                    var xmlDocument = new XmlDocument();
                    xmlDocument.Load(fs);
                    return xmlDocument;
                }
            }
            catch (Exception ex)
            {
                // Log...
                return null;
            }
        }

        #endregion

        #endregion
    }
}