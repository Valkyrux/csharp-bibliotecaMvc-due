using Biblioteca_EntityFramework.Models;
using System;
using System.Linq;

namespace Biblioteca_EntityFramework
{
    public static class DbInitializer
    {
        internal static void Initialize(LibraryContext context)
        {
            //E' il metodo di EF che crea il database SOLO se già non c'è
            context.Database.EnsureCreated();

            // Look for any user
            if (context.Utenti.Any())
            {
                return;   // DB has been seeded
            }

            Utente[] utenti = new Utente[]
            {
                new Utente{Nome = "Carson", Cognome = "Alexander", Email = "email@email.mail"},
                new Utente{Nome = "Meredith", Cognome = "Alonso", Email = "email@email.mail"},
                new Utente{Nome = "Arturo", Cognome = "Anand", Email = "email@email.mail"},
                new Utente{Nome = "Gytis", Cognome = "Barzdukas", Email = "email@email.mail"},
                new Utente{Nome = "Yan", Cognome = "Li", Email = "email@email.mail"},
                new Utente{Nome = "Peggy", Cognome = "Justice", Email = "email@email.mail"},
                new Utente{Nome = "Laura", Cognome = "Norman", Email = "email@email.mail"},
                new Utente{Nome = "Nino", Cognome = "Olivetto"}
            };
            foreach (Utente utente in utenti )
            {
                context.Utenti.Add(utente);
            }
            context.SaveChanges();
           
            var autori = new Autore[]
            {
                new Autore{ Nome = "Alessandro", Cognome = "Manzoni", DataNascita = DateTime.Now },
                new Autore{ Nome = "Ken", Cognome = "Follet", DataNascita = DateTime.Now },
                new Autore{ Nome = "Conan", Cognome = "Doyle", DataNascita = DateTime.Now },
                new Autore{ Nome = "Dan", Cognome = "Brown", DataNascita = DateTime.Now },
                new Autore{ Nome = "Stephen", Cognome = "King", DataNascita = DateTime.Now },
                new Autore{ Nome = "Ernest", Cognome = "Hamingway", DataNascita = DateTime.Now },
                new Autore{ Nome = "Leonardo", Cognome = "Sciascia", DataNascita = DateTime.Now }
            };
            foreach (Autore autore in autori)
            {
                context.Autori.Add(autore);
            }
            context.SaveChanges();

            var Manzoni = context.Autori.Where(item => item.Cognome == "Manzoni").First();
            var Sciascia = context.Autori.Where(item => item.Cognome == "Sciascia").First();
            var Follet = context.Autori.Where(item => item.Cognome == "Follet").First();
            var Doyle = context.Autori.Where(item => item.Cognome == "Doyle").First();
            var King = context.Autori.Where(item => item.Cognome == "King").First();
            var Hamingway = context.Autori.Where(item => item.Cognome == "Hamingway").First();
            var Brown = context.Autori.Where(item => item.Cognome == "Brown").First();

            var libri = new Libro[]
            {
                new Libro{ Titolo = "I promessi sposi", Autori = new List<Autore>{Manzoni}, Data = DateTime.Now, ISBN = "iita2352520", numeroDiPagine = 200, stato = Stato.Disponibile},
                new Libro{ Titolo = "Il Codice da Vinci", Autori = new List<Autore>{Brown}, Data = DateTime.Now, ISBN = "gaghahr520", numeroDiPagine = 400, stato = Stato.Disponibile},
                new Libro{ Titolo = "Il vecchio e il mare", Autori = new List<Autore>{Hamingway}, Data = DateTime.Now, ISBN = "99iitwetwt0", numeroDiPagine = 300, stato = Stato.Disponibile},
                new Libro{ Titolo = "Sherlock Holmes", Autori = new List<Autore>{Doyle}, Data = DateTime.Now, ISBN = "isdhhsh20", numeroDiPagine = 500, stato = Stato.Disponibile},
                new Libro{ Titolo = "Il giorno della Civetta", Autori = new List<Autore>{Sciascia}, Data = DateTime.Now, ISBN = "hshfsithshs0", numeroDiPagine = 500, stato = Stato.Disponibile},
                new Libro{ Titolo = "Una collaborazione improponibile", Autori = new List<Autore>{Brown, Manzoni}, Data = DateTime.Now, ISBN = "hshsfhy0", numeroDiPagine = 400, stato = Stato.Disponibile},
                new Libro{ Titolo = "Un libro per 3", Autori = new List<Autore>{Sciascia, Doyle, Brown}, Data = DateTime.Now, ISBN = "yy5uhshssdf0", numeroDiPagine = 500, stato = Stato.Disponibile}
            };

            foreach (Libro libro in libri)
            {
                context.Libri.Add(libro);
            }

            context.SaveChanges();

            for(int i = 0; i < 100; i++)
            {
                var libro = context.Libri.OrderBy(item => Guid.NewGuid()).First();
                var utente = context.Utenti.OrderBy(item => Guid.NewGuid()).First();
                Prestito prestito = new Prestito() { data = DateTime.Now };
                if (libro.Prestiti != null)
                {
                    libro.Prestiti.Add(prestito);
                }
                else
                {
                    libro.Prestiti = new List<Prestito>() { prestito };
                }

                if(utente.Prestiti != null)
                {
                    utente.Prestiti.Add(prestito);
                }
                else
                {
                    utente.Prestiti = new List<Prestito>() { prestito };
                }
                context.Prestiti.Add(prestito);
            }
            context.SaveChanges();
            /*
            var enrollments = new Enrollment[]
            {
            new Enrollment{StudentID=1,CourseID=1050,Grade=Grade.A},
            new Enrollment{StudentID=1,CourseID=4022,Grade=Grade.C},
            new Enrollment{StudentID=1,CourseID=4041,Grade=Grade.B},
            new Enrollment{StudentID=2,CourseID=1045,Grade=Grade.B},
            new Enrollment{StudentID=2,CourseID=3141,Grade=Grade.F},
            new Enrollment{StudentID=2,CourseID=2021,Grade=Grade.F},
            new Enrollment{StudentID=3,CourseID=1050},
            new Enrollment{StudentID=4,CourseID=1050},
            new Enrollment{StudentID=4,CourseID=4022,Grade=Grade.F},
            new Enrollment{StudentID=5,CourseID=4041,Grade=Grade.C},
            new Enrollment{StudentID=6,CourseID=1045},
            new Enrollment{StudentID=7,CourseID=3141,Grade=Grade.A},
            };
            foreach (Enrollment e in enrollments)
            {
                context.Enrollments.Add(e);
            }
            context.SaveChanges();*/
        }
    }
}