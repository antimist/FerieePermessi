using System;

namespace Condizioni
{
    class Program
    {
        static void Main(string[] args)
        {
            string s="";
            Console.Write("Scrivi qualcosa: ");
            s=Console.ReadLine();
            
            if(s=="primo")
                {Console.WriteLine("Hai scritto 'PRIMO'");}
            else if(s=="secondo")
                {Console.WriteLine("Hai scritto 'SECONDO'");}
            else
                {Console.WriteLine($"Hai scritto '{s}'");}

            switch (s)
            {
                case "primo":
                    Console.WriteLine("Hai scritto 'PRIMO'");
                    break;
                case "secondo":                    
                    Console.WriteLine("Hai scritto 'SECONDO'");
                    break;
                default:
                    Console.WriteLine($"Hai scritto '{s}'");
                    break;
            }
       }
    }
}
