namespace apbd_cw2_s33211.Sprzety;

public abstract class Sprzet
{
    private static int idNumber = 1;
    public int Id { get; set; }
    public string Nazwa { get; set; }
    public bool CzyDostepny { get; set; }

    public Sprzet(string nazwa)
    {
        Id = idNumber++;
        this.Nazwa = nazwa;
        CzyDostepny = true;
    }
    
    

}