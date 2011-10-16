using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace DonaldWCFwebApi.MessageHandlers
{
    public class UriFormatHandler : DelegatingHandler
    {
        private Dictionary<string, MediaTypeWithQualityHeaderValue> mappings;
        public UriFormatHandler()
        {
            mappings = new UriExtensionMappings().ToDictionary(y => y.Extension, y => y.MediaType);
        }

        protected override System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            var lastSegment = request.RequestUri.Segments.LastOrDefault();
            MediaTypeWithQualityHeaderValue mediaType = null;
            var foundMappings = mappings.TryGetValue(lastSegment, out mediaType);
            if (foundMappings)
            {
                var newUri = request.RequestUri.OriginalString.Replace("/" + lastSegment, "");
                request.RequestUri = new Uri(newUri, UriKind.Absolute);
                request.Headers.Accept.Clear();
                request.Headers.Accept.Add(mediaType);
            }
            return base.SendAsync(request, cancellationToken);
        }
    }

    public class UriExtensionMapping
    {
        public string Extension { get; set; }
        public MediaTypeWithQualityHeaderValue MediaType { get; set; }
    }

    public class UriExtensionMappings : List<UriExtensionMapping>
    {
        public UriExtensionMappings()
        {
            Add(new UriExtensionMapping { Extension = "json", MediaType = new MediaTypeWithQualityHeaderValue("application/json") });
            Add(new UriExtensionMapping { Extension = "jpg", MediaType = new MediaTypeWithQualityHeaderValue("image/jpg") });
            Add(new UriExtensionMapping { Extension = "jpga", MediaType = new MediaTypeWithQualityHeaderValue("application/jpg") });
            Add(new UriExtensionMapping { Extension = "odata", MediaType = new MediaTypeWithQualityHeaderValue("application/atom+xml") });
            Add(new UriExtensionMapping() { Extension = "rss", MediaType = new MediaTypeWithQualityHeaderValue("application/rss+xml") });

        }
    }
}