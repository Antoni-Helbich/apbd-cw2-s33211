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

    private Sprzet? ZnajdzSprzet(string idSrzet)
    {
        foreach (var sprzet in Sprzety)
        {
            if (idSrzet == sprzet.Id) return sprzet;
        }

        return null;
    }
    private Uzytkownik? ZnajdzUzytkownika(string idUzytkownika)
    {
        foreach (var uzytkownik in Uzytkownicy)
        {
            if (idUzytkownika == uzytkownik.Identyfikator) return uzytkownik;
        }

        return null;
    }

    public void WypozyczSprzet(string idSprzet, string idUzytkownika)
    {
        Sprzet? sprzetWypozyczenie = ZnajdzSprzet(idSprzet);
        Uzytkownik? uzytkownikWypozyczenie = ZnajdzUzytkownika(idUzytkownika);
        if (sprzetWypozyczenie == null || uzytkownikWypozyczenie == null)
        {
            Console.WriteLine("Sprzęt lub użytkownik nie znaleziony. Wypożyczenie anulowane.");
            return;
        }
        
        if (!sprzetWypozyczenie.CzyDostepny)
        {
            Console.WriteLine("Sprzęt " + sprzetWypozyczenie + " nie jest teraz dostępny do wypożyczenia. Wypożyczenie anulowane.");
            return;
        }
        
        int count = 0;
        int max = uzytkownikWypozyczenie.LimitWypozyczen();
        foreach (var wypozyczenie in Wypozyczenia)
        {
            if (wypozyczenie.Uzytkownik == uzytkownikWypozyczenie && wypozyczenie.CzyZwrotTerminowy == null)
            {
                count++;
            }
        }

        if (count >= max)
        {
            Console.WriteLine(uzytkownikWypozyczenie + " osiągnął już limit wypożyczeń. Wypożyczenie anulowane.");
            return;
        }
        
        
        Wypozyczenia.Add(new Wypozyczenie(uzytkownikWypozyczenie, sprzetWypozyczenie, DateTime.Now));
        sprzetWypozyczenie.CzyDostepny = false;
    }
    
    
}