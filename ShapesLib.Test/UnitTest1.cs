using NUnit.Framework;
using ShapesLib;

namespace ShapesLib.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void DeboObject_ToString_ReturnsCorrectValue()
        {
            var obj = new DemoClass();
            Assert.AreEqual(obj.ToString(), "I am object of demo class that defined in ShapesLib project");
        }
    }
}