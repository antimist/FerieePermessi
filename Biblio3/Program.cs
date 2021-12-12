using System;

namespace Biblio3
{
    public interface IUtente {
        string ID {get; set;}
        int AnnoIscrizione {get; set;}
        string Denominazione  {get;}
    }
    public class Persona : IUtente
    {
        public string ID {get; set;}
        public int AnnoIscrizione {get; set;}
        public string Nome {get; set;}
        public string Cognome {get; set;}
 
         public string Denominazione {
            get {
                return ID + " " + Nome + " " + Cognome;
            }
        }
    }
    public class Organizzazione : IUtente
    {
        public string ID {get; set;}
        public int AnnoIscrizione {get; set;}
        public string RragioneSociale {get; set;}
        public string Denominazione {
            get { 
                return ID + " " + RragioneSociale;
            }
        }                            
    }

    public class Biblioteca
    {
        public static IUtente[] utenti;
        static void stampaUtente()
        {
            foreach (IUtente ute in utenti) 
            {
                Console.WriteLine($"{ute.Denominazione}");
            }
        }

        static void Main(string[] args)
        {
            Persona persona = new Persona();
            Organizzazione organizzazione = new Organizzazione();

            persona.ID = "0001";
            persona.Nome = "Mario";
            persona.Cognome="Rossi";
            persona.AnnoIscrizione =2016;

            organizzazione.ID ="0002";
            organizzazione.RragioneSociale ="Grandi Lettori srl";
            organizzazione.AnnoIscrizione=2010;

            utenti=new IUtente[] {persona, organizzazione};

           stampaUtente();
        }        
    }

}
