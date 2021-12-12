using System;

ARecord ar = new ARecord ("L'Isola Misteriosa", "Jules Verne");
ARecord ar2 = new ARecord ("L'Isola Misteriosa", "Jules Verne");
Console.WriteLine(ar.Equals(ar2));
BRecord br = new BRecord ("L'Isola Misteriosa", "Jules Verne");
Console.WriteLine(ar.Equals(br));

Libro libro1 = new Libro("L'Isola Misteriosa", "Jules Verne");
Console.WriteLine(libro1);

LibroDigitale libro2 = new LibroDigitale("Neuromante", "William Gibson", "8 ore");
Console.WriteLine(libro2);

record Libro(string Titolo, string Autore);
record LibroDigitale(string Titolo, string Autore, string durata) : Libro(Titolo, Autore);

record ARecord(string Titolo, string Autore);
record BRecord(string Titolo, string Autore) : ARecord(Titolo, Autore);