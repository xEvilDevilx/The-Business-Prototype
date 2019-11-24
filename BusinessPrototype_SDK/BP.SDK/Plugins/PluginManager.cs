using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

using BP.SDK.Base.Plugins;
using BP.SDK.Interfaces.Plugins;
using BP.SDK.Log;

namespace BP.SDK.Plugins
{
    /// <summary>
    /// Implements Plugin Manager functionality
    /// 
    /// 2018/09/21 - Created, VTyagunov
    /// </summary>
    public class PluginManager<T> : IPluginManager<T>, IDisposable
    {
        #region Properties

        /// <summary>Collection of the plugins</summary>
        public Dictionary<string, Plugin<T>> Plugins { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public PluginManager()
        {
            Logger.Log.Debug("PluginManager. Ctr. Enter");

            Plugins = new Dictionary<string, Plugin<T>>();

            Logger.Log.Debug("PluginManager. Ctr. Exit");
        }

        #endregion

        #region Methods

        #region IDisposable

        /// <summary>
        /// Use for Dispose current object
        /// </summary>
        public void Dispose()
        {
            foreach (var plugin in Plugins)
                ((IDisposable)plugin.Value.PluginData).Dispose();

            Plugins.Clear();
        }

        #endregion

        /// <summary>
        /// Use for load Plugin<T> object from plugin dll
        /// </summary>
        /// <param name="filePath">File path</param>
        private Plugin<T> LoadAssembly(string filePath)
        {
            Logger.Log.Debug("LoadAssemblies. Enter");

            try
            {
                Module[] modules = Assembly.LoadFrom(filePath).GetModules();
                foreach (var module in modules)
                {
                    Type[] types = module.GetTypes();
                    foreach (var type in types)
                    {
                        var typeInterface = type.GetInterface(typeof(T).FullName);
                        if (typeInterface == null)
                            continue;

                        string fullName = type.FullName;

                        T pluginInst = (T)Activator.CreateInstance(type);

                        if (pluginInst == null)
                            continue;

                        var plugin = new Plugin<T>()
                        {
                            PluginName = type.FullName,
                            PluginFileName = type.Module.Name,
                            IsActive = false,
                            PluginData = pluginInst
                        };

                        return plugin;
                    }
                }

                Logger.Log.Debug("LoadAssemblies. Exit");
                return null;
            }
            catch (Exception ex)
            { 
                Logger.Log.Error("LoadAssemblies. ", ex);
                throw;
            }
        }

        /// <summary>
        /// Use for load plugin dll name from client config xml
        /// </summary>
        /// <param name="pluginsPath">Path of plugins</param>
        public void LoadPlugins(string pluginsPath)
        {
            Logger.Log.Debug("LoadPlugins. Enter");

            try
            {
                if (pluginsPath[pluginsPath.Length - 1] != '/' || pluginsPath[pluginsPath.Length - 1] != '\\')
                    pluginsPath += "/";

                var pluginNames = Directory.GetFiles(pluginsPath);
                foreach (var pluginName in pluginNames)
                {
                    if (!pluginName.EndsWith(".dll"))
                        continue;

                    var plugin = LoadAssembly(pluginName);
                    if(plugin != null)
                        Plugins.Add(plugin.PluginFileName, plugin);
                }

                Logger.Log.Debug("LoadPlugins. Exit");
            }
            catch (Exception ex)
            {
                Logger.Log.Error("LoadPlugins. ", ex);
                throw;
            }
        }

        #endregion
    }
}