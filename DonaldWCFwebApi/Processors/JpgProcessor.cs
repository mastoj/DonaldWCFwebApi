using System;
using System.IO;
using System.Net;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web;
using DonaldWCFwebApi.Resources;

namespace DonaldWCFwebApi.Processors
{
    public class JpgProcessor : MediaTypeFormatter
    {
        public JpgProcessor()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/jpg"));
        }
        protected override object OnReadFromStream(Type type, Stream stream, HttpContentHeaders contentHeaders)
        {
            throw new NotImplementedException();
        }

        protected override void OnWriteToStream(Type type, object value, Stream stream, HttpContentHeaders contentHeaders, TransportContext context)
        {
            var donaldEpisode = value as DonaldEpisode;
            var path = HttpContext.Current.Server.MapPath("~/Content/images/" + donaldEpisode.CoverArt);
            //contentHeaders.ContentDisposition = new ContentDispositionHeaderValue("inline") {FileName = donaldEpisode.CoverArt};

            using (var file = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                file.CopyTo(stream);
            }
        }
    }
}