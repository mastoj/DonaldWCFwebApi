using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using DonaldWCFwebApi.Resources;

namespace DonaldWCFwebApi.Controllers
{
    public class HomeController : Controller
    {
        private static string uri = Configuration.ApiUri;
        //
        // GET: /Home/

        public ActionResult Index()
        {

            HttpClient client = new HttpClient();// {BaseAddress = uri};
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = client.Get(uri);

            var donaldEpisodes = response.Content.ReadAs<List<DonaldEpisode>>();
            ViewBag.ApiUri = uri;
            return View(donaldEpisodes);
        }

    }
}
