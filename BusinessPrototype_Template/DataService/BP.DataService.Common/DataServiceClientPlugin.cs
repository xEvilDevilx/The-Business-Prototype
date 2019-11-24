using System;
using System.ServiceModel;
using System.Xml;

using BP.DataService.WinService.Interfaces;
using BP.SDK.Log;

namespace BP.DataService.Common
{
    /// <summary>
    /// Implements Data Service Client Plugin functionality
    /// 
    /// 2018/09/30 - Created, VTyagunov
    /// </summary>
    /// <typeparam name="T">Type of the plugin interface</typeparam>
    public class DataServiceClientPlugin<T> : IDataServiceClientPlugin
    {
        #region Variables

        /// <summary>Channel Factory</summary>
        private ChannelFactory<T> _channelFactory;

        #endregion

        #region Properties

        /// <summary>Object of the T type</summary>
        public T TObject { get; private set; }
        /// <summary>State of the connection to the Data Service host</summary>
        public CommunicationState State
        {
            get
            {
                if (_channelFactory == null)
                    return CommunicationState.Closed;
                return _channelFactory.State;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Use for a Dispose current object
        /// </summary>
        public void Dispose()
        {
            Logger.Log.DebugFormat("DataServiceClientPlugin<{0}>. Dispose", typeof(T).Name);
            Disconnect();
        }

        /// <summary>
        /// Use for Connect to Data Service Plugin host
        /// </summary>
        /// <returns></returns>
        public void Connect()
        {
            try
            {
                Logger.Log.DebugFormat("DataServiceClientPlugin<{0}>. Connect", typeof(T).Name);

                var quotas = new XmlDictionaryReaderQuotas();
                quotas.MaxStringContentLength = int.MaxValue;

                var reliableSession = new OptionalReliableSession();
                reliableSession.InactivityTimeout = new TimeSpan(0, 5, 0);

                var binding = new NetTcpBinding(SecurityMode.Message);
                binding.MaxBufferPoolSize = int.MaxValue;
                binding.MaxBufferSize = int.MaxValue;
                binding.MaxReceivedMessageSize = int.MaxValue;
                binding.ReaderQuotas = quotas;
                binding.ReceiveTimeout = new TimeSpan(0, 5, 0);
                binding.ReliableSession = reliableSession;
                binding.Security.Message.ClientCredentialType = MessageCredentialType.Windows;
                binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
                binding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;
                binding.SendTimeout = new TimeSpan(0, 5, 0);

                var type = typeof(T);
                var uriStr = string.Format("net.tcp://{0}:{1}/{2}", "localhost", "9002", type.Name);
                var endpoint = new EndpointAddress(new Uri(uriStr));
                _channelFactory = new ChannelFactory<T>(binding, endpoint);
                _channelFactory.Credentials.Windows.ClientCredential.Domain = "";
                _channelFactory.Credentials.Windows.ClientCredential.UserName = "";
                _channelFactory.Credentials.Windows.ClientCredential.Password = "";
                _channelFactory.Closed += OnClosed;
                _channelFactory.Closing += OnClosing;
                _channelFactory.Faulted += OnFaulted;
                _channelFactory.Opened += OnOpened;
                _channelFactory.Opening += OnOpening;

                TObject = _channelFactory.CreateChannel();
                Logger.Log.InfoFormat("DataServiceClientPlugin<{0}>. Connect. {1} Channel created.", typeof(T).Name,
                    _channelFactory.Endpoint);
            }
            catch(Exception ex)
            {
                Logger.Log.ErrorFormat("DataServiceClientPlugin<{0}>. Connect. Details: {1}. Stacktrace: {2}", 
                    typeof(T).Name, ex.Message, ex.StackTrace);
                Disconnect();
                TObject = default(T);
            }
        }

        /// <summary>
        /// Use for Disconnect from Data Service Plugin host
        /// </summary>
        public void Disconnect()
        {
            try
            {
                Logger.Log.DebugFormat("DataServiceClientPlugin<{0}>. Disconnect", typeof(T).Name);

                if (_channelFactory == null)
                    return;

                if (_channelFactory.State == CommunicationState.Closed)
                    return;

                if (_channelFactory.State == CommunicationState.Faulted)
                    Abort();
                else if (_channelFactory.State != CommunicationState.Closed)
                    Close();

                Logger.Log.InfoFormat("DataServiceClientPlugin<{0}>. Disconnect. {1} Channel closed.", typeof(T).Name,
                    _channelFactory.Endpoint);
            }
            catch(Exception ex)
            {
                Logger.Log.ErrorFormat("DataServiceClientPlugin<{0}>. Disconnect. Details: {1}. Stacktrace: {2}",
                    typeof(T).Name, ex.Message, ex.StackTrace);
                Abort();
            }
            finally
            {
                TObject = default(T);
                _channelFactory = null;
            }
        }

        /// <summary>
        /// Use for Abort connection to the Data Service host
        /// </summary>
        private void Abort()
        {
            try
            {
                Logger.Log.DebugFormat("DataServiceClientPlugin<{0}>. Abort", typeof(T).Name);

                if (_channelFactory != null)
                    _channelFactory.Abort();
            }
            catch(Exception ex)
            {
                Logger.Log.ErrorFormat("DataServiceClientPlugin<{0}>. Abort. Details: {1}. Stacktrace: {2}",
                    typeof(T).Name, ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// Use for Close connection to the Data Service host
        /// </summary>
        private void Close()
        {
            try
            {
                Logger.Log.DebugFormat("DataServiceClientPlugin<{0}>. Close", typeof(T).Name);

                if (_channelFactory != null)
                    _channelFactory.Close();
            }
            catch (Exception ex)
            {
                Logger.Log.ErrorFormat("DataServiceClientPlugin<{0}>. Close. Details: {1}. Stacktrace: {2}",
                    typeof(T).Name, ex.Message, ex.StackTrace);
            }
        }

        #region Events

        /// <summary>
        /// Actions for On Opening Data Service host event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event Args</param>
        private void OnOpening(object sender, EventArgs e)
        {
            Logger.Log.DebugFormat("DataServiceClientPlugin<{0}>. OnOpening", typeof(T).Name);
        }

        /// <summary>
        /// Actions for On Opened Data Service host event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event Args</param>
        private void OnOpened(object sender, EventArgs e)
        {
            Logger.Log.DebugFormat("DataServiceClientPlugin<{0}>. OnOpened", typeof(T).Name);
        }

        /// <summary>
        /// Actions for On Closing Data Service host event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event Args</param>
        private void OnClosing(object sender, EventArgs e)
        {
            Logger.Log.DebugFormat("DataServiceClientPlugin<{0}>. OnClosing", typeof(T).Name);
        }

        /// <summary>
        /// Actions for On Closed Data Service host event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event Args</param>
        private void OnClosed(object sender, EventArgs e)
        {
            Logger.Log.DebugFormat("DataServiceClientPlugin<{0}>. OnClosed", typeof(T).Name);
            TObject = default(T);
        }

        /// <summary>
        /// Actions for On Faulted Data Service host event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event Args</param>
        private void OnFaulted(object sender, EventArgs e)
        {
            Logger.Log.DebugFormat("DataServiceClientPlugin<{0}>. OnFaulted", typeof(T).Name);
            TObject = default(T);
        }

        #endregion

        #endregion
    }
}