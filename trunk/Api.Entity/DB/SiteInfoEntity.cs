using System;

namespace Api.Entity.DB
{
    [Serializable]
    public class SiteInfoEntity
    {
        public int ID { get; set; }
       public string SiteName { get; set; }
    }
}
