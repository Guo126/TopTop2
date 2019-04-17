public class Item
{

    //public GameObject gameObject;
    public int number;
    public string name;
    public string explain;
    public string type;
    public string realName;
    public int job;
    public int worth;
    public int addBlood;
    public int addBloodMax;
    public int addMagicNum;
    public int addMagicMax;
    public int addAtk;
    public int addDef;
    public int addEvd;
    public int addCrt;

    public Item(string na, int n, string ex, string ty, string rn, int j, int w,int a_b, int a_bm, int a_mn, int a_mm, int a_atk, int a_def, int a_evd, int a_crt)
    {
        explain = ex;
        number = n;
        name = na;
        type = ty;
        realName = rn;
        job = j;
        worth = w;
        addBlood = a_b;
        addBloodMax = a_bm;
        addMagicNum = a_mn;
        addMagicMax = a_mm;
        addAtk = a_atk;
        addDef = a_def;
        addEvd = a_evd;
        addCrt = a_crt;
    }
}