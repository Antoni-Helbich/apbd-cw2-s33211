using apbd_cw2_s33211.Sprzety;
namespace apbd_cw2_s33211;
class Program
{
    public static void Main()
    {
        Serwis serwis = new Serwis();
        
        List<Sprzet> sprzetyDoDodania = new List<Sprzet>();
        sprzetyDoDodania.Add(new Kamera("Sony", "FullHD", 2.8));
        sprzetyDoDodania.Add(new Laptop("Legion", 16, 512));
        sprzetyDoDodania.Add(new Projektor("BenQ", 2500, "HD"));
        foreach (Sprzet sprzet in sprzetyDoDodania)
        {
            serwis.DodajSprzet(sprzet); //dodanie trzech różnego rodzaju sprzętów
        }

        List<Uzytkownik> uzytkownicyDoDodania = new List<Uzytkownik>();
        uzytkownicyDoDodania.Add(new Uzytkownik("Student", "Antoni", "Helbich"));
        uzytkownicyDoDodania.Add(new Uzytkownik("Student", "Kuba", "Jastrzębski"));
        uzytkownicyDoDodania.Add(new Uzytkownik("Pracownik", "John", "Smith"));
        foreach (var uzytkownik in uzytkownicyDoDodania)
        { 
            serwis.DodajUzytkownika(uzytkownik); //dodanie kilku użytkowników
        }
        
        //dalsze testowanie do dopisania
        
    }
}