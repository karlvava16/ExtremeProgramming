using System.Text;

namespace App
{
    public record RomanNumber(int Value)
    {
        private readonly int _value = Value; // TODO: Refactoring - exclude
        public int Value { get => _value; init => _value = value; }


        public static RomanNumber Parse(String input)
        {
            int value = 0;
            int prevDigit = 0;
            int pos = input.Length;
            int maxDigit = 0;
            bool isLess = false;

            List<String> errors = new();

            foreach (char c in input.Reverse())
            {
                pos -= 1;
                int digit;
                try
                {
                    digit = DigitalValue(c.ToString());
                }
                catch
                {
                    errors.Add($"Invalid symbol '{c}' in position {pos}");
                    continue;
                }

                if (digit != 0 && prevDigit / digit > 10)
                {
                    errors.Add($"Invalid order '{c}' before '{input[pos + 1]}' in position {pos}");
                }

                //// Detect invalid sequences with more than one less digit before a larger one
                //if (digit < prevDigit && prevDigit < maxDigit)
                //{
                //    errors.Add($"Invalid sequence: more than one smaller digit ('{input[pos]}') precedes larger digit '{input[pos + 1]}' at position {pos}");
                //    continue ;
                //}

                if (digit < maxDigit)
                {
                    if (isLess)
                    {
                        throw new FormatException($"Invalid sequence: more than one smaller digit before '{input[pos + 1]}' '{input}'");

                    }
                    isLess = true;
                }
                else
                {
                    maxDigit = digit;
                    isLess = false;
                }

                value += digit >= prevDigit ? digit : -digit;
                prevDigit = digit;
            }

            if (errors.Any())
            {
                throw new FormatException(string.Join("; ", errors));
            }

            return new RomanNumber(value);
        }




        public static int DigitalValue(String digit)
        {
            return digit switch
            {
                "N" => 0,
                "I" => 1,
                "V" => 5,
                "X" => 10,
                "L" => 50,
                "C" => 100,
                "D" => 500,
                "M" => 1000,
                 _  => throw new ArgumentException($" {nameof(RomanNumber)} : {nameof(DigitalValue)}: 'digit' has invalid value '{digit}'")
            };

        }

        public RomanNumber Plus(RomanNumber other)
        {
            return this with { Value = Value + other.Value };
        }
        public RomanNumber Plus(string other)
        {
            return this with { Value = Value + Parse(other).Value };
        }
        /*ДЗ Скласти тести, що перевіряють роботу метода Plus
         * з використанням римських записів чисел, наприклад
         * IV + VI = X
        */

        public override string? ToString()
        {
            //3343->MMMCCCXLIII
            // MMM
            // D (500) X
            // CD (400) X
            // CCC
            // LX
            // III
            if(_value == 0 ) return "N";
            Dictionary<int, string> parts = new ()
            {
                {1000, "M" },
                {900, "CM" },
                {500, "D" },
                {400, "CD" },
                {100, "C" },
                {90, "XC" },
                {50, "L"},
                {40, "XL"},
                {10, "X"},
                {9, "IX"},
                {5, "V"},
                {4, "IV"},
                {1, "I"},
            };


            int v = _value;

            StringBuilder sb = new ();

            foreach (var part in parts)
            {
                while(v >= part.Key)
                {
                    v -= part.Key;
                    sb.Append(part.Value);
                }
            }
            return sb.ToString();
        }
    }
}


/* Повторити основи управління програмними проєктами
 * Закласти рішення (solution) з двома проєктами (App, Test)
 * Налаштувати запуск тестів
 * *Реалізувати парсер одиночних цифр римських чисел, додати тести до них
 * Форми здачі:
 * -архів з проєктом та скріншотами або посилання на github
 */