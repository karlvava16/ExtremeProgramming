using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    public record RomanNumber (int Value)
    {
        private readonly int _value = Value;
        public int Value => _value;


        public static RomanNumber Parse(String input)
        {
           int value = 0;
            int prevDigit = 0;
            foreach (char c in input.Reverse())
            {
                int digit = DigitalValue(c.ToString());
                value += digit >= prevDigit ? digit : -digit;
                prevDigit = digit;
            }
            return new(value);
        }

        public static int DigitalValue(String input)
        {
            return input switch
            {
                "N" => 0,
                "I" => 1,
                "V" => 5,
                "X" => 10,
                "L" => 50,
                "C" => 100,
                "D" => 500,
                 _  => 1000
                //"M" => new(1000),
                //_ => throw new ArgumentException("Invalid Roman numeral")
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