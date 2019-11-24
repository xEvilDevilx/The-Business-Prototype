using System.Runtime.Serialization;

using BP.SDK.Base.Enums;

namespace BP.DataService.Base
{
    /// <summary>
    /// Implements Result data contract
    /// 
    /// 2018/09/23 - Created, VTyagunov
    /// </summary>
    [DataContract]
    public class DataContractQueryResult
    {
        #region Properties

        /// <summary>
        /// State of the query result
        /// </summary>
        [DataMember]
        public ResultStateTypes ResultState { get; private set; }

        /// <summary>
        /// Code of the Result
        /// </summary>
        [DataMember]
        public int ResultCode { get; private set; }

        /// <summary>
        /// Message of the query result
        /// </summary>
        [DataMember]
        public string ResultMessage { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public DataContractQueryResult()
        {
            ResultState = ResultStateTypes.Failed;
            ResultCode = -1;
            ResultMessage = string.Empty;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="resultState">State of the query result</param>
        public DataContractQueryResult(ResultStateTypes resultState)
        {
            ResultState = resultState;
            ResultCode = -1;
            ResultMessage = string.Empty;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="resultState">State of the query result</param>
        /// <param name="resultCode">Code of the query result</param>
        public DataContractQueryResult(ResultStateTypes resultState, int resultCode)
        {
            ResultState = resultState;
            ResultCode = resultCode;
            ResultMessage = string.Empty;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="resultState">State of the query result</param>
        /// <param name="resultCode">Code of the query result</param>
        /// <param name="resultMessage">Message of the query result</param>
        public DataContractQueryResult(ResultStateTypes resultState, int resultCode, string resultMessage)
        {
            ResultState = resultState;
            ResultCode = resultCode;
            ResultMessage = resultMessage;
        }

        #endregion
    }

    /// <summary>
    /// Implements Result data contract
    /// 
    /// 2018/09/23 - Created, VTyagunov
    /// </summary>
    [DataContract]
    public class DataContractQueryResult<T> : DataContractQueryResult
    {
        #region Properties

        /// <summary>
        /// Code of the Result
        /// </summary>
        [DataMember]
        public T ResultObject { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public DataContractQueryResult() : base()
        {
            
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="resultState">State of the query result</param>
        public DataContractQueryResult(ResultStateTypes resultState) : base(resultState)
        {
            
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="resultState">State of the query result</param>
        /// <param name="resultCode">Code of the query result</param>
        public DataContractQueryResult(ResultStateTypes resultState, int resultCode) :
            base(resultState, resultCode)
        {
            
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="resultState">State of the query result</param>
        /// <param name="resultCode">Code of the query result</param>
        /// <param name="resultMessage">Message of the query result</param>
        public DataContractQueryResult(ResultStateTypes resultState, int resultCode, string resultMessage) :
            base(resultState, resultCode, resultMessage)
        {
            
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="resultState">State of the query result</param>
        /// <param name="resultCode">Code of the query result</param>
        /// <param name="resultMessage">Message of the query result</param>
        public DataContractQueryResult(ResultStateTypes resultState, int resultCode, string resultMessage,
            T resultObject) : base(resultState, resultCode, resultMessage)
        {
            ResultObject = resultObject;
        }

        #endregion
    }
}