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
                                                             CreateEpisode(1, 
                                                                            "Donald goes to heaven",
                                                                           "Donald goes to heaven and everyone is happy!", 
                                                                           "heaven.jpg",
                                                                           "Walt Disney",
                                                                           DateTime.Now.AddDays(-2).Date),
                                                             CreateEpisode(2,
                                                                           "Donald goes to hell",
                                                                           "Donald goes to hell and everyone is sad :(", 
                                                                           "hell.jpg",
                                                                           "Tomas Jansson",
                                                                           DateTime.Now.AddDays(-1).Date),
                                                             CreateEpisode(3, 
                                                                           "Donald is in the forest",
                                                                           "Donald smells the trees in the forrest.", 
                                                                           "forrest.jpg",
                                                                           "Chuck Norris",
                                                                           DateTime.Now.Date),
                                                             CreateEpisode(4,
                                                                           "Donald have a sit down with Obama and Osama",
                                                                           "Donald fix world peace and everyone is happy.", 
                                                                           "peace.jpg",
                                                                           "Clint Eastwood",
                                                                           DateTime.Now.AddDays(1).Date)

                                                         };

        private static DonaldEpisode CreateEpisode(int id, string title, string content, string artWorkUrl, string author, DateTime releaseDate)
        {
            return new DonaldEpisode()
                       {
                           Id = id,
                           Title = title,
                           Content = content,
                           CoverArt = artWorkUrl,
                           Author = author,
                           ReleaseDate = releaseDate
                       };
        }

        [WebGet(UriTemplate = "")]
        public IQueryable<DonaldEpisode> Get()
        {
            return episodes.AsQueryable();
        }

        [WebGet(UriTemplate = "{id}")]
        public DonaldEpisode GetId(int id)
        {
            return episodes.SingleOrDefault(y => y.Id == id);
        }

        [WebInvoke(UriTemplate = "", Method = "POST")]
        public DonaldEpisode Post(DonaldEpisode donaldEpisode)
        {
            var maxId = episodes.Select(y => y.Id).Max();
            var newId = maxId + 1;
            donaldEpisode.Id = newId;
            episodes.Add(donaldEpisode);
            return donaldEpisode;
        }

        [WebInvoke(UriTemplate = "{id}", Method = "PUT")]
        public DonaldEpisode Put(DonaldEpisode donaldEpisode, int id)
        {
            donaldEpisode.Id = id;
            episodes.RemoveAll(y => y.Id == id);
            episodes.Add(donaldEpisode);
            return donaldEpisode;
        }

        [WebInvoke(UriTemplate = "{id}", Method = "DELETE")]
        public void Delete(int id)
        {
            episodes.RemoveAll(y => y.Id == id);
        }
    }
}