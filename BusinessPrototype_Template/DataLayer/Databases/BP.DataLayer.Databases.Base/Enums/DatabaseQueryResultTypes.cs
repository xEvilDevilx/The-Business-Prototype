namespace BP.DataLayer.Databases.Base.Enums
{
    /// <summary>
    /// Presents the Query Result Types
    /// 
    /// 2018/10/24 - Created, VTyagunov
    /// </summary>
    public enum DatabaseQueryResultTypes : byte
    {
        None = 255,
        Error = 0,
        Success = 1,
        RecordAlreadyExists = 2,
        RecordNotExists = 3
    }
}