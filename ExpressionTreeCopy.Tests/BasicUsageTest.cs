using System;
using NUnit.Framework;

namespace Redmanmale.ExpressionTreeCopy.Tests
{
    [TestFixture]
    public class BasicUsageTest
    {
        private const string NewStringValue = "newValue";

        private Data _testData;

        private readonly DataUpdater _dataUpdater = new DataUpdater();

        [SetUp]
        public void OnStart()
        {
            _testData = new Data
            {
                SomeOtherData = new OtherData
                {
                    SomeOtherIntProp = 42,
                    SomeOtherStringProp = "def"
                }
            };
        }

        private void CheckValues()
        {
            Assert.AreEqual(NewStringValue, _testData.SomeOtherData.SomeOtherStringProp);
            Assert.AreEqual(42, _testData.SomeOtherData.SomeOtherIntProp);
        }

        [Test]
        public void Case0_Test()
        {
            Console.Write("Test-0 [stupid copy]: ");
            _dataUpdater.Update0(_testData, NewStringValue);
            CheckValues();
            Console.WriteLine("ok");
        }

        [Test]
        public void Case1_Test()
        {
            Console.Write("Test-1 [constructor copy]: ");
            _dataUpdater.Update1(_testData, NewStringValue);
            CheckValues();
            Console.WriteLine("ok");
        }

        [Test]
        public void Case2_Test()
        {
            Console.Write("Test-2 [static method]: ");
            _dataUpdater.Update2(_testData, NewStringValue);
            CheckValues();
            Console.WriteLine("ok");
        }

        [Test]
        public void Case3_Test()
        {
            Console.Write("Test-3 [reflection]: ");
            _dataUpdater.Update3(_testData, NewStringValue);
            CheckValues();
            Console.WriteLine("ok");
        }

        [Test]
        public void Case4_Test()
        {
            Console.Write("Test-4 [expression tree]: ");
            _dataUpdater.Update4(_testData, NewStringValue);
            CheckValues();
            Console.WriteLine("ok");
        }
    }
}
