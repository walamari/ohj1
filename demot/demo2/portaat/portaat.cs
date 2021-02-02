using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Widgets;
/// <summary>
/// Piirretään portaat
/// </summary>
public class portaat : PhysicsGame
{
    public override void Begin()
    {

        Camera.ZoomToAllObjects(50);
        PiirraNelio(this, 0, 0);
        PiirraNelio(this, 80, 80);
        PiirraNelio(this, 160, 160);
        PiirraNelio(this, 240, 240);
        PiirraNelio(this, 320, 320);

    }


    public static PhysicsObject PiirraNelio(PhysicsGame peli, double x, double y)
    {
        PhysicsObject PiirraNelio  = new PhysicsObject(80 , 80, Shape.Rectangle);
        PiirraNelio.X = x;
        PiirraNelio.Y = y;
        peli.Add(PiirraNelio);
        return PiirraNelio;
    }
}