using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;


namespace Bobs
{
    [TestFixture]
    public class ExampleTests
    {

        #region Setup/Teardown
        #region Fixture
        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {

        }
        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {

        }


        #endregion
        #region Test
        [SetUp]
        public void Setup()
        {

        }
        [TearDown]
        public void TearDown()
        {

        }
        #endregion
        #endregion

        [Test]
        public void TestsAreFunctioningTest()
        {
            Assert.IsTrue(true);
        }

    }


}
