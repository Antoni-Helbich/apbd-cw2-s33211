using apbd_cw2_s33211.Sprzety;
namespace apbd_cw2_s33211;

public class Serwis
{
    List<Uzytkownik> Uzytkownicy {get;}
    List<Sprzet> Sprzety {get;}
    List<Wypozyczenie> Wypozyczenia {get;}

    public Serwis()
    {
        Uzytkownicy = new List<Uzytkownik>();
        Sprzety = new List<Sprzet>();
        Wypozyczenia = new List<Wypozyczenie>();
    }

    public void DodajUzytkownika(Uzytkownik uzytkownik)
    {
        Uzytkownicy.Add(uzytkownik);
    }
    public void DodajSprzet(Sprzet sprzet)
    {
        Sprzety.Add(sprzet);
    }

    public void WyswietlUzytkownikow()
    {
        foreach (var uzytkownik in Uzytkownicy)
        {
            Console.WriteLine(uzytkownik);
        }
        
    }
    public void WyswietlSprzet()
    {
        foreach (var sprzet in Sprzety)
        {
            Console.WriteLine(sprzet);
        }
    }

    public void WypozyczSprzet(string idSprzet, string idUzytkownika)
    {
        Sprzet? sprzetWypozyczenie = null;
        Uzytkownik? uzytkownikWypozyczenie = null;
        foreach (var sprzet in Sprzety)
        {
            if (idSprzet == sprzet.Id)
            {
                sprzetWypozyczenie = sprzet;
                if (!sprzet.CzyDostepny)
                {
                    Console.WriteLine("Sprzęt " + sprzet + " nie jest teraz dostępny do wypożyczenia. Wypożyczenie anulowane.");
                    return;
                }
                sprzet.CzyDostepny = false;
                break;
            }
        }

        foreach (var uzytkownik in Uzytkownicy)
        {
            if (idUzytkownika == uzytkownik.Identyfikator)
            {
                uzytkownikWypozyczenie = uzytkownik;
                int count = 0;
                int max = uzytkownik.LimitWypozyczen();
                foreach (var wypozyczenie in Wypozyczenia)
                {
                    if (wypozyczenie.Uzytkownik == uzytkownik && wypozyczenie.CzyZwrotTerminowy == null)
                    {
                        count++;
                    }
                }

                if (count >= max)
                {
                    Console.WriteLine(uzytkownik + " osiągnął już limit wypożyczeń. Wypożyczenie anulowane.");
                    return;
                }
            }
        }

        if (sprzetWypozyczenie == null || uzytkownikWypozyczenie == null)
        {
            Console.WriteLine("Sprzęt lub użytkownik nie znaleziony. Wypożyczenie anulowane.");
        }
        Wypozyczenia.Add(new Wypozyczenie(uzytkownikWypozyczenie, sprzetWypozyczenie, DateTime.Now));
    }
    
}