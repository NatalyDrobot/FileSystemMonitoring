using System;
using System.ComponentModel.DataAnnotations;

namespace FileSystemWatcherDAL.Entities
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        public string Path { get; set; }
        public string TypeObj { get; set; }
        public string TypeEvent { get; set; }

        public DateTime DateEvent { get; set; }
    }
}
