using EvilEye.SDK;
using System;



namespace EvilEye.Module.Settings
{
    class ConsoleClear : BaseModule
    {
        public ConsoleClear() : base("Clear Console", "Clears Melon Loader Console", Main.Instance.settingsMisc, null, false)
        {
        }
        public override void OnEnable()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("=============================================================================================================");
            Console.WriteLine("                                                                                                             ");
            Console.WriteLine("                          ███████╗██╗░░░██╗██╗██╗░░░░░  ███████╗██╗░░░██╗███████╗                            ");
            Console.WriteLine("                          ██╔════╝██║░░░██║██║██║░░░░░  ██╔════╝╚██╗░██╔╝██╔════╝                            ");
            Console.WriteLine("                          █████╗░░╚██╗░██╔╝██║██║░░░░░  █████╗░░░╚████╔╝░█████╗░░                            ");
            Console.WriteLine("                          ██╔══╝░░░╚████╔╝░██║██║░░░░░  ██╔══╝░░░░╚██╔╝░░██╔══╝░░                            ");
            Console.WriteLine("                          ███████╗░░╚██╔╝░░██║███████╗  ███████╗░░░██║░░░███████╗                            ");
            Console.WriteLine("                          ╚══════╝░░░╚═╝░░░╚═╝╚══════╝  ╚══════╝░░░╚═╝░░░╚══════╝                            ");
            Console.WriteLine("                                 Beta Release By Four_DJ, Literal, and Fish.                                 ");
            Console.WriteLine("=============================================================================================================");
            Console.ForegroundColor = ConsoleColor.White;
            LoggerUtill.Log("Cleared Console!", ConsoleColor.Green);
        }
    }
}
