/*
 * Autor: Julian Troyano Dominguez  
 * Programa:
 * Este programa muestra una frase rebotando en los laterales de la pantalla con la flecha hacia arriba se aumenta la velocidad y la flecha haci abajo se disminuye
 * Escape para salir
 * Version = 3.0
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace FraseRebote
{
    class Program
    {
        static void Main(string[] args)
        {
        #region Variables y Constantes

            string frase=string.Empty; //frase que se muestra rebotando
            const int TMAX=200; //tiempo máximo de espera
            const int TMIN=30;  //tiempo minimo de espera
            const int MAXLONFRASE = 50; //longitud máxima de la frase
            const int ESPERA = 2500;
            int alto = 10; //alto de la pantalla
            int tiempo = 90,    //tiempo de espera en el Thread
            posicion = 0,       //posicion en la que se encuentra la frase
            salto = 1,          //la distancia del salto
            longFrase =0; //longitud de la frase que se pone

            // Maximo y minimo del ancho de la pantalla
            int max = Console.WindowWidth;
            int min = 0;

            ConsoleKeyInfo teclaPulsada= new ConsoleKeyInfo();// declaracion de la tecla
            #endregion 

            try
            {
                frase = Frase();

                #region Control de la frase

                if (frase.Length == 0 || (frase.Replace(" ","")).Length==0)  // si la frase esta vacia yo le pongo por defecto una frase
                {
                    frase = "Console.Escribe";
                }
                if (frase.Length > MAXLONFRASE)
                {
                    Console.WriteLine("Frase demasiado larga, máximo 50 caractertes(se recorta la frase)");
                    Thread.Sleep(ESPERA);
                    frase = frase.Substring(0, MAXLONFRASE);
                    frase = frase.Trim();
                }
                frase = " " + frase + " ";
                longFrase = frase.Length-1; //longitud de la frase para controlar el rebote el -1 es para controlar el rebote
                #endregion

                Console.Clear(); // limpieza de pantalla para que solo se vea la frase rebotando.

                //Bucle principal
                #region Bucle

                Console.WriteLine("Pulsa Esc para salir\nFlecha direccion arriba aumenta la velocidad y la de abajo para disminuir");
                while (true)
                {
                    Console.CursorTop = alto;   //asignacion de la posicion vertical de la frase
                    Console.CursorLeft = posicion;  //asignacion de la posicion lateral de la frase
                    Console.Write(frase);           //mostrar la frase
                    posicion += salto;                //incremento de la posicion

                    #region control pantalla
                    ///control para que la frase no se salga de la pantalla
                    if ((posicion + longFrase) == max) // control a la derecha de la pantalla
                    {
                        salto *= -1;        //inversor de la direccion
                        color();            //llamada al metodo que colorea la cadena de string
                    }

                    if (posicion == min) // control a la izquierda de la pantalla
                    {
                        salto *= -1;        // inversor de la direccion
                        color();
                    }
                    #endregion

                    //lectura de la tecla si se ha pulsado o no
                    #region lectura teclas aumento decremento escape

                    if (Console.KeyAvailable)//comprueba si hay lectura en el buffer
                    {
                        teclaPulsada = Console.ReadKey(true);
                        if (teclaPulsada.Key == ConsoleKey.UpArrow && tiempo >= TMIN)//+ velocidad
                        {
                            tiempo -= 10;
                        }
                        if (teclaPulsada.Key == ConsoleKey.DownArrow && tiempo <= TMAX)//- velocidad
                        {
                            tiempo += 10;
                        }
                        if (teclaPulsada.Key == ConsoleKey.Escape)  //sale si se pulsa la tecla escape
                        {
                            Environment.Exit(0);
                        }
                    }
                    #endregion

                    Thread.Sleep(tiempo); //espera
                }
                #endregion
            }
            catch (Exception)
            {
                Console.WriteLine("Ups!, Error inesperado");
                Console.ReadLine();
            }

        }
        /// <summary>
        /// metodo para obtener la frase que el usuario puede introducir
        /// </summary>
        /// <returns>Frase que se va a mostrar en pantalla rebotando</returns>
        static string Frase()
        {
                string frase = string.Empty;
                Console.WriteLine("Introduzca la frase que desea ver rebotando: ");
                frase = Console.ReadLine();
                frase = frase.Trim();
               return frase;
        }

        #region color a la palabra

        static void color()
        {
            #region variables

            int numerocolor=0;
            //se crea un array de tipo console color al que se le añaden los colores de la enumeracion de ConsoleColor
            ConsoleColor[] colores = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));
            Random rnd = new Random();
            #endregion

            numerocolor =rnd.Next(colores.Length);

            if (colores[numerocolor] != ConsoleColor.Black)// como la pantalla es negra de fondo se poene el texto para evitar que no se vea la frase al tener el color negro
            {
                Console.ForegroundColor = colores[numerocolor];
            }

        }
        #endregion
    }
}
