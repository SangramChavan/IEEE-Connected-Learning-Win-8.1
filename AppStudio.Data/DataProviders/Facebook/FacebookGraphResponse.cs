using System;

namespace AppStudio.Data
{
    public class FacebookGraphResponse
    {
        public GraphData[] data { get; set; }
    }

    public class GraphData
    {
        public string id { get; set; }
        public From from { get; set; }
        public string type { get; set; }
        public DateTime created_time { get; set; }
        public DateTime updated_time { get; set; }
        public string message { get; set; }
        public string picture { get; set; }
        public string link { get; set; }
    }

    public class From
    {
        public string category { get; set; }
        public string name { get; set; }
        public string id { get; set; }
    }
}

