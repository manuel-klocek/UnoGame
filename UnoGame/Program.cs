using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnoGame.Repositories;

namespace UnoGame
{
    internal static class Program
    {
        static void Main()
        {
            ///THESE LINES NEED TO BE IN THE PRODUCTION SYSTEM!!
            ///On my branch they can't be activated because obsoleted support of WinForms
            ///mono32 not working on macOS Catalania or higher -> mono64 no support for WinForms


            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            new GameInConsole().Run();
            Console.ReadLine();
        }
    }
}