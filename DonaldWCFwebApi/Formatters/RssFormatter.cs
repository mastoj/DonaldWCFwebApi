using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.ServiceModel.Syndication;
using System.Text;
using System.Web;
using System.Xml;
using DonaldWCFwebApi.Resources;

namespace DonaldWCFwebApi.Formatters
{
    public class RssFormatter : MediaTypeFormatter
    {
        public RssFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/rss+xml"));
        }

        protected override object OnReadFromStream(Type type, Stream stream, HttpContentHeaders contentHeaders)
        {
            throw new NotImplementedException();
        }

        protected override void OnWriteToStream(Type type, object value, Stream stream, HttpContentHeaders contentHeaders, TransportContext context)
        {
            var feed = new SyndicationFeed("Donald episodes", "Donald episodes for your search",
                                           contentHeaders.ContentLocation);
            var donaldEpisodes = value as IQueryable<DonaldEpisode>;
            if (donaldEpisodes != null)
            {
                WriteFeed(donaldEpisodes, stream, HttpContext.Current.Request.Url.OriginalString);
            }
            else
            {
                var donaldEpisode = value as DonaldEpisode;
                if (donaldEpisode != null)
                {
                    WriteFeed(new[] { donaldEpisode }, stream, contentHeaders.ContentLocation.OriginalString);
                }
            }
        }

        private void WriteFeed(IEnumerable<DonaldEpisode> episodes, Stream responseStream, string url)
        {
            XmlTextWriter objX = new XmlTextWriter(responseStream, Encoding.UTF8);
            objX.WriteStartDocument();
            objX.WriteStartElement("rss");
            objX.WriteAttributeString("version", "2.0");
            objX.WriteStartElement("channel");
            objX.WriteElementString("title", "Donald Inc.");
            objX.WriteElementString("link", url);
            objX.WriteElementString("description",
                                    "Your custom donald feed!");
            objX.WriteElementString("copyright", "(c) 2011, Chuck Norris.");
            objX.WriteElementString("ttl", "5");
            foreach (var episode in episodes)
            {
                objX.WriteStartElement("item");
                objX.WriteElementString("title", episode.Title);
                objX.WriteElementString("description", episode.Content);
                objX.WriteElementString("link",
                                        Configuration.ApiUri + "/" + episode.Id);
                objX.WriteElementString("pubDate", episode.ReleaseDate.Value.ToString("R"));
                objX.WriteStartElement("enclosure");
                objX.WriteAttributeString("url", Configuration.ApiUri + "/" + episode.Id + "/jpg");
                objX.WriteAttributeString("type", "application/jpg");
                objX.WriteEndElement();
                objX.WriteEndElement();
            }

            objX.WriteEndElement();
            objX.WriteEndElement();
            objX.WriteEndDocument();
            objX.Flush();
        }
    }
}