using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VH2017
{
    public static class Aparicion
    {
        public static int minimo = 450, contador = 0,randomAnterior=100;
        public static List<Pinchos> pinchos;
        public static List<Pajaro> pajaros;
        public static Texture2D img_pinchos, img_pajaro;

        //probabilidad
        public static void generar() {

            contador += (int)Personaje.velocidad;
            if (contador >= minimo)
            {
                contador = 0;
                Random rnd = new Random();
                int random = rnd.Next(0, 11);
                while (random == randomAnterior)
                    random = rnd.Next(0, 11);
                randomAnterior = random;

                    if (random <= 7)
                    {
                        if (random < 2)
                            pinchos.Add(new Pinchos(img_pinchos, new Vector2(Personaje.posicion.X + 800 - (new Random().Next(25, 50)), Personaje.YInicial - 50 + Personaje.tam.Y), true));
                        else
                        {
                            pinchos.Add(new Pinchos(img_pinchos, new Vector2(Personaje.posicion.X + 800 - (new Random().Next(25, 50)), Personaje.YInicial - 50 + Personaje.tam.Y), false));
                        }
                    }
                    else
                    {
                        Aparicion.pajaros.Add(new Pajaro { imagen = img_pajaro, posicion = new Vector2(Personaje.posicion.X + 800 - (new Random().Next(25, 50)), Personaje.YInicial - 260 + Personaje.tam.Y) });
                    Sonidos.pajaros();
                    }
 
             }
          
            /*contador += (int)Personaje.velocidad;
              if (contador >= minimo)
            {
                contador = 0;
                pinchos.Add(new Pinchos(img_pinchos, new Vector2(Personaje.posicion.X+800, Personaje.YInicial - 50 + Personaje.tam.Y), true));
            }*/
        }//A-B-A-B-A-B-A-B
    }
}
