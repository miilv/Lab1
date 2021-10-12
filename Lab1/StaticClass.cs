using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Lab1
{
    public static class StaticClass
    {
        public static Complex noize1(Vector2 v2)
        {
            Random rnd = new Random();
            Complex field = new Complex(rnd.Next(0, 100) * rnd.NextDouble(),  rnd.Next(0, 100) * rnd.NextDouble());
            return field;
        }
       
    }
    public delegate Complex Fv2Complex(Vector2 v2);
}
