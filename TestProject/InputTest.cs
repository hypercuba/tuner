using GlobalApp.TunerLib.Classes;
using NUnit.Framework;

namespace TestProject
{
    [TestFixture]
    public class InputTest
    {
        [Test]
        public void BeginTest() 
        {
            Input.Instance.Begin(3);
            Assert.IsTrue(true);
        }
    }
}
