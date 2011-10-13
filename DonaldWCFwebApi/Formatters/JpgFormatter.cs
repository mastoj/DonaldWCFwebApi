using System;
using System.IO;
using System.Net;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web;
using DonaldWCFwebApi.Resources;

namespace DonaldWCFwebApi.Processors
{
    public class JpgFormatter : MediaTypeFormatter
    {
        public JpgFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/jpg"));
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("image/jpg"));
        }
        protected override object OnReadFromStream(Type type, Stream stream, HttpContentHeaders contentHeaders)
        {
            throw new NotImplementedException();
        }

        protected override void OnWriteToStream(Type type, object value, Stream stream, HttpContentHeaders contentHeaders, TransportContext context)
        {
            var donaldEpisode = value as DonaldEpisode;
            var path = HttpContext.Current.Server.MapPath("~/Content/images/" + donaldEpisode.CoverArt);
            HttpContext.Current.Response.AppendHeader("content-disposition",
                                                      "attachment;filename=" + donaldEpisode.CoverArt);
            // below does not work
            //contentHeaders.ContentDisposition = new ContentDispositionHeaderValue("attachment") {FileName = donaldEpisode.CoverArt};

            using (var file = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                file.CopyTo(stream);
            }
        }
    }
}