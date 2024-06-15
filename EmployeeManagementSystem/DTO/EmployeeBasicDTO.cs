using EmployeeManagementSystem.Common;
using Newtonsoft.Json;

namespace EmployeeManagementSystem.DTO
{
    public class EmployeeBasicDTO
    {
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]

        public string Id { get; set; }
        [JsonProperty(PropertyName = "uid", NullValueHandling = NullValueHandling.Ignore)]
        public string UId { get; set; }

        [JsonProperty(PropertyName = "salutory", NullValueHandling = NullValueHandling.Ignore)]
        public string Salutory { get; set; }

        [JsonProperty(PropertyName = "firstName", NullValueHandling = NullValueHandling.Ignore)]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "middleName", NullValueHandling = NullValueHandling.Ignore)]
        public string MiddleName { get; set; }

        [JsonProperty(PropertyName = "lastName", NullValueHandling = NullValueHandling.Ignore)]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "nickName", NullValueHandling = NullValueHandling.Ignore)]

        public string NickName { get; set; }

        [JsonProperty(PropertyName = "mobile", NullValueHandling = NullValueHandling.Ignore)]
        public string Mobile { get; set; }

        [JsonProperty(PropertyName = "email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "employeeId", NullValueHandling = NullValueHandling.Ignore)]

        public string EmployeeId { get; set; }

        [JsonProperty(PropertyName = "role", NullValueHandling = NullValueHandling.Ignore)]
        public string Role { get; set; }

        [JsonProperty(PropertyName = "reportingMangerUId", NullValueHandling = NullValueHandling.Ignore)]
        public string ReportingMangerUId { get; set; }

        [JsonProperty(PropertyName = "reportingMangerName", NullValueHandling = NullValueHandling.Ignore)]
        public string ReportingMangerName { get; set; }

        [JsonProperty(PropertyName = "address", NullValueHandling = NullValueHandling.Ignore)]
        public Address Address { get; set; }
    }
}
