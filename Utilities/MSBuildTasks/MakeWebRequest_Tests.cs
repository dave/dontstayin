using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Net;
namespace MSBuildTasks
{
    [TestFixture]
    public class MakeWebRequest_Tests
    {
        [Test]
        public void TestAgainstDsi()
        {
            MakeWebRequest mwr = new MakeWebRequest()
                                 {
                                     TextThatMustBeInResponse = "Welcome to DontStayIn",
                                     Url = @"http://www.dontstayin.com",
                                     TimeoutInSeconds = 10,
                                     MaxNumberOfAttempts = 2
                                 };
            Assert.IsTrue(mwr.Execute());
                                      

        }
    }
}
