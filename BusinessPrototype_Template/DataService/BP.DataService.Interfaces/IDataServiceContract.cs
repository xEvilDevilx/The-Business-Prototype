using System.ServiceModel;
using System.Threading.Tasks;

using BP.DataService.Base;
using BP.SDK.Base.Enums;

namespace BP.DataService.Interfaces
{
    /// <summary>
    /// Presents Data Service Contract functionality interface
    /// 
    /// 2018/09/17 - Created, VTyagunov
    /// </summary>
    [ServiceContract]
    public interface IDataServiceContract
    {
        /// <summary>
        /// Use for Start Plugin by plugin name
        /// </summary>
        /// <param name="pluginName">Name of the Plugin</param>
        /// <returns></returns>
        [OperationContract]
        Task<DataContractQueryResult> StartPlugin(string pluginName);

        /// <summary>
        /// Use for Stop Plugin by plugin name
        /// </summary>
        /// <param name="pluginName">Name of the Plugin</param>
        /// <returns></returns>
        [OperationContract]
        Task<DataContractQueryResult> StopPlugin(string pluginName);

        /// <summary>
        /// Use for Check current Connection state
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        Task<ResultStateTypes> CheckConnection();
    }
}