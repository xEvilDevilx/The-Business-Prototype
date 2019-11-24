using System;
using System.Collections.Generic;

using BP.DataLayer.Databases.Base;

namespace BP.DataLayer.Interfaces
{
    /// <summary>
    /// Presents the Data Provider interface functionality
    /// 
    /// 2018/10/23 - Created, VTyagunov
    /// </summary>
    /// <typeparam name="TReturnObject">Type of the data provider business object to return</typeparam>
    /// <typeparam name="TParametersObject">Type of the data provider business object with query parameters data</typeparam>
    public interface IDataProvider<TReturnObject, TParametersObject> : IDisposable
    {
        /// <summary>
        /// Use for Add Record to the the table
        /// </summary>
        /// <param name="tParams">Object of the type TParametersObject</param>
        /// <returns></returns>
        DatabaseQueryResult Add(TParametersObject tParams);

        /// <summary>
        /// Use for Update the Record in the table
        /// </summary>
        /// <param name="tParams">Object of the type TParametersObject</param>
        /// <returns></returns>
        DatabaseQueryResult Update(TParametersObject tParams);

        /// <summary>
        /// Use for Get the Record from table
        /// </summary>
        /// <param name="tParams">Object of the type TParametersObject</param>
        /// <returns></returns>
        DatabaseQueryResult<TReturnObject> Get(TParametersObject tParams);

        /// <summary>
        /// Use for Get List of the Records from table
        /// </summary>
        /// <param name="tParams">Object of the type TParametersObject</param>
        /// <returns></returns>
        DatabaseQueryResult<List<TReturnObject>> GetList(TParametersObject tParams);

        /// <summary>
        /// Use for Delete the Record from table
        /// </summary>
        /// <param name="tParams">Object of the type TParametersObject</param>
        /// <returns></returns>
        DatabaseQueryResult Delete(TParametersObject tParams);

        /// <summary>
        /// Use for Search the List of the Records by Parameters
        /// </summary>
        /// <param name="tParams">Object of the type TParametersObject</param>
        /// <returns></returns>
        DatabaseQueryResult<List<TReturnObject>> Search(TParametersObject tParams);
    }
}