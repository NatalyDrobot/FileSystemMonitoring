using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace FileSystemWatcherConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            string[] path = new string[1];
            path[0] = @"D:\Temp";

            if (args.Length == 0)
            {
                OutputHelp();
                return;
            }

            switch (args[0])
            {
                case "-help":
                {
                    OutputHelp();
                }
                    break;
                case "-path":
                {
                    path[0] = args[1];
                    Service.StartService(path, "FSWatcherService");
                }
                    break;
                case "-date":
                {
                    Events.GetEventForDate(args[1]);
                }
                    break;
                default:
                    break;
            }
        }

        private static void OutputHelp()
        {
            Console.WriteLine(" *** Мониторинг файловой системы *** " );
            Console.WriteLine(" Параметры запуска" );
            Console.WriteLine("-help  Вызов справки");
            Console.WriteLine(@"-path  Для смены пути к каталогу для мониторинга. Пример: -path D:\Test");
            Console.WriteLine("-date  Для выбора событий по конкретной дате. Пример: -date 2011-10-10");
            
            Console.Read();
        }
        
    }
}
