using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnoGame
{
    internal static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            var logic = new Logic();
            Console.WriteLine("Geben Sie die Anzahl der Spieler an: ");
            var playerNum = Convert.ToInt32(Console.ReadLine());
            logic.StartGame(new SetupCards().Run(), playerNum);
        }
    }
}