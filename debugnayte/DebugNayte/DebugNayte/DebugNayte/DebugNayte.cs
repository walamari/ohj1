using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DebugAliohjelmat;

namespace DebugNayte
{
    /// <summary>
    /// Tehtävä debuggausnäytettä varten.
    /// 
    /// Debuggausnäyte sisältää kaksi osaa. Ensimmäisessä osassa on tarkoitus näyttää,
    /// että muuttujien arvot osataan katsoa ja tarvittaessa muuttaa.
    /// 
    /// Toisessa osassa katsotaan, että osataan käyttää ehdollisia keskeytyskohtia
    /// (eng. conditional breakpoint).
    /// 
    /// Tehtävä 1:
    /// 
    ///   1. Aja ohjelma.
    ///   2. Ohjelma tulostaa
    ///       23
    ///       Johan oli null taulukossa, mutta missä kohti???
    ///   3. Tehtäväsi on saada debuggerilla ajamalla ja sen avulla muuttujia
    ///      muuttamalla ohjelma tulostamaan
    ///       100
    ///       Johan oli null taulukossa, mutta missä kohti???
    ///      Itse ohjelmakoodia ei saa muuttaa lainkaan, kaikki muutokset
    ///      on tehtävä debuggauksen aikana näkyviin muuttujiin.	
    /// 
    ///  Tehtävä 2:
    ///   1. Pyydä ohjaajaa antamaan sinulle luku, ja vaihda se luvun
    ///      100 tilalle rivillä: int koko = 100;
    ///   2. Aloita ohjelman debuggaus. Nyt olisi tarkoituksena ohjelmaa MUUTTAMATTA
    ///      saada selville, mikä on i:n arvo silloin, kun taulukossa oleva
    ///      null-viite tulee vastaan.
    /// 
    /// @author vesal
    /// @version 24.10.2010
    ///          12.10.2011 - C#-versio ~sailniir&viospelh
    ///          18.10.2013 - mikkalle (LuoSanat erilliseen kirjastoon)
    /// </summary>
    public class DebugNayte
    {


        /// <summary>
        /// Laskee merkkijonotaulukon alkioiden yhteispituuden
        /// </summary>
        /// <param name="sanat">taulukko</param>
        /// <returns>yhteispituus</returns>
        public static int SummaaPituudet(String[] sanat)
        {
            int summa = 0;
            for (int i = 0; i < sanat.Length; i++)
            {
                int pituus = sanat[i].Length;
                summa += pituus;
            }
            return summa;
        }


        /// <summary>
        /// Tulostaa taulukon alkioiden summan
        /// </summary>
        /// <param name="taulukko">Taulukko</param>
        public static void TulostaSumma(int[] taulukko)
        {
            int summa = 0;

            for (int i = 0; i < taulukko.Length; i++)
            {
                summa += taulukko[i];
            }

            Console.WriteLine(summa);
        }


        /// <summary>
        /// Luodaan kokonaislukutaulukko ja taulukollinen sanoja
        /// </summary>
        /// <param name="args">ei käytössä</param>
        public static void Main(String[] args)
        {
            int[] luvut = { 3, 7, 8, 5 };
            TulostaSumma(luvut);

            int koko = 250;
            String[] sanat;
            sanat = Sananluoja.LuoSanat(koko);

            try
            {
                int yhteispituus = SummaaPituudet(sanat);
                Console.WriteLine(yhteispituus);
            }
            catch (Exception)
            {
                Console.WriteLine("Johan oli null taulukossa, mutta missä kohti???");
            }
        }
    }
}
