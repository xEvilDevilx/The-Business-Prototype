using System;

using BP.DataLayer.Base.Enums;

namespace BP.DataLayer.Base.Attributes
{
    /// <summary>
    /// Presents Operations Attribute for a data of the business object
    /// 
    /// 2018/10/31 - Created, VTyagunov
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class OperationsAttribute : Attribute
    {
        /// <summary>
        /// Keys of the Operation
        /// </summary>
        public OperationKeyTypes OperationKeys { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="operationKeys">Keys of the Operation</param>
        public OperationsAttribute(OperationKeyTypes operationKeys)
        {
            OperationKeys = operationKeys;
        }
    }

    /// <summary>
    /// Presents Operations Attribute for a data presents by enum
    /// 
    /// 2018/10/21 - Created, VTyagunov
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class OperationsEnumAttribute : Attribute
    {
        /// <summary>
        /// Keys of the Operation
        /// </summary>
        public Enum OperationKeys { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="operationKeys">Keys of the Operation</param>
        public OperationsEnumAttribute(object operationKeys)
        {
            OperationKeys = operationKeys as Enum;
        }
    }
}