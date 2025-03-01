﻿// Otetaan käyttöön Jyväskylän yliopiston Jypeli-kirjasto
using Jypeli;

/// @author  Antti-Jussi Lakanen, Vesa Lappalainen
/// @version 12.1.2012
///
/// <summary>
/// Luokka, jossa harjoitellaan piirtämistä lisäämällä ympyröitä ruudulle
/// </summary>
public class Lumiukko : PhysicsGame
{
    /// <summary>
    /// Pääohjelmassa laitetaan "peli" käyntiin Jypeli-kirjastolle 
    /// tyypilliseen tapaan
    /// </summary>
    public static void Main()
    {
        using (Lumiukko peli = new Lumiukko())
        {
            peli.Run();
        }
    }

    /// <summary>
    /// Aliohjelmassa piirretään ja zoomataan kamera siten että 
    /// kenttä näkyy hyvin ruudulla.
    /// </summary>
    public override void Begin()
    {
                Camera.ZoomToLevel(); // tai Camera.ZoomToAllObjects();
        Level.Background.Color = Color.Black;

        GameObject p1 = new GameObject( 2*100, 2*100, Shape.Circle);
        p1.X = 0; p1.Y = Level.Bottom + 200;
        Add(p1);

        GameObject p2 = new GameObject( 2*50, 2*50, Shape.Circle );
        p2.X = 0; p2.Y = p1.Y + 100 + 50;
        Add(p2);

        GameObject p3 = new GameObject( 2*30, 2*30, Shape.Circle );
        p3.X = 0; p3.Y = p2.Y + 50 + 30;
        Add(p3);
       
        GameObject p4 = new GameObject( 2*3, 2*5, Shape.Circle );
        p4.X = -7; p4.Y = p3.Y + 7 + 0;
        p4.Color = new Color(255, 200, 100, 255);
        Add(p4);
        
        GameObject p5 = new GameObject( 2*3, 2*5, Shape.Circle );
        p5.X = 7; p5.Y = p4.Y + 7 + 0;
        p5.Color = new Color(255, 200, 100, 255);
        Add(p4);
    }
}