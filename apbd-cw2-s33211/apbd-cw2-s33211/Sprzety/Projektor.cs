namespace apbd_cw2_s33211.Sprzety;

public class Projektor : Sprzet
{
    private int JasnoscLumeny { get;set; }
    private string Rozdzielczosc { get;set; }

    public Projektor(string nazwa, int jasnosc, string rozdzielczosc) : base(nazwa)
    {
        this.JasnoscLumeny = jasnosc;
        this.Rozdzielczosc = rozdzielczosc;
    }

    public override string ToString()
    {
        return "Sprzęt ID: " + Id + ", Projektor: " + Nazwa + ", Jasność: " + JasnoscLumeny + " lumenów, Rozdzielczość: " + Rozdzielczosc;
    }
}