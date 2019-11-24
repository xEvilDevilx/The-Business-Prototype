using log4net;
using log4net.Config;

namespace BP.SDK.Log
{
    /// <summary>
    /// Implements static class for log4net using
    /// 
    /// 2016/12/21 - Created, VTyagunov
    /// </summary>
    public static class Logger
    {
        /// <summary>ILog logger</summary>
        private static readonly ILog log = LogManager.GetLogger(typeof(Logger));

        /// <summary>
        /// Get logger object
        /// </summary>
        public static ILog Log
        {
            get { return log; }
        }

        /// <summary>
        /// Using for initialization of Logger
        /// </summary>
        public static void InitLogger()
        {
            XmlConfigurator.Configure();
        }
    }
}