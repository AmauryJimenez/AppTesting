using Newtonsoft.Json;
using System.Collections.Generic;

namespace RestSharpExamples.DataEntities
{
    public class CasesResponse
    {
        [JsonProperty("@odata.totalCount")]
        public int totalCount { get; set; }
        [JsonProperty("@odata.context")]
        public string context { get; set; }       
        [JsonProperty("value")]
        public List<CasesDetails> CasaesDetails { get; set; }
    }
}
