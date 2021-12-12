using System;

namespace matematica
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write(("Inserisci un numero: "));
            string s = Console.ReadLine();
            int n = Convert.ToInt32(s);
            int pippo = Fattoriale(n);
            Console.WriteLine($"Il fattoriale di {n} è {pippo}");
        }
        static int Fattoriale (int n)
        {
            // implementare il fattoriale di un numero intero
            /* il fattoriale di un numero intero è il prodotto
             * di tutti i numeri che lo precedono
             * esempio fattoriale di 5 = 5*4*3*2*1
             * per convenzione 0! = 1
            */ 
            if (n == 0)
            {
                return 1; // caso fattoriale di 0
            }                 
            return n * Fattoriale(n-1);
        }
    }
}