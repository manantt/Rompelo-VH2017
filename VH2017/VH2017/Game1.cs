using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace VH2017
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D imgNubes, imgMontanas, imgArboles, imgSuelo;
        Song song_fondo;
        SoundEffect
           efecto_madera,
           efecto_aguila,
           efecto_andar,
           efecto_salto,
           efecto_muerte,
           efecto_deslizar,
           efecto_cristal;
        SoundEffectInstance
            movimientoSonido,
            saltoSonido,
            deslizamientoSonido,
            aguilaSonido,
            maderaSonido,
            muerteSonido,
            cristalSonido;

        Paisaje paisaje;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

        }

        protected override void Initialize()
        {
            base.Initialize();

        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Personaje.imagen = Content.Load<Texture2D>("personaje");
            Aparicion.img_pinchos = Content.Load<Texture2D>("pinchos");
            UI.imagenRotura = Content.Load<Texture2D>("textura");
            UI.sf_record = Content.Load<SpriteFont>("record");
            UI.sf_titulo = Content.Load<SpriteFont>("titulo");
            UI.sf_romper = Content.Load<SpriteFont>("texto_inicio_romper");
            UI.imagenInicio = Content.Load<Texture2D>("GUI_Inicio");
            UI.imagenProgress = Content.Load<Texture2D>("GUI_Progreso");
            UI.imagenBarraProgreso = Content.Load<Texture2D>("barra");

            UI.imagenRotura2 = Content.Load<Texture2D>("cristal"); 
            UI.imgRecordPost = Content.Load<Texture2D>("segnal");

            imgNubes = Content.Load<Texture2D>("nubes");
            imgMontanas = Content.Load<Texture2D>("montana");
            imgArboles = Content.Load<Texture2D>("arboles");
            imgSuelo = Content.Load<Texture2D>("suelo");
            paisaje = new Paisaje(imgNubes, imgMontanas, imgArboles, imgSuelo, graphics);

            Aparicion.img_pajaro = Content.Load<Texture2D>("pajaro");

            //SFX

            //musica fondo
            song_fondo = Content.Load<Song>("fondoVikingo1");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.6f;

            //sonidos
            efecto_muerte = Content.Load<SoundEffect>("sonidoMuerte");
            efecto_andar = Content.Load<SoundEffect>("caminarCambiado");
            efecto_salto = Content.Load<SoundEffect>("salto2");
            efecto_deslizar = Content.Load<SoundEffect>("deslizar");
            efecto_aguila = Content.Load<SoundEffect>("aguila");
            efecto_madera = Content.Load<SoundEffect>("madera");
            efecto_cristal = Content.Load<SoundEffect>("cristal1");



            //efectos
            movimientoSonido = efecto_andar.CreateInstance();
            saltoSonido = efecto_salto.CreateInstance();
            deslizamientoSonido = efecto_deslizar.CreateInstance();
            aguilaSonido = efecto_aguila.CreateInstance();
            maderaSonido = efecto_madera.CreateInstance();
            muerteSonido = efecto_muerte.CreateInstance();
            cristalSonido = efecto_cristal.CreateInstance();



            inicializar();
        }


        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            paisaje.mover((int)Personaje.posicion.X);
            foreach (Pajaro p in Aparicion.pajaros)
                p.mover();
            UI.mover();
            if (UI.iniciado)
            {
                Personaje.mover();
                comprobarColisiones();
            }

            KeyboardState ks = Keyboard.GetState();
            if ((ks.IsKeyDown(Keys.Space)))
            {
                inicializar();
                UI.iniciado = true;
            }
            if (Personaje.muerto)
                UI.iniciado = false;
            Aparicion.generar();

            base.Update(gameTime);
        }
        void comprobarColisiones()
        {
            foreach (Pinchos p in Aparicion.pinchos)
                if (new Rectangle((int)p.posicion.X, (int)p.posicion.Y, (int)p.tam.X, (int)p.tam.Y)
                    .Intersects(new Rectangle((int)Personaje.posicion.X + 50, (int)Personaje.posicion.Y, (int)Personaje.tam.X - 90, (int)Personaje.tam.Y)))
                {
                    if (p.rompible && Personaje.deslizando)
                    {
                        p.romper();
                        Sonidos.romperMadera();
                    }

                    else
                    {
                        if (!p.roto)
                        {
                            Personaje.muerto = true;
                            Personaje.morir();
                           
                            
                        }
                    }
                }
            foreach(Pajaro p in Aparicion.pajaros)
            {
                if (!Personaje.deslizando)
                {
                    if (new Rectangle((int)p.posicion.X, (int)p.posicion.Y, (int)p.tam.X, (int)p.tam.Y)
                    .Intersects(new Rectangle((int)Personaje.posicion.X + 50, (int)Personaje.posicion.Y, (int)Personaje.tam.X - 90, (int)Personaje.tam.Y)))
                    {
                        Personaje.muerto = true;
                        Personaje.morir();
                       
                    }
                }
            }

        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            paisaje.pintar(spriteBatch);
            spriteBatch.Draw(UI.imgRecordPost, new Rectangle(UI.record - (int)Personaje.posicion.X, 265, 100, 140), Color.White);

            if (UI.iniciado)
            {
                Personaje.pintar(spriteBatch);
                foreach (Pinchos p in Aparicion.pinchos)
                    p.pintar(spriteBatch);
                foreach (Pajaro p in Aparicion.pajaros)
                    p.pintar(spriteBatch);

            }
            UI.pintar(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void inicializar()
        {
            MediaPlayer.Play(song_fondo);
            Aparicion.pinchos = new List<Pinchos>();
            Aparicion.pajaros = new List<Pajaro>();
            Personaje.YInicial = (int)(graphics.PreferredBackBufferHeight - Personaje.tam.Y - 65);
            Personaje.posicion = new Vector2(0, Personaje.YInicial);

            Personaje.deslizando = false;
            Personaje.saltando = false;
            Personaje.muerto = false;
            Personaje.spriteActual = 0;
            Personaje.contadorSalto = 0;
            Sonidos.sonidoAguila = aguilaSonido;
            Sonidos.sonidoAndar = movimientoSonido;    
            Sonidos.sonidoDeslizar = deslizamientoSonido;
            Sonidos.sonidoMadera = maderaSonido;
            Sonidos.sonidoMuerte = muerteSonido;
            Sonidos.sonidoSalto = saltoSonido;
            Sonidos.sonidoCristales = cristalSonido;
            
            for(int i =0; i< paisaje.posiciones.Length;i++)
            {
                paisaje.posiciones[i].X = 0;
            }
            UI.iniciado = false;
            UI.record = int.Parse(System.IO.File.ReadAllText(@"./../../../../../VH2017/VH2017Content/maxPuntuacion.txt"));
            UI.rotura = 0f;
            Aparicion.contador = 0;
            //Aparicion.pinchos.Add(new Pinchos(img_pinchos1, new Vector2(200, Personaje.YInicial - 50 + Personaje.tam.Y), true));
            //Aparicion.pinchos.Add(new Pinchos(img_pinchos1, new Vector2(500, Personaje.YInicial - 50 + Personaje.tam.Y), false));
            //Aparicion.pajaros.Add(new Pajaro { imagen = img_pajaro, posicion = new Vector2(700, Personaje.YInicial - 260 + Personaje.tam.Y) });
        }



    }
}
