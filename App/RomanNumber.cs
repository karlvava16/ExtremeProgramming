namespace App
{
    public record RomanNumber(int Value)
    {
        private readonly int _value = Value;
        public int Value => _value;


        public static RomanNumber Parse(String input)
        {
            int value = 0;
            int prevDigit = 0;
            int pos = input.Length;
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
                    throw new FormatException($"Invalid symbol '{c}' in position {pos}");
                }
                value += digit >= prevDigit ? digit : -digit;
                prevDigit = digit;
            }
            return new(value);
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
                _ => throw new ArgumentException($" {nameof(RomanNumber)} : {nameof(DigitalValue)}'digit' has invalid value '{digit}'")
            };

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