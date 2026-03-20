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
            serwis.DodajSprzet(sprzet); //Dodanie trzech różnego rodzaju sprzętów
        }

        List<Uzytkownik> uzytkownicyDoDodania = new List<Uzytkownik>();
        uzytkownicyDoDodania.Add(new Uzytkownik("Student", "Antoni", "Helbich"));
        uzytkownicyDoDodania.Add(new Uzytkownik("Student", "Kuba", "Jastrzębski"));
        uzytkownicyDoDodania.Add(new Uzytkownik("Pracownik", "John", "Smith"));
        foreach (var uzytkownik in uzytkownicyDoDodania)
        { 
            serwis.DodajUzytkownika(uzytkownik); //Dodanie kilku użytkowników
        }
        
        string idSprzet1 = "S1";
        string idSprzet2 = "S2";
        string idUser1 = "U1";
        string idUser2 = "U2";

        //Poprawne wypożyczenie sprzętu.
        serwis.WypozyczSprzet(idSprzet1, idUser1, 7);
        serwis.WypozyczSprzet(idSprzet2, idUser2, 3);

        //Próba wykonania niepoprawnej operacji (wypożyczenie sprzętu już wypożyczonego).
        try
        {
            serwis.WypozyczSprzet(idSprzet1, idUser2, 2);
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine($"Oczekiwany błąd: {e.Message}");
        }

        //Zwrot sprzętu w terminie (przed upływem 7 dni).
        serwis.ZwrotSprzetu(idSprzet1, idUser1, DateTime.Now.AddDays(5));

        //Zwrot opóźniony skutkujący naliczeniem kary (zwrot po 5 dniach, wypożyczono na 3).
        //Kara o wartości 200 zł - 2 dni ponad termin z laptopem (100 zł za dzień)
        int kara = serwis.ZwrotSprzetu(idSprzet2, idUser2, DateTime.Now.AddDays(5));
        Console.WriteLine($"Naliczone kary: {kara} zł.");

        //Wyświetlenie raportu końcowego o stanie systemu.
        Console.WriteLine("Raport końcowy:");
        Console.WriteLine(serwis.WygenerujRaportWypozyczalni());

    }
}