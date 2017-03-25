using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VH2017
{
    public class Pajaro
    {
        public Texture2D imagen;
        public Song sonido;
        public int spriteActual,contador;
        public float velocidad = 2;
        public Vector2 posicion,tam=new Vector2(163,173);

        public void mover()
        {
            //posicion.X -= velocidad;
            contador++;
            if (contador > 5)
            {
                spriteActual++;
                contador = 0;
            }

            if (spriteActual == 9)
                spriteActual = 0;
        }
        public void pintar(SpriteBatch sb)
        {
            //sb.Draw(imagen, new Rectangle((int)posicion.X-posPersonaje, (int)posicion.Y, (int)tam.X, (int)tam.Y), Color.White);
            sb.Draw(imagen, new Rectangle((int)posicion.X-(int)Personaje.posicion.X, (int)posicion.Y,(int)tam.X, (int)tam.Y),
                new Rectangle(0 + spriteActual * imagen.Width / 9, 0, imagen.Width / 9, imagen.Height), Color.White);
        }
    }
}
