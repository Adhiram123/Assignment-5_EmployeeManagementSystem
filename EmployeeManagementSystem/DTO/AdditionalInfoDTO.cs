using EmployeeManagementSystem.Common;
using Newtonsoft.Json;

namespace EmployeeManagementSystem.DTO
{
    public class AdditionalInfoDTO
    {
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]

        public string Id { get; set; }

        [JsonProperty(PropertyName = "uid", NullValueHandling = NullValueHandling.Ignore)]
        public string UId { get; set; }

        [JsonProperty(PropertyName = "employeeBasicDetailsUid", NullValueHandling = NullValueHandling.Ignore)]
        public string EmployeeBasicDetailsUId { get; set; }

        [JsonProperty(PropertyName = "alternateEmail", NullValueHandling = NullValueHandling.Ignore)]
        public string AlternateEmail { get; set; }

        [JsonProperty(PropertyName = "alternateMobile", NullValueHandling = NullValueHandling.Ignore)]
        public string AlternateMobile { get; set; }

        [JsonProperty(PropertyName = "workInformation", NullValueHandling = NullValueHandling.Ignore)]
        public WorkInfo_? WorkInformation { get; set; }

        [JsonProperty(PropertyName = "personalDetails", NullValueHandling = NullValueHandling.Ignore)]

        public PersonalDetails_? PersonalDetails { get; set; }

        [JsonProperty(PropertyName = "identityInfo", NullValueHandling = NullValueHandling.Ignore)]

        public IdentityInfo_? IdentityInformation { get; set; }

    }
}

