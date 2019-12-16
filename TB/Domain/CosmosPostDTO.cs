using Cosmonaut.Attributes;
using Newtonsoft.Json;

namespace TB.Domain
{
    [CosmosCollection("posts")]
    public class CosmosPostDTO
    {
        [CosmosPartitionKey]
        [JsonProperty("id")]
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
