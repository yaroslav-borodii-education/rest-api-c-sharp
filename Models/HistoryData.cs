using Newtonsoft.Json;

/*
"priceUsd": "0.6425724849494929",
"time": 1702110600000,
"circulatingSupply": "35318996784.3400000000000000",
"date": "2023-12-09T08:30:00.000Z"
*/

namespace RivneOneLove.Models
{
    public class HistoryDataResponse
    {
        [JsonProperty("data")]
        public List<HistoryData> Data { get; set; }
    }

    public class HistoryData
    {
        public string PriceUsd { get; set; }
        public long Time { get; set; }
        public string CirculatingSupply { get; set; }
        public string Date { get; set; }
        
    }
}
