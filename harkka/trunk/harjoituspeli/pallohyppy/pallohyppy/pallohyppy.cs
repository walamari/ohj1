using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Widgets;

/// @author  Konsta ja Valtteri
/// @version 29.11.2018
///
/// <summary>
/// PalloPeli missä tarkoituksena hypppiä pallolla nii korkealle kuin mahdollista ja kerätä tähtiä
/// </summary>

public class pallohyppy : PhysicsGame
{
    const double nopeus = 250;
    const double hyppyNopeus = 1400;
    const int RUUDUN_KOKO = 60;
    PlatformCharacter pallo;
    EasyHighScore topLista = new EasyHighScore();
    private Image tahtiKuva = LoadImage("tahti");
    private Image tasokuva = LoadImage("seinama");

    private int kenttaLaskuri = 1;
    private IntMeter pisteLaskuri;


    public override void Begin()
    {
        AloitaPeli();

    }


    /// <summary>
    /// Aloitapeli aliohjelma sisältää pelin aloitukseen tarvittat aliohjelmat.
    /// </summary>
    public void AloitaPeli()
    {
        ClearAll();
        LuoKentta();
        LisaaNappaimet();
        LuoPistelaskuri();
        LisaaTuhoaja();
        Gravity = new Vector(0, -1000);
        Level.Background.CreateGradient(Color.White, Color.Brown);

        Camera.Velocity = new Vector(0, 70);
        Camera.FollowX(pallo);
        Camera.X = pallo.X;
        Camera.Y = pallo.Y;

        Timer ajastin = new Timer();
        ajastin.Interval = 15;
        ajastin.Timeout += LuoJatkoKentta;
        ajastin.Start();
    }


    /// <summary>
    /// Luo kentän
    /// </summary>
    public void LuoKentta()
    {
       
        TileMap kentta = TileMap.FromLevelAsset("kentta4");
        kentta.SetTileMethod('#', LisaaTaso);
        kentta.SetTileMethod('p', LisaaPelaaja);
        kentta.SetTileMethod('T', LisaaTahti);
        kentta.Execute(RUUDUN_KOKO, RUUDUN_KOKO);
    }


    /// <summary>
    /// Lisää erilaisia valmiiksi tehtyjä kenttiä aina edellisen kentän jälkeen
    /// </summary>
    public void LuoJatkoKentta()
    {
        TileMap JatkoKentta = TileMap.FromLevelAsset(RandomGen.SelectOne("kentta5","kentta3", "kentta2", "kentta1"));
        JatkoKentta.SetTileMethod('#', LisaaTaso2);
        JatkoKentta.SetTileMethod('T', LisaaTahti2);
        JatkoKentta.Execute(RUUDUN_KOKO, RUUDUN_KOKO);
        kenttaLaskuri++;
    }


    /// <summary>
    /// Lisää ensimmäiseen kenttään
    /// </summary>
    /// <param name="paikka">Paikka johon este lisätään</param>
    /// <param name="leveys">Esteen leveys</param>
    /// <param name="korkeus">Esteen korkeus</param>
    public void LisaaTaso(Vector paikka, double leveys, double korkeus)
    {
        PhysicsObject taso = PhysicsObject.CreateStaticObject(leveys, korkeus);
        taso.Position = paikka;      
        taso.Image = tasokuva;
        taso.Tag = "taso";
        Add(taso);
    }


    /// <summary>
    /// Lisää muihin kenttiin esteen paikan
    /// </summary>
    /// <param name="p">Paikka, johono este lisätään</param>
    /// <param name="leveys">Esteen leveys</param>
    /// <param name="korkeus">Esteen korkeus</param>
    public void LisaaTaso2(Vector p, double leveys, double korkeus)
    {
        PhysicsObject taso = PhysicsObject.CreateStaticObject(leveys, korkeus);
        taso.Y = p.Y + Level.Height * kenttaLaskuri;
        taso.X = p.X;
        taso.Image = tasokuva;
        Add(taso);
    }

    
    /// <summary>
    /// Lisää pelaajan eli tässä tapauksessa pallon
    /// </summary>
    /// <param name="paikka">Paikka johon pallo pelin alussa lisätään</param>
    /// <param name="leveys">pallon leveys</param>
    /// <param name="korkeus">pallon korkeus</param>
    public void LisaaPelaaja(Vector paikka, double leveys, double korkeus)
    {
        pallo = new PlatformCharacter(40, 40);
        pallo.Shape = Shape.Circle;
        pallo.IgnoresGravity = true;
        pallo.Color = Color.Red;
        pallo.Mass = 4.0;
        pallo.Position = paikka;
        AddCollisionHandler(pallo, "tahti", KeraaTahti);
        pallo.Tag = "pallo";
        Add(pallo);
    }


    /// <summary>
    /// Lisää näppäimet, jolla pelaajaa voi ohjailla.
    /// </summary>
    public void LisaaNappaimet()
    {
        Keyboard.Listen(Key.F1, ButtonState.Pressed, ShowControlHelp, "Näytä ohjeet");
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");
        Keyboard.Listen(Key.Left, ButtonState.Down, Liikuta, "Liikkuu vasemmalle", pallo, -nopeus);
        Keyboard.Listen(Key.Right, ButtonState.Down, Liikuta, "Liikkuu vasemmalle", pallo, nopeus);
        Keyboard.Listen(Key.Space, ButtonState.Pressed, Hyppaa, "Pelaaja hyppää", pallo, hyppyNopeus);
    }


    /// <summary>
    /// Antaa pallolle nopeuden, jolla se voi liikkua sivuttain.
    /// </summary>
    /// <param name="hahmo"> Hahmo, jota liikutetaan</param>
    /// <param name="nopeus">Nopeus, jolla hahmo voi liikkua</param>
    public void Liikuta(PlatformCharacter hahmo, double nopeus)
    {
        pallo.Walk(nopeus);
    }


    /// <summary>
    /// Antaa pallolle nopeuden, millä se voi hypätä.
    /// </summary>
    /// <param name="hahmo">Hahmo, joka hyppää</param>
    /// <param name="nopeus">Arvo, jolla pallo voi hypätä ylöspäin</param>
    public void Hyppaa(PlatformCharacter hahmo, double nopeus)
    {
        pallo.Jump(nopeus);
    }


    /// <summary>
    /// Aliohjelma, jolla tähteen osuessa se katoaa, pistelaskuriin saa tietyn määrän pisteitä ja kertoo viestin,kun tähteen on osuttu.
    /// </summary>
    /// <param name="hahmo">Hahmo, joka kerää tähtiä</param>
    /// <param name="tahti">Tahti objecti, jota pelissä kerätään</param>
    
    public void KeraaTahti(PhysicsObject hahmo, PhysicsObject tahti)
    {
        MessageDisplay.Add("Keräsit tähden!");
        tahti.Destroy();
        pisteLaskuri.AddValue(5);
    }


    /// <summary>
    /// Luo tähdet ensimmäiseen kenttään.
    /// </summary>
    /// <param name="paikka">Paikka johon tähti tulee</param>
    /// <param name="leveys">tähden leveys</param>
    /// <param name="korkeus">tähden korkeus</param>
    public void LisaaTahti(Vector paikka, double leveys, double korkeus)
    {
        PhysicsObject tahti = PhysicsObject.CreateStaticObject(leveys, korkeus);
        tahti.IgnoresCollisionResponse = true;
        tahti.Position = paikka;
        tahti.Image = tahtiKuva;
        tahti.Tag = "tahti";
        Add(tahti);
    }


    /// <summary>
    /// Tekee tähdet ensimmäisen kentän jälkeisiin kenttiin
    /// </summary>
    /// <param name="paikka">tähden piakka kentällä</param>
    /// <param name="leveys">tähden leveys</param>
    /// <param name="korkeus">tähden korkeus</param>
    public void LisaaTahti2(Vector paikka, double leveys, double korkeus)
    {
        PhysicsObject tahti = PhysicsObject.CreateStaticObject(leveys, korkeus);
        tahti.IgnoresCollisionResponse = true;
        tahti.Y = paikka.Y + Level.Height * kenttaLaskuri;
        tahti.X = paikka.X;

        tahti.Image = tahtiKuva;
        tahti.Tag = "tahti";
        Add(tahti);
    }
            

    /// <summary>
    /// Aliohjelma, joka tekee pistelaskuri joka laskee pisteet
    /// </summary>
    public void LuoPistelaskuri()
    {
        pisteLaskuri = new IntMeter(0);
        Label pisteNaytto = new Label();
        pisteNaytto.X = Screen.Left + 100;
        pisteNaytto.Y = Screen.Top - 100;
        pisteNaytto.TextColor = Color.Black;
        pisteNaytto.Color = Color.LightBlue;
        pisteNaytto.Title = "Pisteet";
        pisteNaytto.BindTo(pisteLaskuri);
        pisteLaskuri.AddOverTime(10000000, 10000000);
        Add(pisteNaytto);
    }


    /// <summary>
    /// tuhoaa pallon osuessaan siihen 
    /// </summary>
    /// <param name="tappaja">tuhoaja</param>
    /// <param name="pallo">tuhottava</param>
    public void PalloKuolee(PhysicsObject tappaja, PhysicsObject pallo)
    {
        pallo.Destroy();
        topLista.EnterAndShow(pisteLaskuri.Value);
        topLista.HighScoreWindow.Closed += delegate { AloitaPeli(); };
       
    }


    /// <summary>
    /// tuhoaa tasot osuessaan niihin
    /// </summary>
    /// <param name="tappaja">tuhoaja</param>
    /// <param name="taso">tuhottava</param>
    public void TasoTuhoutuu(PhysicsObject tappaja, PhysicsObject taso)
    {
        taso.Destroy();
    }


    /// <summary>
    /// lisää viivan joka tuhoaa kaikki siihen osuvat
    /// </summary>
    public void LisaaTuhoaja()
    {
        PhysicsObject tuhoaja = new PhysicsObject(Level.Width, 10);
        tuhoaja.Position = new Vector(0, pallo.Y - 500);
        tuhoaja.Shape = Shape.Rectangle;
        tuhoaja.Velocity = Camera.Velocity = new Vector(0, 70);
        AddCollisionHandler(tuhoaja, "pallo", PalloKuolee);
        AddCollisionHandler(tuhoaja, "taso", TasoTuhoutuu);
        tuhoaja.IgnoresGravity = true;
        tuhoaja.IgnoresPhysicsLogics = true;
        tuhoaja.IgnoresCollisionResponse = true;
        Add(tuhoaja);

    }
   
}