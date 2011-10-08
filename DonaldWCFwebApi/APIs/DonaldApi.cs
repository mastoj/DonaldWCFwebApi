using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Web;
using DonaldWCFwebApi.Resources;

namespace DonaldWCFwebApi.APIs
{
    [ServiceContract]
    public class DonaldApi
    {
        public static List<DonaldEpisode> episodes = new List<DonaldEpisode>
                                                         {
                                                             CreateEpisode("Donald goes to heaven",
                                                                           "Donald goes to heaven and everyone is happy!", 
                                                                           "images/heaven.jpg",
                                                                           "Walt Disney",
                                                                           DateTime.Now.AddDays(-2).Date),
                                                             CreateEpisode("Donald goes to hell",
                                                                           "Donald goes to hell and everyone is sad :(", 
                                                                           "images/hell.jpg",
                                                                           "Tomas Jansson",
                                                                           DateTime.Now.AddDays(-1).Date),
                                                             CreateEpisode("Donald is in the forest",
                                                                           "Donald smells the trees in the forrest.", 
                                                                           "images/forrest.jpg",
                                                                           "Chuck Norris",
                                                                           DateTime.Now.Date),
                                                             CreateEpisode("Donald have a sit down with Obama and Osama",
                                                                           "Donald fix world peace and everyone is happy.", 
                                                                           "images/peace.jpg",
                                                                           "Clint Eastwood",
                                                                           DateTime.Now.AddDays(1).Date)

                                                         };

        private static DonaldEpisode CreateEpisode(string title, string content, string artWorkUrl, string author, DateTime releaseDate)
        {
            return new DonaldEpisode()
                       {
                           Title = title,
                           Content = content,
                           ArtWorkUrl = artWorkUrl,
                           Author = author,
                           ReleaseDate = releaseDate
                       };
        }

        [WebGet(UriTemplate = "")]
        public IEnumerable<DonaldEpisode> Get()
        {
            return episodes;
        }
    }
}