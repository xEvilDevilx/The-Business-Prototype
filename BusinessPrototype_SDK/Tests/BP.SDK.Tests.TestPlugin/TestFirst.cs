using BP.SDK.Interfaces.Plugins;

namespace BP.SDK.Tests.TestPlugin
{
    /// <summary>
    /// Implements a Test Plugin functionality
    /// 
    /// 2018/09/21 - Created, VTyagunov
    /// </summary>
    public class TestFirst : IPlugin
    {
        private string _testFirst;
        private string _test;

        public string TestSecond
        {
            get
            {
                return _testFirst;
            }
            set
            {
                _testFirst = value;
            }
        }

        public string Test
        {
            get
            {
                return _test;
            }
            set
            {
                _test = value;
            }
        }

        public void DoTest()
        {
            Test = "DoTest1";
        }

        public void DoTestSecond()
        {
            TestSecond = "DoTestSecond1";
        }

        public void Start()
        {

        }

        public void Stop()
        {

        }
    }
}