using System;
using System.Data;

namespace BP.DataLayer.Base.Attributes
{
    /// <summary>
    /// Presents ParametersA Attribute for a data of the business object
    /// 
    /// 2018/10/31 - Created, VTyagunov
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ParametersAttribute : Attribute
    {
        #region Properties

        /// <summary>
        /// Name of the Parameter
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Type of the Parameter
        /// </summary>
        public SqlDbType Type { get; private set; }
        /// <summary>
        /// Direction of the Parameter (Input/Output,Return)
        /// </summary>
        public ParameterDirection Direction { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the Parameter</param>
        /// <param name="type">Type of the Parameter</param>
        public ParametersAttribute(string name, SqlDbType type)
        {
            Name = name;
            Type = type;
            Direction = ParameterDirection.Input;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the Parameter</param>
        /// <param name="type">Type of the Parameter</param>
        /// <param name="direction">Direction of the Parameter (Input/Output,Return)</param>
        public ParametersAttribute(string name, SqlDbType type, ParameterDirection direction)
        {
            Name = name;
            Type = type;
            Direction = direction;
        }

        #endregion
    }
}