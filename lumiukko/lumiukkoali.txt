using Jypeli;

/// @author  Vesa Lappalainen
/// @version 09.10.2011
///
/// <summary>
/// Kaksi lumiukkoa aliohjelman avulla
/// </summary>
public class LumiukkoAli : PhysicsGame
{
    /// <summary>
    /// Pääohjelmassa laitetaan "peli" käyntiin Jypeli-kirjastolle tyypilliseen tapaan
    /// </summary>
    /// <param name="args">Ei käytössä</param>
    public static void Main(string[] args)
    {
        using (LumiukkoAli peli = new LumiukkoAli())
        {
            peli.Run();
        }
    }


    /// <summary>
    /// Aliohjelmassa piirretään ja zoomataan kamera siten,
    /// että kenttä näkyy hyvin ruudulla.
    /// </summary>
    public override void Begin()
    {
        // Camera.ZoomToLevel();
        Level.BackgroundColor = Color.Black;

        PiirraLumiukko(this, 0, Level.Bottom + 200.0);
        PiirraLumiukko(this, 200.0, Level.Bottom + 300.0);
        Camera.ZoomToAllObjects(100);
        //        Level.CreateBorders();
        BoundingRectangle rect = Level.FindObjectLimits();
        Camera.ZoomTo(rect);
        Level.Width = rect.Width;
        Level.Height = rect.Height;
        Level.CreateBorders();
    }


    /// <summary>
    /// Aliohjelma piirtää lumiukon
    /// annettuun paikkaan.
    /// </summary>
    /// <param name="peli">Peli, johon lumiukko tehdään.</param>
    /// <param name="x">Lumiukon alimman pallon x-koordinaatti.</param>
    /// <param name="y">Lumiukon alimman pallon y-koordinaatti.</param>
    public static void PiirraLumiukko(PhysicsGame peli, double x, double y)
    {
        PhysicsObject p1, p2, p3;
        double r1 = 100;
        double r2 = 50;
        double r3 = 30;

        p1 = new PhysicsObject(2 * r1, 2 * r1, Shape.Circle);
        p1.X = x;
        p1.Y = y;
        peli.Add(p1);

        p2 = new PhysicsObject(2 * r2, 2 * r2, Shape.Circle);
        p2.X = x;
        p2.Y = p1.Y + r1 + r2;
        peli.Add(p2);

        p3 = new PhysicsObject(2 * r3, 2 * r3, Shape.Circle);
        p3.X = x;
        p3.Y = p2.Y + r2 + r3;
        peli.Add(p3);
    }
}