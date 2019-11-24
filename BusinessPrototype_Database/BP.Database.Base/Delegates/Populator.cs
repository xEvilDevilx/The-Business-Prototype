using System.Data.SqlClient;

namespace BP.Database.Base.Delegates
{
    /// <summary>
    /// Presents Populator delegate
    /// 
    /// 2018/02/01 - Created, VTyagunov
    /// </summary>
    /// <typeparam name="T">Type of reading object</typeparam>
    /// <param name="reader">Sql Data Reader object</param>
    /// <returns>Readed object</returns>
    public delegate T Populator<T>(SqlDataReader reader);
}