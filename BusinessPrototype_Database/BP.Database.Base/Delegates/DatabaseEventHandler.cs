using BP.Database.Base.Enums;

namespace BP.Database.Base.Delegates
{
    /// <summary>
    /// Presents Database Event Handler
    /// </summary>
    /// <param name="dbStateTypes">DB State</param>
    /// <param name="detailsObject">Object for transfer details into event</param>
    public delegate void DatabaseEventHandler(DBStateTypes dbState, object detailsObject);
}