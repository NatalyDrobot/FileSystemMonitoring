using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSWatcherService
{
    class Event
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string TypeObj { get; set; }
        public string TypeEvent { get; set; }

        public DateTime DateEvent { get; set; }
    }
}
