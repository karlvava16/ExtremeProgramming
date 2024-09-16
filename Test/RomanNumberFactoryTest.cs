using System.Collections.ObjectModel;
using System.Reflection;

namespace App;

[TestClass]
public class RomanNumberFactoryTest
{
    public static ReadOnlyDictionary<string, int> DigitValues => new(new Dictionary<String, int>
    {
        { "N", 0    },
        { "I", 1    },
        { "V", 5    },
        { "X", 10   },
        { "L", 50   },
        { "C", 100  },
        { "D", 500  },
        { "M", 1000 },
    });



    [TestMethod]
    public void _CheckSymbolsTest()
    {
        Type? rnType = typeof(RomanNumberFactory);
        MethodInfo? m1Info = rnType.GetMethod("_CheckSymbols",
            BindingFlags.NonPublic | BindingFlags.Static);

        m1Info?.Invoke(null, ["IX"]);

        var ex = Assert.ThrowsException<TargetInvocationException>(
        () => m1Info?.Invoke(null, ["IW"]),
            $"_CheckSymbols 'IW' must throw FormatException"
        );
        Assert.IsInstanceOfType<FormatException>(
            ex.InnerException,
            "FormatException from InnerException"
        );
    }

    [TestMethod]
    public void _CheckPairsTest()
    {
        Type? rnType = typeof(RomanNumberFactory);
        MethodInfo? m1Info = rnType.GetMethod("_CheckPairs",
            BindingFlags.NonPublic | BindingFlags.Static);

        // Assert Not Throws
        m1Info?.Invoke(null, ["IX"]);

        var ex = Assert.ThrowsException<TargetInvocationException>(
        () => m1Info?.Invoke(null, ["IM"]),
            $"_CheckPairs 'IM' must throw FormatException"
        );
        Assert.IsInstanceOfType<FormatException>(
            ex.InnerException,
            "FormatException from InnerException"
        );
    }

    [TestMethod]
    public void _CheckFormatTest()
    {
        Type? rnType = typeof(RomanNumberFactory);
        MethodInfo? m1Info = rnType.GetMethod("_CheckFormat",
            BindingFlags.NonPublic | BindingFlags.Static);

        // Assert Not Throws
        m1Info?.Invoke(null, ["IX"]);

        var ex = Assert.ThrowsException<TargetInvocationException>(
        () => m1Info?.Invoke(null, ["IIX"]),
            $"_CheckFormat 'IIX' must throw FormatException"
        );
        Assert.IsInstanceOfType<FormatException>(
            ex.InnerException,
            "_CheckFormat: FormatException from InnerException"
        );
    }

    [TestMethod]
    public void _CheckValidityTest()
    {
        Type? rnType = typeof(RomanNumberFactory);
        MethodInfo? m1Info = rnType.GetMethod("_CheckValidity",
            BindingFlags.NonPublic | BindingFlags.Static);

        // Assert Not Throws
        m1Info?.Invoke(null, ["IX"]);

        String[] testCases = ["IXIX", "IXX", "IVIV", "XCC", "IXIV", "XCXL", "CMCD"];
        foreach (var testCase in testCases)
        {
            var ex = Assert.ThrowsException<TargetInvocationException>(
            () => m1Info?.Invoke(null, [testCase]),
                $"_CheckValidity '{testCase}' must throw FormatException"
            );
            Assert.IsInstanceOfType<FormatException>(
                ex.InnerException,
                "_CheckValidity: FormatException from InnerException"
            );
        }
    }

    [TestMethod]
    public void ParseTest()
    {

        var Assert_ThrowsException_Methods = typeof(Assert).GetMethods()
        .Where(x => x.Name == "ThrowsException")
        .Where(x => x.IsGenericMethod);

        var Assert_ThrowsException_Func_String_method = Assert_ThrowsException_Methods.Skip(3).FirstOrDefault();

        TestCase[] testCases1 = [
            new() { Source = "N",    Value = 0 },
            new() { Source = "I",    Value = 1 },
            new() { Source = "II",   Value = 2 },
            new() { Source = "III",  Value = 3 },
            new() { Source = "IIII", Value = 4 },
            new() { Source = "IV",   Value = 4 },
            new() { Source = "VI",   Value = 6 },
            new() { Source = "VII",  Value = 7 },
            new() { Source = "VIII", Value = 8 },
            new() { Source = "IX",   Value = 9 },
            new() { Source = "D",    Value = 500 },
            new() { Source = "M",    Value = 1000 },
            new() { Source = "CM",   Value = 900 },
            new() { Source = "MC",   Value = 1100 },
            new() { Source = "MCM",  Value = 1900 },
            new() { Source = "MM",   Value = 2000 },
        ];

        foreach (var testCase in testCases1)
        {
            RomanNumber rn = RomanNumberFactory.Parse(testCase.Source);
            Assert.IsNotNull(rn);
            Assert.AreEqual(
                testCase.Value,
                rn.Value,
                $"{testCase.Source} -> {testCase.Value}"
            );
        }

        var formatExceptionType = typeof(FormatException);
        String partTemplate = "Invalid symbol '{0}' in position {1}";
        TestCase[] exTestCases1 = [
            new() { Source = "W",   ExceptionMessageParts = [String.Format(partTemplate, "W",  0)], ExceptionType = formatExceptionType},
            new() { Source = "Q",   ExceptionMessageParts = [String.Format(partTemplate, "Q",  0)], ExceptionType = formatExceptionType},
            new() { Source = "s",   ExceptionMessageParts = [String.Format(partTemplate, "s",  0)], ExceptionType = formatExceptionType},
            new() { Source = "sX",  ExceptionMessageParts = [String.Format(partTemplate, "s",  0)], ExceptionType = formatExceptionType},
            new() { Source = "Xd",  ExceptionMessageParts = [String.Format(partTemplate, "d",  1)], ExceptionType = formatExceptionType},

        ];

        foreach (var testCase in exTestCases1)
        {


            dynamic? ex = Assert_ThrowsException_Func_String_method?
                .MakeGenericMethod(testCase.ExceptionType!)
                .Invoke(null,
                [() => RomanNumberFactory.Parse(testCase.Source),
                $"Parse '{testCase.Source}' must throw FormatException"]);

            Assert.IsTrue(
                ex!.Message.Contains(
                    testCase.ExceptionMessageParts!.First()
                    ),
                $"FormatException must contain '{testCase.ExceptionMessageParts!.First()}'"
                );
        }

        TestCase[] exTestCases2 = {
        new() { Source = "IM",  ExceptionMessageParts = new[] { "Invalid order 'I' before 'M' in position 0" }, ExceptionType = formatExceptionType },
        new() { Source = "XIM", ExceptionMessageParts = new[] { "Invalid order 'I' before 'M' in position 1" }, ExceptionType = formatExceptionType },
        new() { Source = "IMX", ExceptionMessageParts = new[] { "Invalid order 'I' before 'M' in position 0" }, ExceptionType = formatExceptionType },
        new() { Source = "XMD", ExceptionMessageParts = new[] { "Invalid order 'X' before 'M' in position 0" }, ExceptionType = formatExceptionType },
        new() { Source = "XID", ExceptionMessageParts = new[] { "Invalid order 'I' before 'D' in position 1" }, ExceptionType = formatExceptionType },
        new() { Source = "VX",  ExceptionMessageParts = new[] { "Invalid order 'V' before 'X' in position 0" }, ExceptionType = formatExceptionType },
        new() { Source = "VL",  ExceptionMessageParts = new[] { "Invalid order 'V' before 'L' in position 0" }, ExceptionType = formatExceptionType },
        new() { Source = "LC",  ExceptionMessageParts = new[] { "Invalid order 'L' before 'C' in position 0" }, ExceptionType = formatExceptionType },
        new() { Source = "DM",  ExceptionMessageParts = new[] { "Invalid order 'D' before 'M' in position 0" }, ExceptionType = formatExceptionType }
    };

        foreach (var testCase in exTestCases2)
        {
            var ex = Assert.ThrowsException<FormatException>(
                () => RomanNumberFactory.Parse(testCase.Source),
                $"Parse '{testCase.Source}' must throw FormatException"
            );
            Assert.IsTrue(
                ex.Message.Contains(testCase.ExceptionMessageParts!.First()),
                "FormatException must contain data about mis-ordered symbols and its position"
            );
        }

        TestCase[] exTestCases4 = {
        new() { Source = "IXIX", ExceptionMessageParts = new[] { "Invalid" }, ExceptionType = formatExceptionType },
        new() { Source = "IXX",  ExceptionMessageParts = new[] { "Invalid" }, ExceptionType = formatExceptionType },
        new() { Source = "IXIV", ExceptionMessageParts = new[] { "Invalid" }, ExceptionType = formatExceptionType },
        new() { Source = "XCXC", ExceptionMessageParts = new[] { "Invalid" }, ExceptionType = formatExceptionType },
        new() { Source = "CMM",  ExceptionMessageParts = new[] { "Invalid" }, ExceptionType = formatExceptionType },
        new() { Source = "CMCD", ExceptionMessageParts = new[] { "Invalid" }, ExceptionType = formatExceptionType },
        new() { Source = "XCXL", ExceptionMessageParts = new[] { "Invalid" }, ExceptionType = formatExceptionType },
        new() { Source = "XCC",  ExceptionMessageParts = new[] { "Invalid" }, ExceptionType = formatExceptionType },
        new() { Source = "XCCI", ExceptionMessageParts = new[] { "Invalid" }, ExceptionType = formatExceptionType }
    };

        foreach (var testCase in exTestCases4)
        {
            var ex = Assert.ThrowsException<FormatException>(
                () => RomanNumberFactory.Parse(testCase.Source),
                $"Parse '{testCase.Source}' must throw FormatException"
            );
            Assert.IsTrue(ex.Message.Contains("Invalid"),
                $"Exception message for '{testCase.Source}' must contain 'Invalid'");
        }

        //String[] exTestCases3 =
        //{
        //    "IXC", "IIX", "VIX",
        //    "CIIX", "IIIX", "VIIX",
        //    "VIXC", "IVIX", "CVIIX",  // XIX+ CIX+ IIX- VIX-
        //    "CIXC", "IXCM", "IXXC",
        //};
        //foreach (var testCase in exTestCases3)
        //{
        //    var ex = Assert.ThrowsException<FormatException>(
        //        () => RomanNumberFactory.Parse(testCase),
        //        $"Parse '{testCase}' must throw FormatException"
        //    );
        //    Assert.IsTrue(
        //             ex.Message.Contains(nameof(RomanNumber)) &&
        //             ex.Message.Contains(nameof(RomanNumberFactory.Parse)) &&
        //             ex.Message.Contains(
        //                 $"invalid sequence: more than 1 less digit before '{testCase[^1]}'"),
        //             $"ex.Message must contain info about origin, cause and data. {ex.Message}"
        //         );
        //}

        /*
         Скласти тести (вислови) на проходження групи exTestCases4,
         а також на вміст повідомлень про винятки.
         Реалізувати (модифікувати) алгоритм парсера для проходження
         тестів.
         */
    }

    [TestMethod]
    public void DigitValueTest()
    {
        foreach (var testCase in DigitValues)
        {
            Assert.AreEqual(
                testCase.Value,
                RomanNumberFactory.DigitValue(testCase.Key),
                $"{testCase.Key} -> {testCase.Value}"
            );
        }
        Random random = new();
        for (int i = 0; i < 100; ++i)
        {
            String invalidDigit = ((char)random.Next(256)).ToString();
            if (DigitValues.ContainsKey(invalidDigit))
            {
                --i;
                continue;
            }
            ArgumentException ex =
                Assert.ThrowsException<ArgumentException>(
                    () => RomanNumberFactory.DigitValue(invalidDigit),
                    $"ArgumentException expected for digit = '{invalidDigit}'"
                );

            Assert.IsFalse(
                String.IsNullOrEmpty(ex.Message),
                "ArgumentException must have a message"
            );
            Assert.IsTrue(
                ex.Message.Contains($"'digit' has invalid value '{invalidDigit}'"),
                "ArgumentException message must contain <'digit' has invalid value ''>"
            );
            Assert.IsTrue(
                ex.Message.Contains(nameof(RomanNumberFactory)) &&
                ex.Message.Contains(nameof(RomanNumberFactory.DigitValue)),
                $"ArgumentException message must contain '{nameof(RomanNumberFactory)}' and '{nameof(RomanNumberFactory.DigitValue)}' "
            );
        }


    }

    /* Д.З. Перевести тест-кейси exTestCases3-5 до форми з уніфікованим
    * інтерфейсом TestCase. Забезпечення проходження усіх тестів.
    * 
    * Отримати / перевірити наявність навчальної ліцензії 
    * https://www.jetbrains.com/shop/eform/students
    * Заповнити форму
    * !! вказати університетську пошту (...@student.itstep.org)
    * Слідувати інструкціям з пошти.
    */

    class TestCase
    {
        public String Source { get; set; }
        public int? Value { get; set; }
        public Type? ExceptionType { get; set; }
        public IEnumerable<String>? ExceptionMessageParts { get; set; }

    }

    //static class AssertExtension
    //{
    //    static Exception ThrowsNonGeneric(this Exception ex)
    //}
}