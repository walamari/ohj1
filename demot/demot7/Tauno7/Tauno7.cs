using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

/// @author vesal
/// @version 20.10.2013
/// <summary>
/// Lasketaan rajan ylittävät luvut
/// </summary>
public class Tauno7
{
    /// <summary>
    /// Lasketyaan moniko luku ylittää rajan
    /// </summary>
    public static void Main()
    {
        int[] taulukko = { 72, 22, 73, 2, 16, 62 };
        int maara = MuutaYli(taulukko, 20, 0);
        Console.WriteLine("{0} {1}", maara, String.Join(", ", taulukko));
    }


    /// <summary>
    /// Lasketaan moniko luku on suurempi kuin annettu raja.  Jokainen rajan ylittävä
    /// taulukon alkio muutetaan miksi-arvoksi
    /// </summary>
    /// <param name="t">taukukko jota tutkitaan</param>
    /// <param name="raja">raja jota suuremmat lasketaan ja muutetaan</param>
    /// <param name="miksi">miksi luvuksi muutetaan</param>
    /// <returns>moniko arvo muutettiin</returns>
    /// <example>
    /// <pre name="test">
    ///     int[] t = { 72, 22, 73, 2, 16, 62 };
    ///     MuutaYli(t,20,0) === 4;
    ///     String.Join(", ", t) === "0, 0, 0, 2, 16, 0";
    /// </pre>
    /// </example>
    public static int MuutaYli(int[] t, int raja, int miksi)
    {
        /// TODO: 1. Tee Taunoon apumuuttuja raja ja sen arvoksi 20, tee apumuuttuja miksi ja sen arvoksi 0
        /// TODO: 2. Tee tarvittava apumuuttuja lkm ja indeksi i
        /// TODO: 3. Askella taulukko läpi ja laske moniko alkio on suurempi kuin tämä raja ja samalla muuta nämä isot miksi arvoon
        /// TODO: 4. kopioi näiden rivien tilalle tuo koodi (pyyhi raja ja miksi-muuttujien luonti pois)
        /// TODO: 5. Laita lkm kasvattaneet ja taulukon arvoa muuttaneet lauseet sopivan if-aluseen sisään
        /// TODO: 6: Lisää vastaavat rivit kaikkien muiden rivien kohdalle jotta koodissa on samanlaisia "lohkoja"
        /// TODO: 7: Testaa ja jos toimii, kommentoi koko homma
        /// TODO: 8: Muuta silmukaksi
        return 0;
    }

}
