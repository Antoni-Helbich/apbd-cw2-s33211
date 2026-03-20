using apbd_cw2_s33211.Sprzety;
namespace apbd_cw2_s33211;

public class Serwis
{
    List<Uzytkownik> Uzytkownicy {get; set;}
    List<Sprzet> Sprzety {get; set;}

    public Serwis()
    {
        Uzytkownicy = new List<Uzytkownik>();
        Sprzety = new List<Sprzet>();
    }

    public void DodajUzytkownika(Uzytkownik uzytkownik)
    {
        Uzytkownicy.Add(uzytkownik);
    }

    public void DodajSprzet(Sprzet sprzet)
    {
        Sprzety.Add(sprzet);
    }
    
    
}