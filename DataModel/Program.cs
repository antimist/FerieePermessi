using System;
using System.Text;
// See https://aka.ms/new-console-template for more information

Libro libro1 = new Libro("L'Isola Misteriosa", "Jules Verne");
//Libro libro2 = new Libro("Neuromante", "William Gibson");
//Libro libro2 = new Libro("L'Isola Misteriosa", "Jules Verne");
Libro libro2 = libro1 with {Autore="Giulio Verne"};

Console.WriteLine(libro1==libro2);
Console.WriteLine(libro1);
record Libro 
{
    public string  Titolo{get; init;}
    public string  Autore{get; init;}

    public Libro(){}
    public Libro(string titolo, string autore)
    {
        Titolo=titolo;
        Autore=autore;
    }

    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append ("RECORD: Libro - ");
        stringBuilder.Append (Titolo);
        return stringBuilder.ToString();
    }    
}
     







