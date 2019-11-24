using System;

namespace BP.DataService.WinService.Interfaces
{
    /// <summary>
    /// Presents Data Service Client Plugin functionality interface
    /// 
    /// 2018/09/30 - Created, VTyagunov
    /// </summary>
    public interface IDataServiceClientPlugin : IDisposable
    {
        /// <summary>
        /// Use for Connect to Data Service Plugin host
        /// </summary>
        /// <returns></returns>
        void Connect();

        /// <summary>
        /// Use for Disconnect from Data Service Plugin host
        /// </summary>
        void Disconnect();
    }
}