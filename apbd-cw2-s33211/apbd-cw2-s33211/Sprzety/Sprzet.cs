namespace apbd_cw2_s33211.Sprzety;

public abstract class Sprzet
{
    private static int _idNumber = 1;
    public string Id { get; set; }
    public string Nazwa { get; set; }
    public bool CzyDostepny { get; set; }

    public Sprzet(string nazwa)
    {
        Id = "S" + _idNumber++;
        this.Nazwa = nazwa;
        CzyDostepny = true;
    }
    
    

}