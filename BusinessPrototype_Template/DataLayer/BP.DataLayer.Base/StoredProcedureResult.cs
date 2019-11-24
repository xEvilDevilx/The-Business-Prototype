using System.Data;

using BP.DataLayer.Base.Attributes;
using BP.DataLayer.Base.Enums;

namespace BP.DataLayer.Base
{
    /// <summary>
    /// Presents Stored Procedure Result data object
    /// 
    /// 2018/11/04 - Created, VTyagunov
    /// </summary>
    public class StoredProcedureResult
    {
        /// <summary>
        /// User Result
        /// </summary>
        [Operations(
            OperationKeyTypes.Add |
            OperationKeyTypes.Update |
            OperationKeyTypes.Get |
            OperationKeyTypes.GetList |
            OperationKeyTypes.Delete |
            OperationKeyTypes.Search)]
        [Parameters("@Result", SqlDbType.TinyInt, ParameterDirection.Output)]
        public byte Result { get; set; }

        /// <summary>
        /// User ErrorNumber
        /// </summary>
        [Operations(
            OperationKeyTypes.Add |
            OperationKeyTypes.Update |
            OperationKeyTypes.Get |
            OperationKeyTypes.GetList |
            OperationKeyTypes.Delete |
            OperationKeyTypes.Search)]
        [Parameters("@ErrorNumber", SqlDbType.Int, ParameterDirection.Output)]
        public int ErrorNumber { get; set; }

        /// <summary>
        /// User ErrorMessage
        /// </summary>
        [Operations(
            OperationKeyTypes.Add |
            OperationKeyTypes.Update |
            OperationKeyTypes.Get |
            OperationKeyTypes.GetList |
            OperationKeyTypes.Delete |
            OperationKeyTypes.Search)]
        [Parameters("@ErrorMessage", SqlDbType.NVarChar, ParameterDirection.Output)]
        public string ErrorMessage { get; set; }
    }
}