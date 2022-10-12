using System;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;

namespace mészöly_marcell_HKDXX6_SzTF1NagyHázi
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode; //az emojik megjelenítéséhez
            Writer.BaseBgColor = Console.BackgroundColor;
            Writer.BaseFgColor = Console.ForegroundColor;

            Writer.WriteWelcome();

            Writer.AskForPath();
            string path = string.Empty;
            path += "./Source/";
            path += Console.ReadLine();

            if (!File.Exists(path))
            {
                Writer.WriteError("A fájl nem létezik.");
                Console.ReadKey();
                Environment.Exit(0);
            }

            StreamReader sr = new StreamReader(path);

            string errorDesc = String.Empty;

            Game game = Game.Parse(sr.ReadToEnd(), ref errorDesc);
            if (game == null)
            {
                Writer.WriteError(errorDesc);
                Console.ReadKey();
                Environment.Exit(0);
            }
            sr.Close();
            Writer.WriteDivider();
            Writer.WriteSuccessfulRead();

            game.Run();

            Environment.Exit(0);

        }
    }
}
