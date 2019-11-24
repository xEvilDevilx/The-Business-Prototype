using System.Threading.Tasks;

using BP.DataService.Base;
using BP.DataService.Common;
using BP.DataService.Interfaces;
using BP.SDK.Base.Enums;
using BP.SDK.Log;

namespace BP.DataService.WinService.Channels
{
    /// <summary>
    /// Presents Data Service Channel functionality
    /// 
    /// 2018/09/30 - Created, VTyagunov
    /// </summary>
    public class DataServiceChannel : DataServiceClientPlugin<IDataServiceContract>, IDataServiceContract
    {
        #region Methods

        /// <summary>
        /// Use for Start Plugin by plugin name
        /// </summary>
        /// <param name="pluginName">Name of the Plugin</param>
        /// <returns></returns>
        public async Task<DataContractQueryResult> StartPlugin(string pluginName)
        {
            Logger.Log.Debug("DataServiceChannel. StartPlugin");
            return await TObject.StartPlugin(pluginName);
        }

        /// <summary>
        /// Use for Stop Plugin by plugin name
        /// </summary>
        /// <param name="pluginName">Name of the Plugin</param>
        /// <returns></returns>
        public async Task<DataContractQueryResult> StopPlugin(string pluginName)
        {
            Logger.Log.Debug("DataServiceChannel. StopPlugin");
            return await TObject.StopPlugin(pluginName);
        }

        /// <summary>
        /// Use for Check current Connection state
        /// </summary>
        /// <returns></returns>
        public async Task<ResultStateTypes> CheckConnection()
        {
            Logger.Log.Debug("DataServiceChannel. CheckConnection");
            return await TObject.CheckConnection();
        }

        #endregion
    }
}