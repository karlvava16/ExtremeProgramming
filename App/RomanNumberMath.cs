using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    public static class RomanNumberMath
    {
        public static RomanNumber Plus(params RomanNumber[] args)
        {
            return new(args.Sum(r => r.Value));
        }
    }
}
