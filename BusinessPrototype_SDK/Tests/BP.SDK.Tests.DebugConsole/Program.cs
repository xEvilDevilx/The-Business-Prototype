using System;
using BP.SDK.Configs;
using BP.SDK.Interfaces.Configs;
using BP.SDK.Interfaces.Plugins;
using BP.SDK.Plugins;

namespace BP.SDK.Tests.DebugConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            TestConfigurator();

            Console.ReadLine();
        }

        private static void TestConfigurator()
        {
            // Prepare
            IConfigurator configurator1 = new Configurator();
            IConfigurator configurator2 = new Configurator("SecondTestConfig");
            IConfigurator configurator3 = new Configurator("TestBP\\Configs\\", "ThirdTestConfig");

            string testTag1 = "TestTag1";
            string testValue1 = "TestValue1";

            string testTag21 = "TestTag2-1";
            string testValue21 = "TestValue2-1";
            string testTag20 = "TestTag2-0";
            string testValue20 = "TestValue2-0";

            string testTag32 = "TestTag3-2";
            string testValue32 = "TestValue3-2";
            string testTag30 = "TestTag3-0";
            string testValue30 = "TestValue3-0";
            string testTag31 = "TestTag3-1";
            string testValue31 = "TestValue3-1";
            string testValue305 = "TestValue3-0-5";

            // Execute
            configurator1.WriteValue(testTag1, testValue1);

            configurator2.WriteValue(testTag21, testValue21);
            configurator2.WriteValue(testTag20, testValue20);

            configurator3.WriteValue(testTag32, testValue32);
            configurator3.WriteValue(testTag30, testValue30);
            configurator3.WriteValue(testTag31, testValue31);

            var confValue1 = configurator1.ReadValue(testTag1);

            var confValue21 = configurator2.ReadValue(testTag21);
            var confValue20 = configurator2.ReadValue(testTag20);

            var confValue32 = configurator3.ReadValue(testTag32);
            var confValue30 = configurator3.ReadValue(testTag30);
            var confValue31 = configurator3.ReadValue(testTag31);

            configurator3.WriteValue(testTag30, testValue305);
            var confValue305 = configurator3.ReadValue(testTag30);

            // Check
            
        }

        private static void TestPluginManager()
        {
            IPluginManager<IPlugin> pluginManager = new PluginManager<IPlugin>();
            var pluginsPath = @"G:\DEVELOPMENT\BusinessPrototype\BusinessPrototype_SDK\Tests\BP.SDK.Tests.DebugConsole\bin\Debug\Plugins";
            pluginManager.LoadPlugins(pluginsPath);
        }
    }
}