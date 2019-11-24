using BP.SDK.Base.Enums;

namespace BP.SDK.Interfaces.Common
{
    /// <summary>
    /// Presents Result state interface
    /// 
    /// 2018/09/23 - Created, VTyagunov
    /// </summary>
    public interface IResult
    {
        /// <summary>
        /// State of the result
        /// </summary>
        ResultStateTypes ResultState { get; }
        /// <summary>
        /// Description of the result
        /// </summary>
        string ResultMessage { get; }
    }
}