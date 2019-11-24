using System.ServiceProcess;

using BP.DataService.WinService.Contracts;
using BP.SDK.Log;

namespace BP.DataService.WinService
{
    /// <summary>
    /// Implements Data Service functionality
    /// 
    /// 2018/09/19 - Created, VTyagunov
    /// </summary>
    public partial class DataService : ServiceBase
    {
        #region Variables

        /// <summary>Data Service Contract</summary>
        private static DataServiceContract _dataServiceContract;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public DataService()
        {
            Logger.Log.Debug("DataService. Ctr");

            InitializeComponent();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Use for Initialize all important functionality on Start server
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            Logger.Log.Debug("DataService. OnStart");

            _dataServiceContract = new DataServiceContract();

            if(_dataServiceContract != null)
                _dataServiceContract.Start(null);
            else Logger.Log.ErrorFormat("DataService. OnStart. Error occured in the service start. Data Service Contract is null");
        }

        /// <summary>
        /// Use for Stop all important functionality on Stop server
        /// </summary>
        protected override void OnStop()
        {
            Logger.Log.Debug("DataService. OnStop");

            if (_dataServiceContract != null)
                _dataServiceContract.Stop();
            else Logger.Log.ErrorFormat("DataService. OnStop. Error occured in the service stop. Data Service Contract is null");
        }

        #endregion
    }
}