using Newtonsoft.Json;
using System.Collections.Generic;

namespace RestSharpExamples.DataEntities
{
    public class CasesDetails
    {
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("creationDate")]
        public string creationDate { get; set; }
        [JsonProperty("estimatedSolutionDate")]
        public string solutionDate { get; set; }
        [JsonProperty("processName")]
        public string processName { get; set; }
        [JsonProperty("closed")]
        public string isClosed { get; set; }

    }
}
