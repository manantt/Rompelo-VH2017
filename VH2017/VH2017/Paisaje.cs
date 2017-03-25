using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VH2017
{
    public class Paisaje
    {
        const int NUM_FONDOS = 4;

        const int NUBES = 0;
        const int MONTANAS = 1;
        const int ARBOLES = 2;
        const int SUELO = 3;

        public Vector2[] posiciones;
        Texture2D[] imagenes;
        float[] distancias;
        Vector2 tam;
        float velocidadNubes;

        public Paisaje(Texture2D imgNubes, Texture2D imgMontanas, Texture2D imgArboles, Texture2D imgSuelo, GraphicsDeviceManager graphics)
        {

            posiciones = new Vector2[NUM_FONDOS];
            imagenes = new Texture2D[NUM_FONDOS];
            distancias = new float[NUM_FONDOS];

            posiciones[NUBES] = posiciones[MONTANAS] = posiciones[ARBOLES] = posiciones[SUELO] = new Vector2(0, 0);
            tam = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            imagenes[NUBES] = imgNubes;
            imagenes[MONTANAS] = imgMontanas;
            imagenes[ARBOLES] = imgArboles;
            imagenes[SUELO] = imgSuelo;

            velocidadNubes = 0.5f;

            distancias[NUBES] = 0.5f;
            distancias[MONTANAS] = 0.25f;
            distancias[ARBOLES] = 0.5f;
            distancias[SUELO] = Personaje.velocidad;
        }
        public void mover(int posPersonaje)
        {
            //movimiento personaje
            KeyboardState kb = Keyboard.GetState();
            if (UI.iniciado)
            {
                for (int i = 0; i < posiciones.Length; i++)
                {
                    posiciones[i].X -= distancias[i];
                }
            }

            //movimiento nubes
            posiciones[NUBES].X -= velocidadNubes;
            //codigo sagrado
            for (int i = 0; i < posiciones.Length; i++)
            {
                if (posiciones[i].X > 0)
                    posiciones[i].X -= tam.X;
                if (posiciones[i].X < -tam.X)
                    posiciones[i].X += tam.X;
            }
        }
        public void pintar(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < posiciones.Length; i++)
            {
                spriteBatch.Draw(imagenes[i], new Rectangle((int)posiciones[i].X, 0, (int)tam.X, (int)tam.Y), Color.White);
                spriteBatch.Draw(imagenes[i], new Rectangle((int)(posiciones[i].X + tam.X), 0, (int)tam.X, (int)tam.Y), Color.White);
            }
        }
    }
}
