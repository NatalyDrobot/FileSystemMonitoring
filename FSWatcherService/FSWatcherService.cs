using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.ServiceProcess;
using System.Threading;

namespace FSWatcherService
{
    public partial class FSWatcherService : ServiceBase
    {
        Logger logger;
        public FSWatcherService()
        {
            InitializeComponent();
            this.CanStop = true;
            this.CanPauseAndContinue = true;
            this.AutoLog = true;
        }

        protected override void OnStart(string[] args)
        {
            string path = @"D:\Temp";
            if (args.Length != 0)
                path = args[0];
            logger = new Logger(path);
            Thread loggerThread = new Thread(new ThreadStart(logger.Start));
            loggerThread.Start();
        }

        protected override void OnStop()
        {
            logger.Stop();
            Thread.Sleep(1000);
        }
    }

    class Logger
    {
        FileSystemWatcher watcher;
        object obj = new object();
        bool enabled = true;
        public Logger(string path)
        {
            watcher = new FileSystemWatcher(path);
            watcher.Deleted += Watcher_Deleted;
            watcher.Created += Watcher_Created;
            watcher.Changed += Watcher_Created;
            watcher.Renamed += Watcher_Renamed;
        }

        public void Start()
        {
            watcher.EnableRaisingEvents = true;
            while (enabled)
            {
                Thread.Sleep(1000);
            }
        }
        public void Stop()
        {
            watcher.EnableRaisingEvents = false;
            enabled = false;
        }

        // переименование файлов
        private void Watcher_Renamed(object sender, RenamedEventArgs e)
        {
            Event newEvent = new Event
            {
                DateEvent = DateTime.Now,
                Path = e.OldFullPath,
                TypeEvent = e.ChangeType.ToString() + " in "+ e.FullPath,
                TypeObj = GetAttributeObj(e.FullPath)
            };

            RecordEvent(newEvent);
        }

        // создание или изменение файлов
        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            Event newEvent = new Event
            {
                DateEvent = DateTime.Now,
                Path = e.FullPath,
                TypeEvent = e.ChangeType.ToString(),
                TypeObj = GetAttributeObj(e.FullPath)
            };

            RecordEvent(newEvent);
        }

        // удаление файлов
        private void Watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            Event newEvent = new Event
            {
                DateEvent = DateTime.Now,
                Path = e.FullPath,
                TypeEvent = e.ChangeType.ToString(),
                TypeObj = "-"
            };

            RecordEvent(newEvent);
        }

        private static string GetAttributeObj(string path)
        {
            string typeObj = null;
            FileAttributes attrFile = File.GetAttributes(path);
            if ((attrFile & FileAttributes.Directory) == FileAttributes.Directory)
            {
                typeObj = "Directory";
            }
            else
            {
                typeObj = "File";
            }

            return typeObj;
        }

        private void RecordEvent(Event newEvent)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:26586/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.PostAsJsonAsync("api/event", newEvent);
                //var response = client.GetAsync("api/event");
                Thread.Sleep(1000);
            }
        }
    }
}
