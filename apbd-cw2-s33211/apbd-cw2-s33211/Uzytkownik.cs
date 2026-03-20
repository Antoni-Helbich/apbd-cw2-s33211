namespace apbd_cw2_s33211;
public class Uzytkownik
{
    public UzytkownikType UzytkownikType { get; set; }
    private static int _idNumber = 1;
    public string Identyfikator { get; set; }
    public string Imie { get; set; }
    public string Nazwisko { get; set; }

    public Uzytkownik(UzytkownikType typ, string imie, string nazwisko)
    {
        UzytkownikType = typ;
        Identyfikator = "U" + _idNumber++;
        Imie = imie;
        Nazwisko = nazwisko;
    }

    public int LimitWypozyczen()
    {
        switch (UzytkownikType)
        {
            case UzytkownikType.Student: return 2;
            case UzytkownikType.Pracownik: return 5;
        }

        return 0;
    }

    public override string ToString()
    {
        return "Użytkownik id: " +  Identyfikator + ", Imie i nazwisko: " + Imie + " " + Nazwisko;
    }

}

public enum UzytkownikType
{
    Student,
    Pracownik
}