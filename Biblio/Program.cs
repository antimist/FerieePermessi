using System;

namespace Biblio
{
    public class Persona
    {
        //proprietà
        public string Nome {get; set;}
        public string Cognome {get; set;}
        public virtual string Denominazione
        {
            get {
                return Nome + " " + Cognome;
            }
        }
    }

    public class Utente : Persona
    {
        public string Id {get; set;}
        public int    AnnoIscrizione {get; set;}

        public override string Denominazione {
            get {
                return Id + " " + Nome + " " + Cognome;
            }
        }
    }
    public class Libro    
    {
        //attributi
        private string  id;
        private string titolo;
        private string autore;
        private Utente utente;
        //costruttore
        public Libro (string id, string titolo, string autore)
        {
            this.id=id;
            this.titolo=titolo;
            this.autore = autore;
            this.utente = null;
        }

        //metodi    
        public string Descrizione()
        {
            return  string.Format($"{titolo} di {autore}");
        }

        public void Prestito(Utente utente)
        {
            if (this.utente !=null){
                Console.WriteLine($"Titolo è gia in prestito a {this.utente.Denominazione}");
                return;
            }

            this.utente = utente;
            Console.WriteLine($"Titolo prestato a {this.utente.Denominazione}");
            /*
            foreach (Libro l in Libro)
            {
                if (l.id = id)
                {
                    l.utente = utente;
                }
            }
            */
       }
 
        public void Restituzione()
        {
            Console.WriteLine($"{this.utente.Denominazione} ha eseguito la restituzione");
            
            // foreach (Libro l in Libro)
            // {
            //    if (l.id = id)
            //    {
                    this.utente = null;
            //    }
            //}
        }

    }
    public class Biblioteca
    {
        static void Main(string[] args)
        {
            Utente utente1 = new Utente {Id="0001", Nome="Mario", Cognome="Rossi" , AnnoIscrizione=2015};
            Console.WriteLine(utente1.Denominazione);

            Utente utente2 = new Utente {Id="0002", Nome="Giuseppe", Cognome="Verdi" , AnnoIscrizione=2010};
            Console.WriteLine(utente2.Denominazione);

            Libro libro = new Libro("L001", "L'isola misteriosa", "Giulio Verne" );
            Console.WriteLine(libro.Descrizione() );

            libro.Prestito(utente1);
            libro.Prestito(utente2);

            libro.Restituzione();
            libro.Prestito(utente2);
        }
    }
}
