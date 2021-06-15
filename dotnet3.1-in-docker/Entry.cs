
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

/*
 Mohlo by být sloučeno do 1 třídy -> předěláno na structuru
 */
namespace dotnet3._1_in_docker
{
    public class EntryBase
    {
        public string communication { get; set; }       
    }

    public class Entry : EntryBase
    {

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum LocationType { Country = 1, City = 2, Zip = 3};
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum Status { Created = 1, Contracted = 2};
        

        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }        
        public LocationType location_type { get; set; }
        public string location_string { get; set; }        
        public Status status { get; set; }
    }
}
