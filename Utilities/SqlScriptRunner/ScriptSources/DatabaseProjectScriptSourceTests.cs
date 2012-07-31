using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
namespace SqlScriptRunner.ScriptSources
{
    
    [TestFixture]
    public class DatabaseProjectScriptSourceTests
    {

        [Test]
        public void GoUpALevel_DummyData_Test()
        {
            DatabaseProjectScriptSource dp = new DatabaseProjectScriptSource("asds");
            dp.currentPath = ".\\hello\\";
            dp.GoUpALevel();
            Assert.AreEqual(".\\", dp.currentPath);
        }
    }
}
