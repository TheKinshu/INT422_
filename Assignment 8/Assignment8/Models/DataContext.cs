using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Assignment8.Models
{
    public partial class DataContext : DbContext
    {

        public DataContext() : base("name-DataContext") { }
        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<Artist> Artists { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Track> Tracks { get; set; }

    }
}