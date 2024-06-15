using Newtonsoft.Json;

namespace EmployeeManagementSystem.Common
{
    public class Address
    {
        [JsonProperty(PropertyName = "houseNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string HouseNumber { get; set; }

        [JsonProperty(PropertyName = "cityName", NullValueHandling = NullValueHandling.Ignore)]
        public string CityName { get; set; }

        [JsonProperty(PropertyName = "districName", NullValueHandling = NullValueHandling.Ignore)]
        public string DistricName { get; set; }
    }
}
