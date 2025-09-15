public static class MoneyToString
{
    public static string Get(int dinero)
    {
        string strDinero = dinero.ToString();
        string res = "";
		
        if(dinero < 1)//sin ditero
            res = "";
        else if(strDinero.Length == 6)//cientos de miles
        {
            for(int i = 0; i < strDinero.Length; i++)
            {
                res += strDinero[i];
				
                if(i == 2)
                    res += ".";
            }
        }else if(strDinero.Length == 7)//millones
        {
            for(int i = 0; i < strDinero.Length; i++)
            {
                res += strDinero[i];
				
                if(i == 0 || i == 3)
                    res += ".";
            }
        }
		
        return res;
    }
}