using System;
using System.Collections.Generic;

using BP.DataLayer.Databases.Base;

namespace BP.DataLayer.Interfaces
{
    /// <summary>
    /// Presents functionality for static database queries
    /// 
    /// 2018/11/18 - Created, VTyagunov
    /// </summary>
    public interface IDataProviderManager : IDisposable
    {
        DatabaseQueryResult ExecuteNoneQuerySP<TObject, TPropertiesEnum>(string storedProcName, 
            TObject tObject, TPropertiesEnum tPropertiesEnumType);

        DatabaseQueryResult<object> ExecuteScalarSP<TObject, TPropertiesEnum>(string storedProcName,
            TObject tObj, TPropertiesEnum tPropertiesEnumType);

        DatabaseQueryResult<TReturn> ExecuteSP<TReturn, TObject, TPropertiesEnum>(string storedProcName,
            TObject tObject, TPropertiesEnum tPropertiesEnumType);

        DatabaseQueryResult<List<TReturn>> ExecuteListSP<TReturn, TObject, TPropertiesEnum>(string storedProcName,
            TObject tObject, TPropertiesEnum tPropertiesEnumType);
    }
}