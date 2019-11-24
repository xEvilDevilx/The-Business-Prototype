using System;

namespace BP.DataLayer.Base.Enums
{
    /// <summary>
    /// Presents Operation Key types
    /// 
    /// 2018/11/01 - Created, VTyagunov
    /// </summary>
    [Flags]
    public enum OperationKeyTypes : byte
    {
        Add = 1,
        Update = 2,
        Get = 4,
        GetList = 8,
        Delete = 16,
        Search = 32
    }
}