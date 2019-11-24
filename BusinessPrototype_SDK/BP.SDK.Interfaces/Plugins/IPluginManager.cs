using System.Collections.Generic;

using BP.SDK.Base.Plugins;

namespace BP.SDK.Interfaces.Plugins
{
    /// <summary>
    /// Presents Plugin Manager functionality interface
    /// 
    /// 2018/09/21 - Created, VTyagunov
    /// </summary>
    public interface IPluginManager<T>
    {
        /// <summary>Collection of the plugins</summary>
        Dictionary<string, Plugin<T>> Plugins { get; }

        /// <summary>
        /// Use for load plugin dll name from client config xml
        /// </summary>
        /// <returns></returns>
        void LoadPlugins(string pluginsPath);
    }
}