using BP.DataLayer.Databases.Base;

namespace BP.DataService.WinService.Interfaces
{
    /// <summary>
    /// Presents Data Service Host Plugin functionality interface
    /// 
    /// 2018/09/27 - Created, VTyagunov
    /// </summary>
    public interface IDataServiceHostPlugin
    {
        /// <summary>
        /// Use for Start Plugin host
        /// </summary>
        /// <param name="connectionSettings">Connection Settings</param>
        void Start(ConnectionSettingsExt connectionSettings);

        /// <summary>
        /// Use for Stop Plugin host
        /// </summary>
        void Stop();
    }
}