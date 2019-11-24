namespace BP.Database.Base.Enums
{
    /// <summary>
    /// Presents Connection State types
    /// 
    /// 2018/09/10 - Created, VTyagunov
    /// </summary>
    public enum DBStateTypes : byte
    {
        SqlServerNotInstalled,
        DatabaseNotExists,
        ExecuteFailed,
        ReadConnectionStringFailed
    }
}