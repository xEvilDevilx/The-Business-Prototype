using System;
using System.Net.Security;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Xml;

using BP.DataLayer.Databases.Base;
using BP.DataService.WinService.Interfaces;
using BP.SDK.Log;

namespace BP.DataService.Common
{
    /// <summary>
    /// Implements Data Service Plugin functionality
    /// 
    /// 2018/09/27 - Created, VTyagunov
    /// </summary>
    /// <typeparam name="T">Type of the plugin interface</typeparam>
    public abstract class DataServiceHostPlugin<T> : IDataServiceHostPlugin
    {
        #region Variables

        /// <summary>Service Host</summary>
        private ServiceHost _serviceHost = null;

        #endregion

        #region Properties

        /// <summary>
        /// Connection Settings
        /// </summary>
        public ConnectionSettingsExt ConnectionSettings { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Use for Start Plugin host
        /// </summary>
        /// <param name="connectionSettings">Connection Settings</param>
        public void Start(ConnectionSettingsExt connectionSettings)
        {
            try
            {
                Logger.Log.DebugFormat("DataServiceHostPlugin<{0}>. Start", typeof(T).Name);

                if (connectionSettings == null)
                    throw new ArgumentNullException(nameof(connectionSettings), 
                        "Connection Settings in the Start method parameter of the Data Service Host Plugin is null!");

                ConnectionSettings = connectionSettings;

                if (_serviceHost != null) _serviceHost.Close();

                var type = typeof(T);
                var addressTCP = string.Format("net.tcp://{0}:{1}/{2}", "localhost", "9002", type.Name);

                Uri[] addressBase = { new Uri(addressTCP) };
                _serviceHost = new ServiceHost(this, addressBase);

                var serviceMetadataBehavior = new ServiceMetadataBehavior();
                var serviceThrottlingBehavior = new ServiceThrottlingBehavior();
                serviceThrottlingBehavior.MaxConcurrentCalls = 500;
                serviceThrottlingBehavior.MaxConcurrentInstances = 500;
                serviceThrottlingBehavior.MaxConcurrentSessions = 50;

                _serviceHost.Description.Behaviors.Add(serviceMetadataBehavior);
                _serviceHost.Description.Behaviors.Add(serviceThrottlingBehavior);
                if (!_serviceHost.Description.Behaviors.Contains(typeof(ServiceDebugBehavior)))
                {
                    var serviceDebugBehavior = new ServiceDebugBehavior();
                    serviceDebugBehavior.IncludeExceptionDetailInFaults = true;
                    _serviceHost.Description.Behaviors.Add(serviceDebugBehavior);
                }
                else ((ServiceDebugBehavior)_serviceHost.Description.Behaviors[typeof(ServiceDebugBehavior)]).IncludeExceptionDetailInFaults = true;

                var netTcpBinding = new NetTcpBinding();
                netTcpBinding.MaxBufferSize = int.MaxValue;
                netTcpBinding.MaxReceivedMessageSize = int.MaxValue;
                netTcpBinding.ReaderQuotas = new XmlDictionaryReaderQuotas();
                netTcpBinding.ReaderQuotas.MaxStringContentLength = int.MaxValue;
                netTcpBinding.ReceiveTimeout = TimeSpan.MaxValue;
                netTcpBinding.ReliableSession = new OptionalReliableSession();
                netTcpBinding.ReliableSession.InactivityTimeout = TimeSpan.MaxValue;
                netTcpBinding.Security.Mode = SecurityMode.Message;
                netTcpBinding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
                netTcpBinding.Security.Message.ClientCredentialType = MessageCredentialType.Windows;
                netTcpBinding.Security.Transport.ProtectionLevel = ProtectionLevel.EncryptAndSign;
                netTcpBinding.SendTimeout = new TimeSpan(0, 0, 60);

                _serviceHost.AddServiceEndpoint(typeof(T), netTcpBinding, addressTCP);
                _serviceHost.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexTcpBinding(), "mex");

                _serviceHost.Open();

                Logger.Log.InfoFormat("DataServiceHostPlugin<{0}>. Start. {1} host started.", typeof(T).Name, _serviceHost.BaseAddresses[0]);
            }
            catch(Exception ex)
            {
                Logger.Log.ErrorFormat("DataServiceHostPlugin<{0}>. Start. Details: {1}. Stacktrace: {2}",
                    typeof(T).Name, ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// Use for Stop Plugin host
        /// </summary>
        public void Stop()
        {
            try
            {
                Logger.Log.DebugFormat("DataServiceHostPlugin<{0}>. Stop", typeof(T).Name);

                if (_serviceHost != null)
                {
                    _serviceHost.Close();
                    _serviceHost = null;
                }

                ConnectionSettings = null;

                Logger.Log.InfoFormat("DataServiceHostPlugin<{0}>. Stop. {1} host started.", typeof(T).Name, _serviceHost.BaseAddresses[0]);
            }
            catch(Exception ex)
            {
                Logger.Log.ErrorFormat("DataServiceHostPlugin<{0}>. Stop. Details: {1}. Stacktrace: {2}",
                    typeof(T).Name, ex.Message, ex.StackTrace);
            }
        }

        #endregion
    }
}