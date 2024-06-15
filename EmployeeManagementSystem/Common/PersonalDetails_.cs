using Newtonsoft.Json;


namespace EmployeeManagementSystem.Common
{
    public class PersonalDetails_
    {
        [JsonProperty(PropertyName = "dateOfBirth", NullValueHandling = NullValueHandling.Ignore)]
       
        public DateTime DateOfBirth { get; set; }

        [JsonProperty(PropertyName = "age", NullValueHandling = NullValueHandling.Ignore)]
        public string Age { get; set; }

        [JsonProperty(PropertyName = "gender", NullValueHandling = NullValueHandling.Ignore)]
        public string Gender { get; set; }

        [JsonProperty(PropertyName = "religion", NullValueHandling = NullValueHandling.Ignore)]

        public string Religion { get; set; }

        [JsonProperty(PropertyName = "caste", NullValueHandling = NullValueHandling.Ignore)]
        public string Caste { get; set; }

        [JsonProperty(PropertyName = "martialStatus", NullValueHandling = NullValueHandling.Ignore)]
        public string MartialStatus { get; set; }

        [JsonProperty(PropertyName = "bloodGroup", NullValueHandling = NullValueHandling.Ignore)]
        public string BloodGroup { get; set; }

        [JsonProperty(PropertyName = "height", NullValueHandling = NullValueHandling.Ignore)]
        public string Height { get; set; }

        [JsonProperty(PropertyName = "weight", NullValueHandling = NullValueHandling.Ignore)]

        public string Weight { get; set; }




    }
}
