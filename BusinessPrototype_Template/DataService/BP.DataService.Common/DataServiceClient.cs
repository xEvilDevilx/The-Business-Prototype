using System;
using System.ServiceModel;
using System.Threading.Tasks;

using BP.DataService.Base;
using BP.DataService.Interfaces;
using BP.SDK.Base.Enums;
using BP.SDK.Log;

namespace BP.DataService.Common
{
    /// <summary>
    /// Implements Data Service Client utils functionality
    /// 
    /// 2018/10/07 - Created, VTyagunov
    /// </summary>
    public class DataServiceClient
    {
        #region Delegates

        /// <summary>
        /// Presents a Data Service Method Link delegate
        /// </summary>
        /// <typeparam name="T">Data Service Plugin Interface type</typeparam>
        /// <param name="tDataServiceInterface">Data Service Plugin Interface</param>
        /// <returns></returns>
        public delegate Task DSMethodLinkDelegate<T>(T tDataServiceInterface);

        /// <summary>
        /// Presents a Deta Service Return Method Link delegate
        /// </summary>
        /// <typeparam name="T">Data Service Plugin Interface type</typeparam>
        /// <typeparam name="R">Type of the Return object</typeparam>
        /// <param name="tDataServiceInterface">Data Service Plugin Interface</param>
        /// <returns></returns>
        public delegate Task<R> DSReturnMethodLinkDelegate<T, R>(T tDataServiceInterface);

        /// <summary>
        /// Presents On Fail Event Handler delegate
        /// </summary>
        /// <param name="queryResult">Query Result</param>
        public delegate void OnErrorEventHandler(DataContractQueryResult queryResult);

        #endregion

        #region Events

        /// <summary>On Error Event</summary>
        public static event OnErrorEventHandler OnErrorEvent;

        #endregion

        #region Methods

        /// <summary>
        /// Use for Execute a method to the Data Service
        /// </summary>
        /// <typeparam name="T">Data Service Plugin Interface type</typeparam>
        /// <param name="method">Method for Execute</param>
        /// <returns></returns>
        public static async Task Execute<T>(DSMethodLinkDelegate<T> method) where T : IDataServicePlugin
        {
            Logger.Log.DebugFormat("DataServiceClient. Execute<{0}>", typeof(T).Name);

            DataServiceClientPlugin<T> dsClientPlugin = null;

            try
            {
                dsClientPlugin = new DataServiceClientPlugin<T>();
                dsClientPlugin.Connect();
                await method((T)dsClientPlugin.TObject);
            }
            catch (Exception ex)
            {
                var message = string.Format("Details: {0}. Stacktrace: {1}", ex.Message, ex.StackTrace);
                Logger.Log.ErrorFormat("DataServiceClient. Execute<{0}>. {1}",
                    typeof(T).Name, message);
                var queryResult = new DataContractQueryResult(ResultStateTypes.Failed, -1, message);
                if (OnErrorEvent != null)
                    OnErrorEvent(queryResult);
            }
            finally
            {
                var d = dsClientPlugin as IDisposable;
                if (d != null)
                    d.Dispose();

                dsClientPlugin = default(DataServiceClientPlugin<T>);
            }
        }

        /// <summary>
        /// Use for Execute a method to the Data Service
        /// </summary>
        /// <typeparam name="T">Data Service Plugin Interface type</typeparam>
        /// <typeparam name="R">Type of the Return object</typeparam>
        /// <param name="method">Method for Execute</param>
        /// <returns></returns>
        public static async Task<R> Execute<T, R>(DSReturnMethodLinkDelegate<T, R> method) where R : new() where T : IDataServicePlugin
        {
            Logger.Log.DebugFormat("DataServiceClient. Execute<{0}, {1}>", typeof(T).Name, typeof(R).Name);

            DataServiceClientPlugin<T> dsClientPlugin = null;

            try
            {
                dsClientPlugin = new DataServiceClientPlugin<T>();
                dsClientPlugin.Connect();

                if (dsClientPlugin.State != CommunicationState.Opened)
                    throw new Exception("DataServiceClientPlugin connection to host doesn't open!");

                var result = await method((T)dsClientPlugin.TObject);
                return result;
            }
            catch(Exception ex)
            {
                var message = string.Format("Details: {0}. Stacktrace: {1}", ex.Message, ex.StackTrace);
                Logger.Log.ErrorFormat("DataServiceClient. Execute<{0}, {1}>. {2}",
                    typeof(T).Name, typeof(R).Name, message);
                var queryResult = new DataContractQueryResult(ResultStateTypes.Failed, -1, message);
                if (OnErrorEvent != null)
                    OnErrorEvent(queryResult);
                return default(R);
            }
            finally
            {
                var d = dsClientPlugin as IDisposable;
                if (d != null)
                    d.Dispose();

                dsClientPlugin = default(DataServiceClientPlugin<T>);
            }
        }

        #endregion
    }
}