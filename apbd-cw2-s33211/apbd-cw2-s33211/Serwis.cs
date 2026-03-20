using apbd_cw2_s33211.Sprzety;
namespace apbd_cw2_s33211;
public class Serwis
{
    public List<Uzytkownik> Uzytkownicy { get; }
    public List<Sprzet> Sprzety { get; }
    public List<Wypozyczenie> Wypozyczenia { get; }

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

    public List<Uzytkownik> PobierzUzytkownikow()
    {
        return Uzytkownicy;
    }

    public List<Sprzet> PobierzSprzet()
    {
        return Sprzety;
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
            throw new ArgumentException("Sprzęt lub użytkownik nie został znaleziony.");
        }
        
        if (!sprzetWypozyczenie.CzyDostepny)
        {
            throw new InvalidOperationException($"Sprzęt {sprzetWypozyczenie.Id} nie jest teraz dostępny do wypożyczenia.");
        }
        
        int count = PobierzAktywneWypozyczeniaUzytkownika(idUzytkownika).Count;
        int max = uzytkownikWypozyczenie.LimitWypozyczen();

        if (count >= max)
        {
            throw new InvalidOperationException($"Użytkownik {uzytkownikWypozyczenie.Identyfikator} osiągnął limit wypożyczeń.");
        }
        
        Wypozyczenia.Add(new Wypozyczenie(uzytkownikWypozyczenie, sprzetWypozyczenie, DateTime.Now, dni));
        sprzetWypozyczenie.CzyDostepny = false;
    }

    public int ZwrotSprzetu(string idSprzet, string idUzytkownika, DateTime dataZwrotu)
    {
        Sprzet? sprzetZwrot = ZnajdzSprzet(idSprzet);
        Uzytkownik? uzytkownikZwrot = ZnajdzUzytkownika(idUzytkownika);

        if (sprzetZwrot == null || uzytkownikZwrot == null)
        {
            throw new ArgumentException("Nie znaleziono użytkownika lub sprzętu.");
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
            throw new InvalidOperationException("Nie ma takiego aktywnego wypożyczenia do zwrotu.");
        }

        wypozyczenieZwrot.Sprzet.CzyDostepny = true;
        
        int dni = (dataZwrotu - wypozyczenieZwrot.DataZwrotu).Days;
        
        if (dni <= 0)
        {
            wypozyczenieZwrot.CzyZwrotTerminowy = true;
            return 0; 
        }
        else
        {
            wypozyczenieZwrot.CzyZwrotTerminowy = false;
            int kara = dni * sprzetZwrot.KaraZaDzien;
            wypozyczenieZwrot.Kara = kara;
            return kara; 
        }
    }
    
    public void SprzetNiedostepny(string idSprzet)
    {
        Sprzet? sprzetNiedostepny = ZnajdzSprzet(idSprzet);
        if (sprzetNiedostepny == null)
        {
            throw new ArgumentException("Nie znaleziono sprzętu.");
        }

        if (!sprzetNiedostepny.CzyDostepny)
        {
            throw new InvalidOperationException("Sprzęt jest już oznaczony jako niedostępny.");
        }
        
        sprzetNiedostepny.CzyDostepny = false;
    }

    public List<Wypozyczenie> PobierzAktywneWypozyczeniaUzytkownika(string idUzytkownika)
    {
        Uzytkownik? uzytkownik = ZnajdzUzytkownika(idUzytkownika);
        if (uzytkownik == null)
        {
            throw new ArgumentException("Nie znaleziono użytkownika.");
        }
        
        List<Wypozyczenie> aktywneWypozyczenia = new List<Wypozyczenie>();
        foreach (var wypozyczenie in Wypozyczenia)
        {
            if (wypozyczenie.Uzytkownik == uzytkownik && wypozyczenie.CzyZwrotTerminowy == null)
            {
                aktywneWypozyczenia.Add(wypozyczenie);
            }
        }

        return aktywneWypozyczenia;
    }

    public List<Wypozyczenie> PobierzPrzeterminowaneWypozyczenia()
    {
        List<Wypozyczenie> przeterminowaneWypozyczenia = new List<Wypozyczenie>();
        foreach (var wypozyczenie in Wypozyczenia)
        {
            int dni = (wypozyczenie.DataZwrotu - DateTime.Now).Days;
            if (dni < 0)
            {
                przeterminowaneWypozyczenia.Add(wypozyczenie);
            }
        }

        return przeterminowaneWypozyczenia;
    }

    public string WygenerujRaportWypozyczalni()
    {
        string raport = "Zapisanych użytkowników: " + Uzytkownicy.Count;
        raport += "\nZapisanych sprzętów: " + Sprzety.Count;
        int dostepne = 0;
        int niedostepne = 0;
        foreach (var sprzet in Sprzety)
        {
            if(sprzet.CzyDostepny) dostepne++;
            else niedostepne++;
        }
        raport += "\nDostępne sprzęty: " + dostepne;
        raport += "\nNiedostępne sprzęty: " + niedostepne;
        raport += "\nZapisane wypożyczenia: " + Wypozyczenia.Count;
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