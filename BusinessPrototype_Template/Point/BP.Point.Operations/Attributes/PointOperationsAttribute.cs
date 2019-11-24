using System;

namespace BP.Point.Operations.Attributes
{
    /// <summary>
    /// Presents Point Operations Attribute for mark operation classes
    /// 
    /// 2019/01/23 - Created, VTyagunov
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class PointOperationsAttribute : Attribute
    {
        /// <summary>
        /// ID of the Operation
        /// </summary>
        public int OperationID { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="operationID">ID of the Operation</param>
        public PointOperationsAttribute(int operationID)
        {
            OperationID = operationID;
        }
    }
}