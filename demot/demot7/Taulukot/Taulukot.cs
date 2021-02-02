using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Demo7
{
    /// @author  Vesa Lappalainen
    /// @version 16.10.2011
    /// <summary>
    /// Aliohjelmia taulukoille 
    /// </summary>
    public class Taulukot
    {
        /// <summary>
        /// Testataan taulukkoaliohjelmia
        /// </summary>
        public static void Main()
        {
            double d = ErotaDouble("2k3", 1);
            Console.WriteLine(d); /// tulostaa 2
        }


        /// <summary>
        /// Otetaan jonossa oleva reaaliluku.  Jos luku ei ole
        /// mielekäs luku, niin palautetaan oletus. Desimaalina on aina .
        /// </summary>
        /// <param name="jono">jono josta reaaliluku otetaan</param>
        /// <param name="oletus">mikä arvo annetaan jos jonosta ei saada lukua</param>
        /// <returns>otettu reaaliluku</returns>
        /// <example>
        /// <pre name="test">
        ///   ErotaDouble("") ~~~ 0.0;
        ///   ErotaDouble("",2) ~~~ 2.0;
        ///   ErotaDouble(" 2.3 ") ~~~ 2.3;
        ///   ErotaDouble("5 3") ~~~ 5;
        ///   ErotaDouble("5k3") ~~~ 5;
        ///   ErotaDouble("5e3") ~~~ 5000;
        ///   ErotaDouble("5E-3") ~~~ 0.005;
        ///   ErotaDouble("k") ~~~ 0.0;
        ///   ErotaDouble("k",1.0) ~~~ 1.0;
        ///   ErotaDouble("2..3") ~~~ 0.0;
        /// </pre>
        /// </example>
        public static double ErotaDouble(string jono, double oletus = 0.0)
        {
            string reg = @"^([-0-9\.eE]+)(.*)$";
            Match m = Regex.Match(jono.Trim(), reg);
            string tjono = m.Groups[1].Value;
            double tulos = oletus;
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";
            if (double.TryParse(tjono, NumberStyles.Any, nfi, out tulos)) return tulos;
            return oletus;
        }

        /// <summary>
        /// Otetaan jonossa oleva reaaliluku.  Jos luku ei ole
        /// mielekäs luku, niin palautetaan oletus.
        /// </summary>
        /// <param name="jono">jono josta reaaliluku otetaan</param>
        /// <param name="oletus">mikä arvo annetaan jos jonosta ei saada lukua</param>
        /// <returns>otettu reaaliluku</returns>
        /// <example>
        /// <pre name="test">
        ///   Erota("",0.0) ~~~ 0.0;
        ///   Erota(" 2.3 ",0.0) ~~~ 2.3;
        ///   Erota("5 3",0.0) ~~~ 5;
        ///   Erota("k",0.0) ~~~ 0.0;
        ///   Erota("k",1.0) ~~~ 1.0;
        ///   Erota("2..3",0.0) ~~~ 0.0;
        /// </pre>
        /// </example>
        public static double Erota(string jono, double oletus)
        {
            return ErotaDouble(jono, oletus);
        }

    }
}
