using Newtonsoft.Json;

namespace EmployeeManagementSystem.Common
{
    public class WorkInfo_
    {
        [JsonProperty(PropertyName = "designatinName", NullValueHandling = NullValueHandling.Ignore)]
        public string DesignationName { get; set; }

        [JsonProperty(PropertyName = "departmentName", NullValueHandling = NullValueHandling.Ignore)]
        public string DepartmentName { get; set; }

        [JsonProperty(PropertyName = "locatinName", NullValueHandling = NullValueHandling.Ignore)]

        public string LocationName { get; set; }

        [JsonProperty(PropertyName = "employeeStatus", NullValueHandling = NullValueHandling.Ignore)]
        public string EmployeeStatus { get; set; }

        [JsonProperty(PropertyName = "sourceOfHire", NullValueHandling = NullValueHandling.Ignore)]
        public string SourceOfHire { get; set; }

        [JsonProperty(PropertyName = "dateOfJoining", NullValueHandling = NullValueHandling.Ignore)]
        
        public DateTime DateOfJoining { get; set; }
    }
}
