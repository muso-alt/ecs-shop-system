using Newtonsoft.Json;

namespace ShopComplex.Containers
{
    public class Container
    {
        public Container(int id, string value)
        {
            this.id = id;
            this.value = value;
        }
        
        [JsonProperty]
        public int id { get; set; }
        
        [JsonProperty]
        public string value { get; set; }
    }
}