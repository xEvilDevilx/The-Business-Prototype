using System;
using System.IO;
using System.ServiceModel;
using System.Threading.Tasks;

using BP.DataService.Base;
using BP.DataService.Common;
using BP.DataService.Interfaces;
using BP.DataService.WinService.Interfaces;
using BP.SDK.Base.Enums;
using BP.SDK.Interfaces.Plugins;
using BP.SDK.Log;
using BP.SDK.Plugins;

namespace BP.DataService.WinService.Contracts
{
    /// <summary>
    /// Presents Data Service Contract functionality
    /// 
    /// 2018/09/17 - Created, VTyagunov
    /// </summary>
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, IncludeExceptionDetailInFaults = true,
        InstanceContextMode = InstanceContextMode.Single)]
    public class DataServiceContract : DataServiceHostPlugin<IDataServiceContract>, IDataServiceContract, IDisposable
    {
        #region Variables

        /// <summary>Plugin Manager</summary>
        private IPluginManager<IDataServiceHostPlugin> _pluginManager;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public DataServiceContract()
        {
            Logger.Log.Debug("DataServiceContract. Ctr");

            _pluginManager = new PluginManager<IDataServiceHostPlugin>();
            var currentPath = Directory.GetCurrentDirectory();
            var pluginsPath = $"{currentPath}/Plugins";
            _pluginManager.LoadPlugins(pluginsPath);
        }

        #endregion

        #region Methods

        #region IDisposable

        /// <summary>
        /// Use for Dispose current object
        /// </summary>
        public void Dispose()
        {
            if (_pluginManager is IDisposable)
                ((IDisposable)_pluginManager).Dispose();

            _pluginManager.Plugins.Clear();
        }

        #endregion

        /// <summary>
        /// Use for Start Plugin by plugin name
        /// </summary>
        /// <param name="pluginName">Name of the Plugin</param>
        /// <returns></returns>
        public async Task<DataContractQueryResult> StartPlugin(string pluginName)
        {
            return await Task.Factory.StartNew(() =>
            {
                string message = string.Empty;

                try
                {
                    Logger.Log.DebugFormat("DataServiceContract. StartPlugin. Plugin name: {0}", pluginName);

                    var pluginExists = CheckPluginExists(pluginName);
                    if (pluginExists.ResultState == ResultStateTypes.Failed)
                        return pluginExists;
                    
                    if (!_pluginManager.Plugins[pluginName].IsActive)
                    {
                        _pluginManager.Plugins[pluginName].PluginData.Start(ConnectionSettings);
                        _pluginManager.Plugins[pluginName].IsActive = true;
                        Logger.Log.InfoFormat("DataServiceContract. StartPlugin. {0} started.", pluginName);
                    }
                    else
                    {
                        message = $"Plugin {pluginName} already started!";
                        Logger.Log.InfoFormat("DataServiceContract. StartPlugin. {0}", message);
                    }

                    // System.Threading.Thread.Sleep(5000); -- uncomment if query will execute not in time
                    return new DataContractQueryResult(ResultStateTypes.Success, -1, message);
                }
                catch(Exception ex)
                {                    
                    message = $"Error occured in Start {pluginName} plugin!";
                    Logger.Log.ErrorFormat("DataServiceContract. StartPlugin. Details: {0}. {1}. Stacktrace: {2}",
                        message, ex.Message, ex.StackTrace);
                    return new DataContractQueryResult(ResultStateTypes.Failed, -1, message);
                }
            });
        }
        
        /// <summary>
        /// Use for Stop Plugin by plugin name
        /// </summary>
        /// <param name="pluginName">Name of the Plugin</param>
        /// <returns></returns>
        public async Task<DataContractQueryResult> StopPlugin(string pluginName)
        {            
            return await Task.Factory.StartNew(() =>
            {
                string message = string.Empty;

                try
                {
                    Logger.Log.DebugFormat("DataServiceContract. StopPlugin. Plugin name: {0}", pluginName);

                    var pluginExists = CheckPluginExists(pluginName);
                    if (pluginExists.ResultState == ResultStateTypes.Failed)
                        return pluginExists;

                    if (_pluginManager.Plugins[pluginName].IsActive)
                    {
                        _pluginManager.Plugins[pluginName].PluginData.Stop();
                        _pluginManager.Plugins[pluginName].IsActive = false;
                        Logger.Log.InfoFormat("DataServiceContract. StopPlugin. {0} stoped.", pluginName);
                    }
                    else
                    {
                        message = $"Plugin {pluginName} already stoped!";
                        Logger.Log.InfoFormat("DataServiceContract. StopPlugin. {0}", message);
                    }

                    // System.Threading.Thread.Sleep(5000); -- uncomment if query will execute not in time
                    return new DataContractQueryResult(ResultStateTypes.Success, -1, message);
                }
                catch (Exception ex)
                {
                    message = $"Error occured in Start {pluginName} plugin!";
                    Logger.Log.ErrorFormat("DataServiceContract. StopPlugin. Details: {0}. {1}. Stacktrace: {2}",
                        message, ex.Message, ex.StackTrace);
                    return new DataContractQueryResult(ResultStateTypes.Failed, -1, message);
                }
            });
        }

        /// <summary>
        /// Use for Check current Connection state
        /// </summary>
        /// <returns></returns>
        public async Task<ResultStateTypes> CheckConnection()
        {
            Logger.Log.Debug("DataServiceContract. CheckConnection.");
            return await Task.Factory.StartNew(() => { return ResultStateTypes.Success; });
        }

        /// <summary>
        /// use for Chech Plugin Exists
        /// </summary>
        /// <param name="pluginName">Name of the Plugin</param>
        /// <returns></returns>
        private DataContractQueryResult CheckPluginExists(string pluginName)
        {
            Logger.Log.DebugFormat("DataServiceContract. CheckPluginExists. Plugin name: {0}", pluginName);

            if (!_pluginManager.Plugins.ContainsKey(pluginName))
            {
                var message = $"There is no {pluginName} plugin in Plugin collection!";
                Logger.Log.InfoFormat("DataServiceContract. CheckPluginExists. {0}", message);
                return new DataContractQueryResult(ResultStateTypes.Failed, -1, message);
            }

            return new DataContractQueryResult(ResultStateTypes.Success, -1, string.Empty);
        }

        #endregion
    }
}