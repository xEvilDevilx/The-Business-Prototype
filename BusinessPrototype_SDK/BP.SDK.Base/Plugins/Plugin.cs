using System;

namespace BP.SDK.Base.Plugins
{
    /// <summary>
    /// Presents Plugin object
    /// </summary>
    /// <typeparam name="T">Type of the Plugin</typeparam>
    public class Plugin<T> : IDisposable
    {
        /// <summary>Name of the Plugin</summary>
        public string PluginName { get; set; }
        /// <summary>Name of the Plugin File</summary>
        public string PluginFileName { get; set; }
        /// <summary>Data of the Plugin</summary>
        public T PluginData { get; set; }
        /// <summary>Status of the plugin activity</summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Use for Dispose Plugin object
        /// </summary>
        public void Dispose()
        {
            PluginName = string.Empty;
            PluginFileName = string.Empty;
            if (PluginData is IDisposable)
                ((IDisposable)PluginData).Dispose();
            PluginData = default(T);
            IsActive = false;
        }
    }
}