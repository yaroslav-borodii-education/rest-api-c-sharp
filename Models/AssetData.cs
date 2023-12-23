using Newtonsoft.Json;

namespace RivneOneLove.Models
{
    public class AssetDataResponse
    {
        [JsonProperty("data")]
        public AssetData Data { get; set; }
    }

    public class AssetData
    {
        public string Id { get; set; }
        public string Rank { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Supply { get; set; }
        public string MaxSupply { get; set; }
        public string MarketCapUsd { get; set; }
        public string VolumeUsd24Hr { get; set; }
        public string PriceUsd { get; set; }
        public string ChangePercent24Hr { get; set; }
        public string Explorer { get; set; }
    }
}
