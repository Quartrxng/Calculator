using System.Data;
using Calculator;

namespace CalculatorTest
{
    [TestClass]
    public sealed class Test
    {
        [TestMethod]

        [DataRow("1+1",2)]
        [DataRow("3/2", 1.5)]
        [DataRow("3-1", 2)]
        [DataRow("3- 1", 2)]
        [DataRow("6*6", 36)]
        [DataRow("6 * 6", 36)]
        [DataRow("6*6-6*3/2", 27)]
        [DataRow("3/0", double.PositiveInfinity)]
        [DataRow("-3/0", double.NegativeInfinity)]
        [DataRow("-3-1", -4)]
        [DataRow("-3 - 1 ", -4)]
        [DataRow("100 * 2 - 50 + 40 / -8", 145)]


        public void TestWithNumbers(string input, double output)
        {
            var Calc = new Program();
            var result = Calc.Calculator(input);
            Assert.AreEqual(output, result);
        }
    }
}
