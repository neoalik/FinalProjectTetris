using System;
using Tetris.Game;

namespace Tetris
{
    class Program
    {
        static void Main(string[] args)
        {
            //grid X => 0 - 9, y => 0 - 21
            Utils.DisableConsoleQuickEdit.Go();//cia tam kad pele negalima butu statyti zemeklio
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.CursorVisible = false;

            GuiController guiController = new GuiController(0, 0, 120, 30, '░');
            guiController.ShowMenu();



            /*Console.SetCursorPosition(5, 16);

            Console.WriteLine("■■■");

            Console.SetCursorPosition(5, 17);

            Console.WriteLine("■");*/

            /*Console.WriteLine("Test");


            Console.SetCursorPosition(0, 21);

            Console.WriteLine("■■■■■■■■■■");

            Console.ReadKey();*/
        }
    }
}
