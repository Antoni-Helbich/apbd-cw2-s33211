namespace apbd_cw2_s33211.Sprzety;

public class Kamera : Sprzet
{
    public string Rozdzielczosc {get; set;}
    public double Ogniskowa { get; set; }
    
    public Kamera(string nazwa, string rozdzielczosc, double ogniskowa) : base(nazwa)
    {
        this.Rozdzielczosc = rozdzielczosc;
        this.Ogniskowa = ogniskowa;
        this.KaraZaDzien = 45;
    }

    public override string ToString()
    {
        return "Sprzęt ID: " + Id + ", Kamera: " + Nazwa + ", Rozdzielczość: " + Rozdzielczosc + ", Ogniskowa: " + Ogniskowa;
    }
}