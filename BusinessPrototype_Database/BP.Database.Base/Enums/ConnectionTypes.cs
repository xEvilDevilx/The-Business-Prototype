namespace BP.Database.Base.Enums
{
    /// <summary>
    /// Presents Connection Types
    /// 
    /// 2018/09/09 - Created, VTyagunov
    /// </summary>
    public enum ConnectionTypes : byte
    {
        TCPIP,
        NamedPipes,
        SharedMemory        
    }
}