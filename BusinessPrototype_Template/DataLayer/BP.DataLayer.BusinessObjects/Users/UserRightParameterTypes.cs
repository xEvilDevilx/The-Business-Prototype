using System;

namespace BP.DataLayer.BusinessObjects.Users
{
    /// <summary>
    /// Presents User Right Parameter types
    /// 
    /// 2018/11/20 - Created, VTyagunov
    /// </summary>
    [Flags]
    public enum UserRightParameterTypes : byte
    {
        ChangeUserRight = 1,
        ChangeUserGroupRight = 2,
        GetUserRightsStatus = 4,
        GetUserRights = 8
    }
}