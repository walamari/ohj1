using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

/// @author vesal
/// @version 20.10.2013
/// <summary>
/// Kahden taulukon sisätulo
/// </summary>
public class Tauno72
{
    /// <summary>
    /// Lasketaan kahden taulukon sisätulo
    /// </summary>
    public static void Main()
    {
        int[,] maski = { 
            {1,1,1},
            {1,0,0},
            {0,1,1} 
        };
        int[,] luvut = { 
           {255,34,120,222},
           {35,50,60,70},
           {50,90,102,10},
           {20,34,44,55} 
        };
        int st = Sisatulo(luvut, maski, 2, 1);  // 273
        // eli 1*35 + 1*50  + 1*60 +
        //     1*50 + 0*90  + 0*102 +
        //     0*20 + 1*34  + 1*44           
        Console.WriteLine(st);
    }


    /// <summary>
    /// Lasketaan matriin osan ja maskin välinen sisätulo (eli kerrotaan vastinalkiot ja summataan).
    /// Aloituspisteen iy,ix tulee olla sisäpiste (se ei saa olla reunassa)
    /// Maskin tulee olla 3x3 kokoinen.
    /// </summary>
    /// <param name="luvut">matriisi, jonka osa otetaan</param>
    /// <param name="maski">maski, jonka kanssa sisätulo lasketaan</param>
    /// <param name="iy">miltä riviltä luvut-taulukkoa aloitetaan</param>
    /// <param name="ix">miltä sarakkeelta luvut-taulukkoa aloitetaan</param>
    /// <returns>sisätulo</returns>
    /// <example>
    /// <pre name="test">
    ///    int[,] maski = { {1,1,1},{1,0,0},{0,1,1} };
    ///    int[,] luvut = { {255,34,120,222},{35,50,60,70},{50,90,102,10},{20,34,44,55} };
    ///    Sisatulo(luvut,maski,2,1) === 273;
    ///    Sisatulo(luvut,maski,1,1) === 636;
    ///    Sisatulo(luvut,maski,1,2) === 538;
    ///    Sisatulo(luvut,maski,2,2) === 369;
    /// </pre>
    /// </example>
    /// <example>
    /// <pre name="test">
    ///  int[,] naapurit = { {1,1,1},{1,0,1},{1,1,1} };
    ///  int[,] alkuSukupolvi = {
    ///    { 1,0,1,1 },
    ///    { 0,1,1,0 },
    ///    { 1,0,0,0 },
    ///    { 1,0,0,1 }
    ///  };
    ///  Sisatulo(alkuSukupolvi,naapurit,2,1) === 4;
    ///  Sisatulo(alkuSukupolvi,naapurit,1,1) === 4;
    ///  Sisatulo(alkuSukupolvi,naapurit,1,2) === 3;
    ///  Sisatulo(alkuSukupolvi,naapurit,2,2) === 3;
    /// </pre>
    /// </example>
    public static int Sisatulo(int[,] luvut, int[,] maski, int iy, int ix)
    {
        int summa = 0;
        summa += luvut[1, 0] * maski[0, 0];
        summa += luvut[1, 1] * maski[0, 1];
        // TODO täydennä tähän puuttuvat rivit 
        summa += luvut[2, 1] * maski[1, 1];
        // TODO: jatka tähän laskut loppuun pisteen 2,1 sisätulon laskemiseksi
        // TODO: eli "laita tuo 3x3 maski pisteen 2,1 kohdalle" ja kerro kaikki kohdakkain olevat alkiot keskenään
        // TODO: ja lisää summaan
        return summa;
    }


}
