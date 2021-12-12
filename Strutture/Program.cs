using System;

namespace Strutture
{
    public class MyClass
    {
        public string myString;
        public int myInteger;

    }

    public struct MyStruct 
    {
        public string myString;
        public int myInteger;
    }    
    class Program
    {
        static void Main(string[] args)
        {
            MyClass mc = new MyClass();
            MyStruct ms =  new MyStruct();

            mc.myString = "Alessandro"; mc.myInteger= 50;
            ms.myString = "Susanna"; ms.myInteger= 150;
            
            Console.WriteLine($"Prima {mc.myString} {mc.myInteger}");
            Console.WriteLine($"Prima {ms.myString} {ms.myInteger}");

            classMethod(mc, ms, 50, "Mario");

            Console.WriteLine($"Dopo {mc.myString} {mc.myInteger}");
            Console.WriteLine($"Dopo {ms.myString} {ms.myInteger}");            


        }

        static void classMethod(MyClass mc, MyStruct ms, int intValue, string strValue)
        {
            mc.myInteger= intValue;
            mc.myString = strValue;

            ms.myInteger= intValue;
            ms.myString=strValue;

        }        
    }
}
