using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
namespace MSBuildTasks
{
    [TestFixture]
    public class ReplaceText_Tests
    {
        [Test]
        public void TextIsReplaced(){
            System.IO.File.WriteAllText("TempFile", "hello");
            ReplaceText replaceText = new ReplaceText() { FilePath = "TempFile", OldText = "hello", NewText = "goodbye" };
            replaceText.Execute();
            Assert.AreEqual("goodbye", System.IO.File.ReadAllText("TempFile"));
            System.IO.File.Delete("TempFile");

        }
    }
}
