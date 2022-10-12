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

            Game game = null;
            readInput(ref game);

            game.Run();

            Environment.Exit(0);

        }

        /// <summary>
        /// A beolvasás indításáért/lefuttatásáért felel
        /// </summary>
        /// <param name="game">Az objektum, amibe írjuk a játékot.</param>
        private static void readInput(ref Game game)
        {

            string path = string.Empty;
            path += "./Source/";
            string fileFound = Path.GetFileName(TryFindFile());

            if (fileFound != string.Empty)
            {
                Writer.AskIfFoundFileIsOk(fileFound);
                string YesNo = Console.ReadLine();

                while (YesNo != "I" && YesNo != "i" && YesNo != "N" && YesNo != "n")
                {
                    Writer.WriteErrorYesNoAnswer();
                    YesNo = Console.ReadLine();
                }

                if (YesNo == "I" || YesNo == "i")
                {
                    path += fileFound;
                }
                else
                {
                    Writer.AskForPath();
                    
                    path += Console.ReadLine();
                }
            }
            else
            {
                Writer.AskForPath();
                path += Console.ReadLine();
            }


            

            

            if (!File.Exists(path))
            {
                Writer.WriteError("A fájl nem létezik.");
                Console.ReadKey();
                Environment.Exit(0);
            }

            StreamReader sr = new StreamReader(path);

            string errorDesc = String.Empty;

            game = Game.Parse(sr.ReadToEnd(), ref errorDesc);
            sr.Close();
            if (game == null)
            {
                Writer.WriteError(errorDesc);
                Console.ReadKey();
                Environment.Exit(0);
            }
            Writer.WriteDivider();
            Writer.WriteSuccessfulRead();
        }

        private static string TryFindFile()
        {
            string[] files = Directory.GetFiles("./Source/");
            int i = 0;
            string fileName = string.Empty;
            while(i < files.Length && fileName == string.Empty)
            {
                if (files[i].EndsWith(".txt") && !files[i].Contains("0ReadMe.txt"))
                {
                    fileName = files[i];
                }
                i++;
            }
            return fileName;

        }
    }
}
