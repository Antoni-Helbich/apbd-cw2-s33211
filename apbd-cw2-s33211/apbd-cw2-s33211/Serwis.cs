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

    public void WypozyczSprzet(string idSprzet, string idUzytkownika, int dni)
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
        
        
        Wypozyczenia.Add(new Wypozyczenie(uzytkownikWypozyczenie, sprzetWypozyczenie, DateTime.Now, dni));
        sprzetWypozyczenie.CzyDostepny = false;
    }

    public void ZwrotSprzetu(string idSprzet, string idUzytkownika, DateTime dataZwrotu)
    {
        Sprzet sprzetZwrot =  ZnajdzSprzet(idSprzet);
        Uzytkownik uzytkownikZwrot = ZnajdzUzytkownika(idUzytkownika);
        if (sprzetZwrot == null || uzytkownikZwrot == null)
        {
            Console.WriteLine("Nie znaleziono użytkownika lub sprzętu. Zwrot anulowany.");
            return;
        }

        Wypozyczenie? wypozyczenieZwrot = null;
        foreach (var wypozyczenie in Wypozyczenia)
        {
            if (wypozyczenie.Uzytkownik == uzytkownikZwrot && wypozyczenie.Sprzet == sprzetZwrot &&
                wypozyczenie.CzyZwrotTerminowy == null)
            {
                wypozyczenieZwrot = wypozyczenie;
            }
        }

        if (wypozyczenieZwrot == null)
        {
            Console.WriteLine("Nie ma takiego wypożyczenia do zwrotu. Zwrot anulowany.");
            return;
        }
        
        int dni = (dataZwrotu - wypozyczenieZwrot.DataZwrotu).Days;
        if (dni <= 0)
        {
            Console.WriteLine("Zwrot przebiegł pomyślnie.");
            wypozyczenieZwrot.CzyZwrotTerminowy = true;
        }
        else
        {
            Console.WriteLine("Zwrot nieterminowy, dodatkowa opłata o wysokości: " + dni*sprzetZwrot.KaraZaDzien + " zł.");
            wypozyczenieZwrot.CzyZwrotTerminowy = false;
        }
    }
    
    public void SprzetNiedostepny(string idSprzet)
    {
        Sprzet? sprzetNiedostepny = ZnajdzSprzet(idSprzet);
        if (sprzetNiedostepny == null)
        {
            Console.WriteLine("Nie znaleziono sprzętu.");
            return;
        }

        if (!sprzetNiedostepny.CzyDostepny)
        {
            Console.WriteLine("Sprzęt jest już oznaczony jako niedostępny.");
        }
        else
        {
            sprzetNiedostepny.CzyDostepny = false;
        }
    }

    public void WyswietlAktywneWypozyczeniaUzytkownika(string idUzytkownika)
    {
        Uzytkownik? uzytkownik = ZnajdzUzytkownika(idUzytkownika);
        if (uzytkownik == null)
        {
            Console.WriteLine("Nie znaleziono użytkownika.");
            return;
        }
        
        foreach (var wypozyczenie in Wypozyczenia)
        {
            if (wypozyczenie.Uzytkownik == uzytkownik && wypozyczenie.CzyZwrotTerminowy == null)
            {
                Console.WriteLine(wypozyczenie);
            }
        }
    }

    public void WyswietlPrzeterminowaneWypozyczenia()
    {
        foreach (var wypozyczenie in Wypozyczenia)
        {
            int dni = (wypozyczenie.DataZwrotu - DateTime.Now).Days;
            if (dni < 0)
            {
                Console.WriteLine(wypozyczenie);
            }
        }
    }

    public string WygenerujRaportWypożyczalni()
    {
        string raport = "Zapisanych użytkowników: " + Uzytkownicy.Count();
        raport += "\nZapisanych sprzętów: " + Sprzety.Count();
        int dostepne = 0;
        int niedostepne = 0;
        foreach (var sprzet in Sprzety)
        {
            if(sprzet.CzyDostepny) dostepne++;
            else  niedostepne++;
        }
        raport += "\nDostępne sprzęty: " + dostepne;
        raport += "\nNiedostępne sprzęty: " +  niedostepne;
        raport += "\nZapisane wypożyczenia: " + Wypozyczenia.Count();
        int zrealizowane = 0;
        int wToku = 0;
        foreach (var wypozyczenie in Wypozyczenia)
        {
            if(wypozyczenie.CzyZwrotTerminowy == null) wToku++;
            else zrealizowane++;
        }
        raport += "\nWypożyczenia zrealizowane: " + zrealizowane + ", W toku: " + wToku;
        return raport;
    }
    
}