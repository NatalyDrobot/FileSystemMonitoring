using System;
using System.Data.Entity;
using FileSystemWatcherDAL.Entities;

namespace FileSystemWatcherDAL
{
    public class FileWatcherContext: DbContext
    {
            public FileWatcherContext()
                : base("FileWatcherContext")
            { }

            public virtual DbSet<Event> Events { get; set; }
            public virtual DbSet<User> Users { get; set; }
        }
    }
