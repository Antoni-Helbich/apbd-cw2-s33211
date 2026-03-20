namespace apbd_cw2_s33211.Sprzety;

public class Laptop : Sprzet
{
    public int Ram { get; set; }
    public int PojemnoscDyskuSSD { get; set; }

    public Laptop(string nazwa, int ram, int dysk) : base(nazwa)
    {
        this.Ram = ram;
        this.PojemnoscDyskuSSD = dysk;
        this.KaraZaDzien = 100;
    }

    public override string ToString()
    {
        return "Sprzęt ID: " + Id + ", Laptop: " + Nazwa + ", RAM: " + Ram + " GB, DyskSSD: " +  PojemnoscDyskuSSD + " GB";
    }
}