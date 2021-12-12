using System;

namespace ArrayQuadrato
{
    class Program
    {
        static void Main(string[] args)
        {
            int [,]a = new int[5,5];

            int conta=0;
            for (int i=0; i<5; i++) 
            {
                for(int j=0; j<5; j++)
                {
                    ++conta;
                    a[i,j]=conta;
                    if(j==0)
                    {
                        a[i,j]=0;
                    }
                    Console.Write($"{a[i,j]} ");               
                }
                Console.Write("\n");
            }
          
        }
    }
}
