using System;
using System.Text.RegularExpressions;
using OpenCvSharp;

class Programa
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("por favor arrastre un video y luego presione enter");
        }
        string video = Console.ReadLine();
        if (System.IO.File.Exists(video) == false)
        {
            Console.Write("el archivo del video no existe");
            return;
        }
        System.IO.Directory.CreateDirectory("frames");

        var captura = new VideoCapture(video);
        var ventana = new Window("Prueba de 10 segundos - Prueva OpenCVSharp guardar frames ");
        var imagen = new Mat();

        var fps = captura.Get(VideoCaptureProperties.Fps);
        var framestotales = (int)captura.Get(VideoCaptureProperties.FrameCount);
        var duracion = framestotales / fps;
        if (duracion > 10)
        {
            Console.WriteLine("el video que abrio dura mas de 10 segundos, presione enter para salir y vuelva a iniciar");
            Console.ReadLine();
            Environment.Exit(0);
        }
        else
        {
            var i = 0;
            while (captura.IsOpened())
            {
                {
                    captura.Read(imagen);
                    if (imagen.Empty()) { break; }
                    i++;
                    var numFrame = i.ToString().PadLeft(8, '0');
                    var NomFrame = $@"frames\imaage{numFrame}.png";
                    Cv2.ImWrite(NomFrame, imagen);

                    ventana.ShowImage(imagen);
                    if (Cv2.WaitKey(1) == 120) //boton para parar el proceso, letra X
                        break;
                }
            }
            Console.WriteLine("Finalizo el programa");
        }
    }
}