using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VH2017
{
    public static class Sonidos
    {

        public static bool sonidoFin = false;
        public static int contSonido1, contSonido2, tiempoPasos = 25;
        public static SoundEffectInstance
            sonidoMadera,
            sonidoAguila,
            sonidoAndar,
            sonidoSalto,
            sonidoMuerte,
            sonidoDeslizar,
            sonidoCristales;



        public static void romperMadera()
        {
            sonidoMadera.Play();
            sonidoMadera.Volume = 0.3f;
            if (sonidoMadera.State == SoundState.Playing)
            {
                sonidoMadera.Play();
            }
            else
            {
                sonidoMadera.Stop();
            }
        }

        public static void pajaros()
        {
            sonidoAguila.Play();
            if (sonidoAguila.State == SoundState.Playing)
            {
                sonidoAguila.Play();
            }
            else
            {
                sonidoAguila.Stop();
            }
        }

        public static void andar()
        {
            if (!Personaje.muerto)
            {
                contSonido1++;
                if (!sonidoFin && !Personaje.saltando && !Personaje.deslizando)
                {
                    sonidoAndar.Play();
                    sonidoFin = true;
                }
                if (contSonido1 == tiempoPasos)
                {
                    sonidoAndar.Stop();
                    contSonido1 = 0;
                    sonidoFin = false;
                }
                if (sonidoAndar.State == SoundState.Playing)
                {
                    sonidoAndar.Play();
                }
                else
                {
                    sonidoAndar.Stop();
                }



            }
        }

        public static void saltar()
        {
            
            sonidoSalto.Play();
            
            //if (sonidoSalto.State == SoundState.Playing)
            //{
            //    sonidoSalto.Play();
            //}
            //else
            //{
            //    sonidoSalto.Stop();
            //}

        }

        public static void muerte()
        {
            sonidoMuerte.Play();
            if (sonidoMuerte.State == SoundState.Playing)
            {
                sonidoMuerte.Play();
            }
            else
            {
                sonidoMuerte.Stop();
            }
        }

        public static void deslizar()
        {
            sonidoDeslizar.Play();
            if (sonidoDeslizar.State == SoundState.Playing)
            {
                sonidoDeslizar.Play();
            }
            else
            {
                sonidoDeslizar.Stop();
            }
        }
        public static void romperCristales()
        {
            sonidoCristales.Play();

            if (sonidoCristales.State == SoundState.Playing)
            {
                sonidoCristales.Play();
            }
            else
            {
                sonidoCristales.Stop();
            }

        }

    }
}
