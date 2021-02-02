using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
// using Jypeli.Effects;
using Jypeli.Widgets;

namespace Demo7
{

    /// @author  Vesa Lappalainen
    /// @version 16.10.2011
    /// <summary>
    /// Peli, jossa vihaisia Legoja tiputellaan toisten päälle
    /// </summary>
    public class AngryLego : PhysicsGame
    {
        private static String[] lines = {
                  "                        ",
                  "                        ",
                  "                        ",
                  "                        ",
                  "/                       ",
                  "                        ",
                  "                        ",
                  "                        ",
                  "                        ",
                  "                        ",
                  "                        ",
                  "                        ",
                  "                        ",
                  "                        ",
                  "                        ",
                  "                        ",
                  "                        ",
                  "        =======         ",
                  "        X* X  X         ",
                  "        X  X  X         ",
                  "   *    X  X  X     *   ",
                  "        X  X  X         ",
                  "        X *X *X         ",
                  };

        private static int tileWidth = 800 / lines[0].Length;
        private static int tileHeight = 480 / lines.Length;

        private PhysicsObject maila;
        private TileMap tiles = TileMap.FromStringArray(lines);


        /// <summary>
        /// Aloitetaan peli. Aluksi siivotaan kaikki, jotta voidaan aloittaa uusi peli
        /// </summary>
        public override void Begin()
        {
            ClearGameObjects();
            ClearControls();
            Gravity = new Vector(0, -500);
            // IsFullScreen = true;

            Level.Background.CreateGradient(Color.Blue, Color.White);

            tiles['X'] = LuoSeina;
            tiles['='] = LuoKatto;
            tiles['/'] = LuoMaila;
            tiles['*'] = LuoVihollinen;
            // tiles.SetTileMethod('X', LuoSeina);
            // tiles.SetTileMethod('=', LuoKatto);
            // tiles.SetTileMethod('*', LuoVihollinen);

            tiles.Insert(tileWidth, tileHeight);
            //            tiles.Execute(tileWidth, tileHeight);

            Level.CreateBorders();
            Camera.ZoomToLevel();

            Keyboard.Listen(Key.F1, ButtonState.Pressed, ShowControlHelp, "Show help");
            Keyboard.Listen(Key.F5, ButtonState.Pressed, Begin, "New game");
            Keyboard.Listen(Key.Escape, ButtonState.Pressed, Exit, "Exit game");
            Keyboard.Listen(Key.Up, ButtonState.Pressed, KaannaMailaa, "Move up", 5.0);
            Keyboard.Listen(Key.Down, ButtonState.Pressed, KaannaMailaa, "Move down", -5.0);
            Keyboard.Listen(Key.Space, ButtonState.Pressed, PudotaPallo, "Drop ball");
        }


        /// <summary>
        /// Luodaan seinäelementti
        /// </summary>
        /// <returns>luotu elementti</returns>
        private PhysicsObject LuoSeina()
        {
            PhysicsObject seina = new PhysicsObject(tileWidth, tileHeight);
            seina.Color = Color.Wheat;
            seina.Tag = "rakenne";
            seina.Image = LoadImage("tiili");
            return seina;
        }


        /// <summary>
        /// Luodaan kattoelementti.  Luodaan hieman ylisuureksi, jolloin liimautuu
        /// naapuriin kiinni.
        /// </summary>
        /// <returns>luotu elementti</returns>
        private PhysicsObject LuoKatto()
        {
            PhysicsObject katto = new PhysicsObject(tileWidth * 1.4, tileHeight);
            katto.Color = Color.Red;
            katto.Tag = "rakenne";
            return katto;
        }


        /// <summary>
        /// Luodaan maila, jolle palloja pudotellaan
        /// </summary>
        /// <returns>luotu taso</returns>
        private PhysicsObject LuoMaila()
        {
            maila = PhysicsObject.CreateStaticObject(tileWidth * 6, tileHeight);
            maila.Color = Color.Black;
            return maila;
        }


        /// <summary>
        /// Luodaan vihollinen, joka hajoaa osuessaan rekenteeseen
        /// </summary>
        /// <returns>luotu vihollinen</returns>
        private PhysicsObject LuoVihollinen()
        {
            PhysicsObject vihu = new PhysicsObject(tileWidth / 2, tileWidth / 2, Shape.Circle);
            vihu.Color = Color.Pink;
            AddCollisionHandler(vihu, "rakenne", VihuunOsui);
            vihu.Tag = "vihu";
            vihu.Image = LoadImage("Baby");
            return vihu;
        }


        /// <summary>
        /// Apualiohjelma vihollisen räjäyttämiseksi ja poistamiseksi
        /// </summary>
        /// <param name="vihu"></param>
        private void PossautaVihu(IPhysicsObject vihu)
        {
            Explosion rajahdys = new Explosion(vihu.Width * 10);
            rajahdys.Position = vihu.Position;
            rajahdys.UseShockWave = false;
            Add(rajahdys);
            Remove(vihu);
        }


        /// <summary>
        /// Kun vihollinen osuu rakenteeseen
        /// </summary>
        /// <param name="vihu">vihollinen joka törmäsi</param>
        /// <param name="rakenne">rakenne johon osui</param>
        private void VihuunOsui(PhysicsObject vihu, PhysicsObject rakenne)
        {
            PossautaVihu(vihu);
        }


        /// <summary>
        /// Tapahtuma kun pallo osuu viholliseen
        /// </summary>
        /// <param name="pallo">pallo joka osui</param>
        /// <param name="vihu">vihollinen johon osuttiin</param>
        private void PalloOsui(PhysicsObject pallo, PhysicsObject vihu)
        {
            PossautaVihu(vihu);
        }


        /// <summary>
        /// Käännetään pudotustasoa
        /// </summary>
        /// <param name="kulma">millä kulmalla käännetään</param>
        private void KaannaMailaa(double kulma)
        {
            maila.Angle += Angle.FromDegrees(kulma);
        }


        /// <summary>
        /// Pudotetaan uusi pallo, joka voi rikkoa vihollisen
        /// </summary>
        private void PudotaPallo()
        {
            PhysicsObject pallo = new PhysicsObject(tileWidth, tileWidth, Shape.Circle);
            pallo.Color = Color.Yellow;
            pallo.Position = maila.Position + new Vector(0, maila.Height + tileWidth);
            pallo.Image = LoadImage("Igor");
            Add(pallo);
            AddCollisionHandler(pallo, "vihu", PalloOsui);
        }
    }
}