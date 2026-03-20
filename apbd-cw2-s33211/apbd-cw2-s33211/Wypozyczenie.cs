using apbd_cw2_s33211.Sprzety;

namespace apbd_cw2_s33211;

public class Wypozyczenie
{
    public Uzytkownik Uzytkownik { get; set; }
    public Sprzet Sprzet { get; set; }
    public DateTime DataWypozyczenia { get; set; }
    public DateTime DataZwrotu { get; set; }
    public bool? CzyZwrotTerminowy { get; set; }

    public Wypozyczenie(Uzytkownik uzytkownik, Sprzet sprzet, DateTime dataWypozyczenia, int dni)
    {
        this.Uzytkownik = uzytkownik;
        this.Sprzet = sprzet;
        this.DataWypozyczenia = dataWypozyczenia;
        this.DataZwrotu = dataWypozyczenia.AddDays(dni);
        CzyZwrotTerminowy = null;
    }

    public override string ToString()
    {

        if (CzyZwrotTerminowy == null)
        {
            return "Wypożyczenie użytkownika: " +  Uzytkownik.Identyfikator + ", Sprzętu: " + Sprzet.Id + ", do: " + DataZwrotu;
        }
        else if (CzyZwrotTerminowy == true)
        {
            return "Wypożyczenie użytkownika: " +  Uzytkownik.Identyfikator + ", Sprzętu: " + Sprzet.Id + ", zwrócone terminowo.";
        }
        else
        {
            return "Wypożyczenie użytkownika: " +  Uzytkownik.Identyfikator + ", Sprzętu: " + Sprzet.Id + ", zwrócone nieterminowo.";
        }
        
        
    }
}