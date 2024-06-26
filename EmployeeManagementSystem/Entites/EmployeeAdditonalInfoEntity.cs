using EmployeeManagementSystem.Common;
using EmployeeManagementSystem.DTO;
using Newtonsoft.Json;

namespace EmployeeManagementSystem.Entites
{
    public class EmployeeAdditonalInfoEntity: BaseEntity
    {
        [JsonProperty(PropertyName = "employeeBasicDetailsUid", NullValueHandling = NullValueHandling.Ignore)]
        public string EmployeeBasicDetailsUId { get; set; }

        [JsonProperty(PropertyName = "alternateEmail", NullValueHandling = NullValueHandling.Ignore)]
        public string AlternateEmail { get; set; }

        [JsonProperty(PropertyName = "alternateMobile", NullValueHandling = NullValueHandling.Ignore)]
        public string AlternateMobile { get; set; }

        [JsonProperty(PropertyName = "workInformation", NullValueHandling = NullValueHandling.Ignore)]
        public WorkInfo_ WorkInformation { get; set; }

        [JsonProperty(PropertyName = "personalDetails", NullValueHandling = NullValueHandling.Ignore)]

        public PersonalDetails_ PersonalDetails { get; set; }

        [JsonProperty(PropertyName = "identityInformation", NullValueHandling = NullValueHandling.Ignore)]

        public IdentityInfo_ IdentityInformation { get; set; }

    }

    public class AdditionalEmployeelDetailsFilterCriteria
    {
        public AdditionalEmployeelDetailsFilterCriteria()
        {
            Filters = new List<AdditionalFilterCriteria>();
            Employees = new List<AdditionalInfoDTO>();
        }
        public int Page { get; set; } //Page Number
        public int PageSize { get; set; }  //Number of records in one Page
        public int TotalCount { get; set; } //Total records present In database
        public List<AdditionalFilterCriteria> Filters { get; set; } // filter Records

        public List<AdditionalInfoDTO> Employees { get; set; } //response

    }
    public class AdditionalFilterCriteria
    {
        public string FieldName { get; set; }
        public string FieldValue { get; set; }

    }
}
