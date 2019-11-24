using System;

namespace BP.Point.Operations.Attributes
{
    /// <summary>
    /// Presents Point Operations Methods Attribute for mark operation methods
    /// 
    /// 2019/01/23 - Created, VTyagunov
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class PointOperationsMethodAttribute : Attribute
    {
        /// <summary>
        /// ID of the Operation method
        /// </summary>
        public int OperationMethodID { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="operationMethodID">ID of the Operation method</param>
        public PointOperationsMethodAttribute(int operationMethodID)
        {
            OperationMethodID = operationMethodID;
        }
    }
}