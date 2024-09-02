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
            return input switch
            {
                "I" => new(1),
                "V" => new(5),
                "X" => new(10),
                "L" => new(50),
                "C" => new(100),
                "D" => new(500),
                "M" => new(1000),
                _ => throw new ArgumentException("Invalid Roman numeral")
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