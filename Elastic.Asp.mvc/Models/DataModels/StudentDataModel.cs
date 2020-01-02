using Elastic.Asp.mvc.Models.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Elastic.Asp.mvc.Models.DataModels
{
    public class Shards
    {
        public int total { get; set; }
        public int successful { get; set; }
        public int skipped { get; set; }
        public int failed { get; set; }
    }

    public class Total
    {
        public int value { get; set; }
        public string relation { get; set; }
    }

    
    public class Hit<T>
    {
        public string _index { get; set; }
        public string _type { get; set; }
        public string _id { get; set; }
        public double _score { get; set; }

        [JsonProperty("_source")]
        public T TSource { get; set; }
    }

    public class Hits<T>
    {
        public Total total { get; set; }
        public double max_score { get; set; }
        [JsonProperty("hits")]
        public List<Hit<T>> THitsList { get; set; }
    }

    public class RootObjectList<T>
    {
        public int took { get; set; }
        public bool timed_out { get; set; }
        public Shards _shards { get; set; }

        [JsonProperty("hits")]
        public Hits<T> THits { get; set; }
    }

    public class RootObject<T>
    {
        public string _index { get; set; }
        public string _type { get; set; }
        public string _id { get; set; }
        public int _version { get; set; }
        public int _seq_no { get; set; }
        public int _primary_term { get; set; }
        public bool found { get; set; }

        [JsonProperty("_source")]
        public T TSource { get; set; }
    }
}
