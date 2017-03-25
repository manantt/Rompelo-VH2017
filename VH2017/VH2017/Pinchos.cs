using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VH2017
{
    public class Pinchos
    {
        Texture2D imagen;
        public Vector2 posicion, tam;
        public Boolean rompible, roto;

        public Pinchos(Texture2D img, Vector2 pos, Boolean rompi)
        {
            this.imagen = img;
            this.posicion = pos;
            this.rompible = rompi;
            tam = new Vector2(150, 50);
            this.roto = false;
        }
        public void romper()
        {
            roto = true;
        }
        public void pintar(SpriteBatch sb)
        {

            sb.Draw(imagen, new Rectangle((int)posicion.X-(int)Personaje.posicion.X, (int)posicion.Y, (int)tam.X, (int)tam.Y),new Rectangle (0+imagen.Width*(roto?1:0)/2,0,imagen.Width/2,imagen.Height), (rompible?Color.White:new Color(90,90,90)));
        }
    }
}
