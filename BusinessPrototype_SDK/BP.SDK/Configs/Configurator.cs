using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

using BP.SDK.Advanced;
using BP.SDK.Interfaces.Configs;
using BP.SDK.Log;
using BP.SDK.Properties;

namespace BP.SDK.Configs
{
    /// <summary>
    /// Implements Configurator functionality
    /// 
    /// 2018/10/10 - Created, VTyagunov
    /// </summary>
    public class Configurator : IConfigurator
    {
        #region Variables

        /// <summary>Path of the config file</summary>
        private string _configFilePath;
        /// <summary>Name of the config file</summary>
        private string _configFileName;
        /// <summary>Format of the config file</summary>
        private string _configFileFormat;

        #endregion

        #region Properties

        /// <summary>
        /// Full Path to config file
        /// </summary>
        public string FullPath
        {
            get
            {
                if (string.IsNullOrEmpty(_configFilePath))
                    throw new Exception("Configurator. FullPath. There is no config file path!");

                if (string.IsNullOrEmpty(_configFileName))
                    throw new Exception("Configurator. FullPath. There is no config file name!");

                if (string.IsNullOrEmpty(_configFileFormat))
                    throw new Exception("Configurator. FullPath. There is no config file format!");

                if (!_configFilePath.EndsWith("/") && !_configFilePath.EndsWith("\\") && !_configFilePath.EndsWith("\""))
                    _configFilePath += "\\";

                if (!Directory.Exists(_configFilePath))
                    DirectoryAdvanced.CreateDirectoryFullAcess(_configFilePath);

                return string.Concat(_configFilePath, _configFileName, _configFileFormat);
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor. It use path of the config file and file name by default
        /// </summary>
        public Configurator()
        {
            Logger.Log.Debug("Configurator. Ctr");

            var drive = Path.GetPathRoot(Environment.SystemDirectory);
            _configFilePath = string.Concat(drive, Resource.СonfigFilePathWithoutDrive);
            if (!_configFilePath.EndsWith("/") && !_configFilePath.EndsWith("\\") && !_configFilePath.EndsWith("\""))
                _configFilePath += "\\";

            _configFileName = Resource.СonfigFileName;
            _configFileFormat = Resource.ConfigFileFormat;

            CreateConfigFile();
        }

        /// <summary>
        /// Constructor. It use path of the config file from parameter and file name by default
        /// </summary>
        /// <param name="configFileName">Name of the config file</param>
        public Configurator(string configFileName)
        {
            Logger.Log.Debug("Configurator. Ctr");

            if (string.IsNullOrEmpty(configFileName))
                throw new ArgumentException("Configurator. Ctr. File name is empty!", configFileName);

            var regexItem = new Regex("^[a-zA-Z0-9]*$");
            if (!regexItem.IsMatch(configFileName))
                throw new ArgumentException("Config file name is can contains only 'a-z', 'A-Z' and '0-9' characters!");

            var drive = Path.GetPathRoot(Environment.SystemDirectory);
            _configFilePath = string.Concat(drive, Resource.СonfigFilePathWithoutDrive);
            if (!_configFilePath.EndsWith("/") && !_configFilePath.EndsWith("\\") && !_configFilePath.EndsWith("\""))
                _configFilePath += "\\";

            _configFileName = configFileName;
            _configFileFormat = Resource.ConfigFileFormat;

            CreateConfigFile();
        }

        /// <summary>
        /// Constructor. It use path of the config file from parameter and file name by default
        /// </summary>
        /// <param name="configFilePath">Path of the config file</param>
        /// <param name="configFileName">Name of the config file</param>
        public Configurator(string configFilePath, string configFileName)
        {
            Logger.Log.Debug("Configurator. Ctr");

            if (string.IsNullOrEmpty(configFilePath))
                throw new ArgumentException("CreateConfigFile. Ctr. File path is empty!", configFilePath);

            if (string.IsNullOrEmpty(configFileName))
                throw new ArgumentException("CreateConfigFile. Ctr. File name is empty!", configFileName);

            var regexItem = new Regex("^[a-zA-Z0-9]*$");
            if (!regexItem.IsMatch(configFileName))
                throw new ArgumentException("Config file name is can contains only 'a-z', 'A-Z' and '0-9' characters!");

            _configFilePath = configFilePath;
            if (!_configFilePath.EndsWith("/") && !_configFilePath.EndsWith("\\") && !_configFilePath.EndsWith("\""))
                _configFilePath += "\\";

            _configFileName = configFileName;
            _configFileFormat = Resource.ConfigFileFormat;

            CreateConfigFile();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Use for Read Value from a config file
        /// </summary>
        /// <param name="configName">Name of the config string</param>
        /// <returns></returns>
        public string ReadValue(string configName)
        {
            Logger.Log.Debug("Configurator. ReadString");

            try
            {
                var configFileExists = ConfigFileExists();
                if (!configFileExists)
                    CreateConfigFile();

                var xml = XDocument.Load(FullPath);
                var tag = xml.Descendants(Resource.ConfigSectionTag).FirstOrDefault();

                if (tag != null)
                {
                    var tagNode = tag.Descendants(configName).FirstOrDefault();
                    if (tagNode != null)
                    {
                        var tagValue = tagNode.Value;
                        return tagValue;
                    }
                }

                Logger.Log.WarnFormat("Configurator. ReadString. There is no tag with name '{0}' in the config file: '{1}'",
                    configName, FullPath);
                return string.Empty;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Use for Write Value to a config file
        /// </summary>
        /// <param name="configName">Name of the config string</param>
        /// <param name="configValue">Value of the config string</param>
        public void WriteValue(string configName, string configValue)
        {
            Logger.Log.Debug("Configurator. WriteString");

            try
            {
                var configFileExists = ConfigFileExists();
                if (!configFileExists)
                    CreateConfigFile();

                var xml = XDocument.Load(FullPath);
                var configNameElement = xml.Element(Resource.ConfigSectionTag).Element(configName);
                if(configNameElement != null)
                    configNameElement.Value = configValue;
                else xml.Element(Resource.ConfigSectionTag).Add(new XElement(configName, configValue));
                xml.Save(FullPath);

                Logger.Log.InfoFormat("Configurator. WriteValue. New value '{0}' was written in the '{1}' tag",
                        configValue, configName);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Use for Create Config File
        /// </summary>
        private void CreateConfigFile()
        {
            Logger.Log.Debug("Configurator. CreateConfigFile");

            var configFileExists = ConfigFileExists();
            if (configFileExists)
            {
                Logger.Log.WarnFormat("Configurator. CreateConfigFile. The config file '{0}' is already exists!",
                    FullPath);
                return;
            }

            try
            {
                if (!File.Exists(FullPath))
                {
                    using (var fs = FileAdvanced.CreateFileFullAcess(FullPath))
                    {
                        var configFile = new XDocument(new XElement(Resource.ConfigSectionTag, string.Empty));
                        configFile.Save(fs);

                        Logger.Log.InfoFormat("Configurator. CreateConfigFile. The config file '{0}' was created.",
                            FullPath);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Use for Check Config File Exists
        /// </summary>
        /// <returns></returns>
        private bool ConfigFileExists()
        {
            Logger.Log.Debug("Configurator. ConfigFileExists");

            try
            {
                return File.Exists(FullPath);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}