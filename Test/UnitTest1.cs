using App;

namespace Test
{
    [TestClass]
    public class RomanNumberTest
    {
        [TestMethod]
        public void ParseTest()
        {
            List<int> rightAnswers = new List<int>() { 1, 5, 10, 50, 100, 500, 1000 };
            List<string> numbersRome = new List<string>() { "I", "V", "X", "L", "C", "D", "M" };

            for (int i = 0; i < rightAnswers.Count; i++)
            {
                Assert.IsNotNull(numbersRome[i]);
                Assert.AreEqual(rightAnswers[i], RomanNumber.Parse(numbersRome[i]).Value, numbersRome[i] + " parsing failed");

            }
        }
    }
}