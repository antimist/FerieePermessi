using System;

namespace Asterischi
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Quante rige: ");
            string s = Console.ReadLine();
            while(true)
            {
                if (s=="no") 
                {
                    break; // esce dal ciclo
                }
                
                int j = Convert.ToInt32(s);
                for (int r=1; r<=j; r++)
                {
                    for (int ast=1; ast<=r; ast++)
                    {
                        Console.Write("* ");
                    }
                    Console.Write("\n");
                }
                

                break;
            }
            
        }
    }
}
