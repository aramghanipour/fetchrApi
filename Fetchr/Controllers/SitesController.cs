namespace Fetchr.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Runtime.Serialization.Json;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Cors;

    using Fetchr.Models;

    using Newtonsoft.Json;

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SitesController : ApiController
    {
        [Route("sites/getall")]
        [HttpGet]
        public async Task<IEnumerable<SiteModel>> GetSites()
        {
            List<SiteModel> sites = new List<SiteModel>();

            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            var json = wc.DownloadString("http://api.sl.se/api2/typeahead.json?key=ce888553d33f433d97f54b8bca411f50&searchString=Norgegatan");

            dynamic response = JsonConvert.DeserializeObject(json);

            foreach (var data in response.ResponseData)
            {
                SiteModel site = new SiteModel()
                                  {
                                      SiteId = data.SiteId,
                                      Name = data.Name,
                                      SiteType = data.Type,
                                      Longitude = data.X,
                                      Latitude = data.Y
                                  };

                sites.Add(site);
            }
            
            return sites;
        }

        [Route("sites/get/{name}")]
        [HttpGet]
        public async Task<SiteModel> GetSite(string name)
        {
            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            var json = await wc.DownloadStringTaskAsync("http://api.sl.se/api2/typeahead.json?key=ce888553d33f433d97f54b8bca411f50&searchstring=" + name + "&stationsonly=true&maxresults=1");

            dynamic response = JsonConvert.DeserializeObject(json);

            SiteModel site = null;
            
            foreach (var data in response.ResponseData)
            {
                site = new SiteModel()
                {
                    SiteId = data.SiteId,
                    Name = data.Name,
                    SiteType = data.Type,
                    Longitude = data.X,
                    Latitude = data.Y
                };

                break;
            }

            return site;
        }
    }
}
