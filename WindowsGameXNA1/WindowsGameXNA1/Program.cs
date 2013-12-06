using System;
using System.Diagnostics;

namespace WindowsGameXNA1
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            Trace.WriteLine("starting");

            using (Game1 game = new Game1())
            {
                game.Run();
            }
        }
    }
#endif
}

