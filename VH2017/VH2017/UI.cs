using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VH2017
{
    public static class UI
    {
        public static Boolean iniciado,sonado=false;
        public static int record;
        public static float rotura = 0f;
        public static SpriteFont sf_titulo, sf_record, sf_romper;
        public static String titulo = "TODO", pulseParaIniciar = "TODO";
        public static Texture2D imgRecordPost, imagenInicio, imagenRotura, imagenProgress, imagenRotura2, imagenBarraProgreso;
        static int contadorPantallaRota = 0, spriteActual = 0;
        static Vector2 posBarraProgreso = new Vector2(600, 10);

        static Vector2 pos_record = new Vector2(650, 15),
            pos_titulo = new Vector2(240, 65), pos_rompelo = new Vector2(180, 185);

        public static void mover()
        {
            contadorPantallaRota++;
            if (contadorPantallaRota > 5)
            {
                spriteActual++;
                contadorPantallaRota = 0;
            }
            if (spriteActual == 2)
            {
                spriteActual = 0;
            }

            else if (Personaje.posicion.X > record * 0.8 && Personaje.posicion.X < record)
            {
                rotura = 0.35f;
            }
            else if (Personaje.posicion.X > record)
            {
                rotura = 0.65f;
            }

        }

        public static void pintar(SpriteBatch sb)
        {
            if (iniciado)
            {

                sb.Draw(imagenRotura, new Rectangle((int)(800 * (1 - rotura)), 0, 800, 600),
                    new Rectangle(0 + spriteActual * 30, 0, imagenRotura.Width - 10, imagenRotura.Height),
                    Color.LightGray);
                if (Personaje.posicion.X > record)
                {
                    if (!sonado)
                    {
                        sonado = true;
                        Sonidos.romperCristales();
                    }             
                    sb.Draw(imagenRotura2, new Rectangle(300, 200, imagenRotura2.Width * 2, imagenRotura2.Height * 2), Color.White);
                    sb.Draw(imagenRotura2, new Rectangle(400, 0, imagenRotura2.Width * 2, imagenRotura2.Height * 2), Color.White);
                    sb.Draw(imagenRotura2, new Rectangle(150, 0, imagenRotura2.Width * 2, imagenRotura2.Height * 2), Color.White);
                    sb.Draw(imagenRotura2, new Rectangle(500, 100, imagenRotura2.Width * 2, imagenRotura2.Height * 2), Color.White);
                }

                sb.Draw(imagenProgress, posBarraProgreso, Color.White);
                sb.Draw(imagenBarraProgreso, new Rectangle((int)posBarraProgreso.X + 28, (int)posBarraProgreso.Y + 8, (Personaje.posicion.X < record ? (int)(imagenBarraProgreso.Width * Personaje.posicion.X / record) : imagenBarraProgreso.Width), imagenBarraProgreso.Height), Color.GreenYellow);
                sb.DrawString(sf_record, ((int)Personaje.posicion.X).ToString() + " / " + record, pos_record, Color.RosyBrown);

            }
            else
            {
                sb.Draw(imagenInicio, new Rectangle(0, 0, 800, 500), Color.White);
                sb.DrawString(sf_titulo, "Record: " + record, pos_titulo, Color.White);
                sb.DrawString(sf_romper, "ROMPELO!", pos_rompelo, Color.Brown);
                sb.DrawString(sf_titulo, "Pulse [ESPACIO].", new Vector2(pos_rompelo.X + 25, pos_rompelo.Y + 100), Color.Brown);
            }

        }
    }
}
