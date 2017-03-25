using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VH2017
{
    public static class Personaje
    {
        public static Vector2 posicion, tam = new Vector2(2048 / 16, 1075 / 6);
        public static int YInicial;
        public static float velocidad = 3f, subidaPorFrame = 2f, dist = 165, contadorSalto = 0, contadorDeslizar = 0, contadorCorriendo=0;
        public static Boolean saltando, deslizando, muerto;
        public static Texture2D imagen;
        public static int spriteActual;


        public static void mover()
        {
            if (!muerto)
            {
                Personaje.posicion.X += Personaje.velocidad;
                KeyboardState ks = Keyboard.GetState();
                if ((ks.IsKeyDown(Keys.Up) || ks.IsKeyDown(Keys.W)) && !deslizando && !saltando)
                {
                    saltando = true;
                    Sonidos.saltar();
                }
                else if ((ks.IsKeyDown(Keys.Down) || ks.IsKeyDown(Keys.S)) && !saltando && !deslizando)
                {
                    deslizando = true;
                    Sonidos.deslizar();
                }else if(!saltando && !deslizando)
                {
                    Sonidos.andar();
                }
                if (saltando)
                {
                    saltar();
                }
                if (deslizando)
                {
                    deslizar();
                }
            }
        }

        private static void saltar()
        {
            
            contadorSalto++;
            if(contadorSalto >=dist)
            {
                saltando = false;
                contadorSalto = 0;
                spriteActual = 0;
                posicion.Y = YInicial;
            }
            if (contadorSalto < dist / 3)
            { 
                spriteActual = 1;
                Personaje.posicion.Y -= subidaPorFrame;
            }

            else if (contadorSalto < dist * 2 / 3)
                spriteActual = 2;
            else
            { 
                spriteActual = 3;
                Personaje.posicion.Y += subidaPorFrame;
            }


        }
        private static void deslizar()
        {
            
           
            contadorDeslizar++;
            if (contadorDeslizar >= dist*0.75)
            {
                deslizando = false;
                contadorDeslizar = 0;
                spriteActual =0;
            }
           


        }

        public static void morir()
        {
            Sonidos.sonidoMuerte.Play();
            String puntuacion = System.IO.File.ReadAllText(@"./../../../../../VH2017/VH2017Content/maxPuntuacion.txt");
            
            if (int.Parse(puntuacion) < (int)posicion.X)
            {
                using (System.IO.StreamWriter fileWrite = new System.IO.StreamWriter(@"./../../../../../VH2017/VH2017Content/temp.txt"))
                {
                    fileWrite.WriteLine(((int)posicion.X).ToString());
                }
                UI.record = (int)posicion.X;
                //aqui se renombrea el archivo temporal 
                System.IO.File.Delete(@"./../../../../../VH2017/VH2017Content/maxPuntuacion.txt");
                System.IO.File.Move("./../../../../../VH2017/VH2017Content/temp.txt", "./../../../../../VH2017/VH2017Content/maxPuntuacion.txt");
            }

        }

        public static void pintar(SpriteBatch sb)
        {
            if (!muerto)
            {

                if (deslizando)
                {
                    sb.Draw(Personaje.imagen, new Rectangle(0, (int)Personaje.posicion.Y, (int)Personaje.tam.X, (int)Personaje.tam.Y),
                        new Rectangle(0 ,imagen.Height*2/3, imagen.Width / 8, imagen.Height / 3), Color.White);
                }
                else if (saltando)
                {
                    sb.Draw(Personaje.imagen, new Rectangle(0, (int)Personaje.posicion.Y, (int)Personaje.tam.X, (int)Personaje.tam.Y),
                        new Rectangle(0 + (imagen.Width / 8) * spriteActual, imagen.Height / 3, imagen.Width / 8, imagen.Height / 3), Color.White);
                }
                else //de normal
                {
                    contadorCorriendo++;
                    if (contadorCorriendo > 5)
                    {
                        spriteActual++;
                        contadorCorriendo = 0;
                    } 
                    spriteActual=(spriteActual==8)?0:spriteActual;
                    sb.Draw(Personaje.imagen, new Rectangle(0, (int)Personaje.posicion.Y, (int)Personaje.tam.X, (int)Personaje.tam.Y),
                        new Rectangle(0+(imagen.Width/8)*spriteActual, 0, imagen.Width / 8, imagen.Height / 3), Color.White);
                }
            }
        }
    }
}
