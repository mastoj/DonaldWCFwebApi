using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DonaldWCFwebApi.Resources
{
    public class DonaldEpisode
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string CoverArt { get; set; }
        public string Author { get; set; }
        public DateTime? ReleaseDate { get; set; }
    }
}