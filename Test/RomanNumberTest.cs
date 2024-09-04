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
            { "MM",     2000 },

            //Not optimal
            { "XXXX",   40 },
            { "LL",     100 },
            { "CCCC",   400 },
            { "DDD",    1500 },
            
            // Incorrect
            //{ "IC",     -1 },
            //{ "IL",     -1 },
            //{ "XD",     -1 },
            //{ "VVVV",   -1 },
            //{ "MMMM",   -1 }
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

            Dictionary<string, (char, int)[]> exTestCases = new()
            {
                {"W", new[] {('W', 0)}},
                {"Q", new[] {('Q', 0)}},
                {"s", new[] {('s', 0)}},
                {"Xd", new[] {('d', 1)}},
                {"SWXF", new[] {('S', 0), ('W', 1), ('F', 3)}},
                {"XXFX", new[] {('F', 2)}},
                {"VVVFX", new[] {('F', 3)}},
                {"IVF", new[] {('F', 2)}},
            };

            foreach (var testCase in exTestCases)
            {
                var ex = Assert.ThrowsException<FormatException>(
                    () => RomanNumber.Parse(testCase.Key),
                    $"{nameof(FormatException)} Parse '{testCase.Key}' must throw");

                foreach (var (symbol, position) in testCase.Value)
                {
                    Assert.IsTrue(ex.Message.Contains($"Invalid symbol '{symbol}' in position {position}"),
                        $"{nameof(FormatException)} must contain data about symbol '{symbol}' at position {position}. " +
                        $"TestCase: '{testCase.Key}', ex.Message: '{ex.Message}'");
                }
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
            { "M", 1000 },
        };

            foreach (var kvp in romanToInt)
            {

                Assert.AreEqual(
                    kvp.Value,
                    RomanNumber.DigitalValue(kvp.Key),
                    $"{kvp.Value} parsing failed. Expected {kvp}, got {kvp.Value}."
                );
            }


            Random random = new Random();
            for (int i = 0; i < 100; i++)
            {
                String invalidDigit = ((char)random.Next(256)).ToString();


                if (romanToInt.ContainsKey(invalidDigit))
                {
                    i--;
                    continue;
                }


                ArgumentException ex = Assert.ThrowsException<ArgumentException>(
                () => RomanNumber.DigitalValue(invalidDigit),
                $"ArgumentException erxpected for digit = '{invalidDigit}'"
                 );

                // вимагатимемо від винятку
                // повідомлення, що
                // містить назву аргументу (digit)
                // містить значення аргументу, що призвело до винятку
                // назву класу та метододу, що викинуло виняток

                Assert.IsFalse(
                    String.IsNullOrEmpty(ex.Message),
                    "ArgumentException must have a message"
                    );
                Assert.IsTrue(
                    ex.Message.Contains($"'digit' has invalid value '{invalidDigit}'"),
                    $"ArgumentException message must contain <'digit' has invalid value '{invalidDigit}'>"
                    );
                Assert.IsTrue(
                    ex.Message.Contains(nameof(RomanNumber)) &&
                    ex.Message.Contains(nameof(RomanNumber.DigitalValue)),
                    $"ArgumentException message must contain '{nameof(RomanNumber)}' and '{nameof(RomanNumber.DigitalValue)}'"
                    );
            }
        }

    }
}


/*
    Д.З. збільшити кількість тестових кейсів виняткових ситуацій ParsTest
   з різною довжиною послідовностей.

    * забезпечити виведннення першої позиції помилки(за наявності кількох)
     наприклад, парс("SWXF") має вивести помилку на позиції 0, літер 'S'
     
    ** за наявності кількох помилок вивести усі неправильні символи та їх позиції
     та їх позиції парс('SWXF') -> 'S'(0), 'S'(1), 'F'(3)
     
*/