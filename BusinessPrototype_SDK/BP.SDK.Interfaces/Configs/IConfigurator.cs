namespace BP.SDK.Interfaces.Configs
{
    /// <summary>
    /// Presents Configurator functionality interface
    /// 
    /// 2018/10/10 - Created, VTyagunov
    /// </summary>
    public interface IConfigurator
    {
        /// <summary>
        /// Full Path to config file
        /// </summary>
        string FullPath { get; }

        /// <summary>
        /// Use for Read Value from a config file
        /// </summary>
        /// <param name="configName">Name of the config string</param>
        /// <returns></returns>
        string ReadValue(string configName);

        /// <summary>
        /// Use for Write Value to a config file
        /// </summary>
        /// <param name="configName">Name of the config string</param>
        /// <param name="configValue">Value of the config string</param>
        void WriteValue(string configName, string configValue);
    }
}