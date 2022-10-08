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

            UIWriter.WriteWelcome();

            UIWriter.AskForPath();
            string path = string.Empty;
            path += "./Source/";
            path += Console.ReadLine();

            InputReader inputReader = new InputReader(path);
            Game game = inputReader.ReadGame();
            UIWriter.WriteSuccessfulRead();

            game.Run();

            Environment.Exit(0);

        }
    }
}
