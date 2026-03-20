namespace apbd_cw2_s33211;
public class Uzytkownik
{
    public UzytkownikType UzytkownikType { get; set; }
    private static int _idNumber = 1;
    public string Identyfikator { get; set; }
    public string Imie { get; set; }
    public string Nazwisko { get; set; }

    public Uzytkownik(string typ, string imie, string nazwisko)
    {
        if(typ == "Student") UzytkownikType = UzytkownikType.Student;
        else if (typ == "Pracownik") UzytkownikType = UzytkownikType.Pracownik;
        Identyfikator = "U" + _idNumber++;
        Imie = imie;
        Nazwisko = nazwisko;
    }

}

public enum UzytkownikType
{
    Student,
    Pracownik
}