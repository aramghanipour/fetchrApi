namespace Fetchr.Models
{
    using Fetchr.Models.Enums;

    public class SiteModel
    {
        public int SiteId { get; set; }

        public string Name { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public SiteTypes SiteType { get; set; }
    }
}