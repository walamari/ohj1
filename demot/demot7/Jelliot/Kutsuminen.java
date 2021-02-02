import Prog1Tools.IOTools;
import java.util.Arrays;

public class Kutsuminen {
    public static void main() {
        int a = 4, b = 5;
        int iso;
        iso = Suurempi(a, b);
        System.out.println(iso);
        iso = Suurempi(iso+1, 7 / 3 + 5 / 2);
        System.out.println(iso);
        int[] luvut = new int[4];
        int[] m = luvut;
        luvut[2] = 4;
        luvut[1] = 6;
        m[3] = iso+3;
        VaihdaSuurin(luvut, 5);
        System.out.println(Arrays.toString(luvut));
    }

    public static int Suurempi(int luku1, int luku2)
    {
        if (luku1 >= luku2) return luku1;
        return luku2;
    }

    public static void VaihdaSuurin(int[] t, int korvaavaArvo)
    {
        int paikka = -1;
        int suurin = Integer.MIN_VALUE;
        for (int i = 0; i < t.length; i++)
        {
            int luku = t[i];
            if (luku > suurin)
            {
                suurin = luku;
                paikka = i;
            }
        }
        if (paikka < 0) return;
        t[paikka] = korvaavaArvo;
    }
}
