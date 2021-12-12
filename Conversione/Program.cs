using System;

namespace Conversione
{
    class Program
    {
        static void Main(string[] args)
        {
            int     x=10;
            double  y=x;

            float   x1 = 12_000f;
            int     y1=(int)x1;

            int y2=y1++;

            int x2= Convert.ToInt32("12345");
            
            Console.WriteLine($"x={x} y={y}");
            Console.WriteLine($"x1={x1} y1={y1}"); 
            Console.WriteLine($"y2={y2} x2={x2}");
        }
    }
}
