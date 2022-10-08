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
            UIWriter writer = new UIWriter();

            writer.WriteWelcome();

            writer.AskForPath();
            string path = string.Empty;
            path += "./Source/";
            path += Console.ReadLine();

            InputReader inputReader = new InputReader(path);
            Game game = inputReader.ReadGame(ref writer);
            writer.WriteSuccessfulRead();

            game.Run(ref writer);

            Environment.Exit(0);

        }
    }
}
