using App;

namespace Test
{
    [TestClass]
    public class RomanNumberTest
    {
        [TestMethod]
        public void ParseTest()
        {
            var testCases = new Dictionary<string, int>()
        {
            { "N",      0 },
            { "I",      1 },
            { "II",     2 },
            { "III",    3 },
            { "IIII",   4 },
            { "IV",     4 },
            { "VI",     6 },
            { "VII",    7 },
            { "VIII",   8 },
            { "D",      500 },
            { "CM",     900 },
            { "M",      1000 },
            { "MC",     1100 },
            { "MCM",    1900 },
            { "MM",     2000 }
        };

            foreach (var testCase in testCases)
            {
                RomanNumber rn = RomanNumber.Parse(testCase.Key);
                Assert.IsNotNull(rn);
                Assert.AreEqual(testCase.Value
                    ,
                    rn.Value,
                    $"{testCase.Key} parsing failed. Expected {testCase.Value}, got {rn.Value}."
                );
            }
        }

        [TestMethod]
        public void DigitalValueTest()
        {
            var romanToInt = new Dictionary<string, int>()
        {
            { "N", 0 },
            { "I", 1 },
            { "V", 5 },
            { "X", 10 },
            { "L", 50 },
            { "C", 100 },
            { "D", 500 },
            { "M", 1000 }
        };

            foreach (var kvp in romanToInt)
            {


                Assert.AreEqual(
                    kvp.Value,
                    RomanNumber.DigitalValue(kvp.Key),
                    $"{kvp.Value} parsing failed. Expected {kvp}, got {kvp.Value}."
                );
            }
        }

    }

    /*
    Д.З. збільшити кількість тестових кейсів для ParsTest
    Використоувати як оптимальні, так і неоптимальні форми чисел.
    Перевірити працездатність шляхом включення неправильних кейсів
     
     
     */
}