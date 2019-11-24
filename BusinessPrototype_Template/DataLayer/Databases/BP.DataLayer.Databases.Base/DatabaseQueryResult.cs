using BP.DataLayer.Databases.Base.Enums;

namespace BP.DataLayer.Databases.Base
{
    /// <summary>
    /// Implements the Query Result data object
    /// 
    /// 2018/10/24 - Created, VTyagunov
    /// </summary>
    public class DatabaseQueryResult
    {
        #region Properties

        /// <summary>
        /// Type of the Query Result
        /// </summary>
        public DatabaseQueryResultTypes QueryResultType { get; private set; }

        /// <summary>
        /// Message of the Result
        /// </summary>
        public string ResultMessage { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public DatabaseQueryResult()
        {
            QueryResultType = DatabaseQueryResultTypes.None;
            ResultMessage = string.Empty;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="queryResultType">Type of the Query Result</param>
        public DatabaseQueryResult(DatabaseQueryResultTypes queryResultType)
        {
            QueryResultType = queryResultType;
            ResultMessage = string.Empty;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="queryResultType">Type of the Query Result</param>
        /// <param name="resultMessage">Message of the Result</param>
        public DatabaseQueryResult(DatabaseQueryResultTypes queryResultType, string resultMessage)
        {
            QueryResultType = queryResultType;
            ResultMessage = resultMessage;
        }

        #endregion
    }

    /// <summary>
    /// Implements the Query Result data object
    /// 
    /// 2018/10/24 - Created, VTyagunov
    /// </summary>
    /// <typeparam name="T">Type of the object to return</typeparam>
    public class DatabaseQueryResult<T> : DatabaseQueryResult
    {
        #region Properties

        /// <summary>
        /// Object of the Result
        /// </summary>
        public T ResultObject { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public DatabaseQueryResult() : base()
        {
            
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="queryResultType">Type of the Query Result</param>
        public DatabaseQueryResult(DatabaseQueryResultTypes queryResultType) : base(queryResultType)
        {
            
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="queryResultType">Type of the Query Result</param>
        /// <param name="resultMessage">Message of the Result</param>
        public DatabaseQueryResult(DatabaseQueryResultTypes queryResultType, string resultMessage) :
            base(queryResultType, resultMessage)
        {
            
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="queryResultType">Type of the Query Result</param>
        /// <param name="resultMessage">Message of the Result</param>
        /// <param name="resultObject">Object of the Result</param>
        public DatabaseQueryResult(DatabaseQueryResultTypes queryResultType, string resultMessage, T resultObject) :
            base(queryResultType, resultMessage)
        {
            ResultObject = resultObject;
        }

        #endregion
    }
}