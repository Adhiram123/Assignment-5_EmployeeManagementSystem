using EmployeeManagementSystem.Common;
using EmployeeManagementSystem.DTO;
using Newtonsoft.Json;
using System.Buffers.Text;

namespace EmployeeManagementSystem.Entites
{
    public class EmployeeBasicEntity :BaseEntity
    {
        [JsonProperty(PropertyName = "salutory", NullValueHandling = NullValueHandling.Ignore)]
        public string Salutory { get; set; }

        [JsonProperty(PropertyName = "firstName", NullValueHandling = NullValueHandling.Ignore)]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "middleName", NullValueHandling = NullValueHandling.Ignore)]
        public string MiddleName { get; set; }

        [JsonProperty(PropertyName = "lastName", NullValueHandling = NullValueHandling.Ignore)]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "nickNmae", NullValueHandling = NullValueHandling.Ignore)]

        public string NickName { get; set; }

        [JsonProperty(PropertyName = "mobile", NullValueHandling = NullValueHandling.Ignore)]
        public string Mobile { get; set; }

        [JsonProperty(PropertyName = "email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "employeeId", NullValueHandling = NullValueHandling.Ignore)]

        public string EmployeeId { get; set; }

      

        [JsonProperty(PropertyName = "reportingMangerUId", NullValueHandling = NullValueHandling.Ignore)]
        public string ReportingMangerUId { get; set; }

        [JsonProperty(PropertyName = "reportingMangerName", NullValueHandling = NullValueHandling.Ignore)]
        public string ReportingMangerName { get; set; }

        [JsonProperty(PropertyName = "address", NullValueHandling = NullValueHandling.Ignore)]
        public Address Address { get; set; }

        [JsonProperty(PropertyName = "role", NullValueHandling = NullValueHandling.Ignore)]
        public string Role { get; set; }



    }

    public class EmployeeFilterCriteria
    {
        public EmployeeFilterCriteria()
        {
            Filters = new List<FilterCriteria>();
            Employees = new List<EmployeeBasicDTO>();
        }
        public int Page { get; set; } //Page Number
        public int PageSize { get; set; }  //Number of records in one Page
        public int TotalCount { get; set; } //Total records present In database
        public List<FilterCriteria> Filters { get; set; } // filter Records

        public List<EmployeeBasicDTO> Employees { get; set; } //response

    }
    public class FilterCriteria
    {
        public string FieldName { get; set; }
        public string FieldValue { get; set; }

    }
}
