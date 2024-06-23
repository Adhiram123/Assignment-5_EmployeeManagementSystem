using AutoMapper;
using EmployeeManagementSystem.Common;
using EmployeeManagementSystem.CosmosDb;
using EmployeeManagementSystem.DTO;
using EmployeeManagementSystem.Entites;
using EmployeeManagementSystem.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EmployeeManagementSystem.Services
{
    public class EmployeeBasicDetailsService : IEmployeeService
    {
        public readonly IEmployeebasicDetailsCosmosDbService _empBasicCosmos;
        public readonly IMapper _mapper;
        public EmployeeBasicDetailsService(IEmployeebasicDetailsCosmosDbService employeebasicDetails, IMapper mapper)
        {
            _empBasicCosmos = employeebasicDetails;
            _mapper = mapper;

        }

        public async Task<EmployeeBasicDTO> AddBasicEmployeeDetails(EmployeeBasicDTO employeeBasicDTO)
        {
            var emp = _mapper.Map<EmployeeBasicEntity>(employeeBasicDTO);

            BaseEntity.Initializer1(true, "basic", "adhiram", emp);

           
            var response = await _empBasicCosmos.AddEmployee(emp);

            var responseModel = _mapper.Map<EmployeeBasicDTO>(response);

            return responseModel;

        }

       

        public async Task<EmployeeBasicDTO> GetBasicEmployeeByUid(string uid)
        {
            var response = await _empBasicCosmos.GetEmployeeByUId(uid);
            if (response != null)
            {
              
                
                var responseModel = _mapper.Map<EmployeeBasicDTO>(response);

                return responseModel;
            }
            else
            {
                return null;
            }
        }

        public async Task<EmployeeBasicDTO> Update(EmployeeBasicDTO employeeBasicDto)
        {
            var response = await _empBasicCosmos.Update(employeeBasicDto);

            BaseEntity.Initializer1(false, "basic", "adhiram", response);

            response.UId = employeeBasicDto.UId;
            response.Salutory = employeeBasicDto.Salutory;
            response.FirstName = employeeBasicDto.FirstName;
            response.MiddleName = employeeBasicDto.MiddleName;
            response.LastName = employeeBasicDto.LastName;
            response.Email = employeeBasicDto.Email;
            response.Mobile = employeeBasicDto.Mobile;
            response.EmployeeId = employeeBasicDto.EmployeeId;
            response.Role = employeeBasicDto.Role;
            response.ReportingMangerName = employeeBasicDto.ReportingMangerName;
            response.ReportingMangerUId = employeeBasicDto.ReportingMangerUId;
            response.Address = employeeBasicDto.Address;
           
          //  response = _mapper.Map<EmployeeBasicEntity>(employeeBasicDto);

           
            response = await _empBasicCosmos.Creat(response);

            var response1 = _mapper.Map < EmployeeBasicDTO>(response);


            return response1;
           
        }
        public async Task<EmployeeBasicDTO> Delete(string uid)
        {
            var response = await _empBasicCosmos.Delete(uid);

          

            //Asign the mandatory Values
            response.Archive = true;
            response.Active = false;

            var employeeBasicDto = _mapper.Map<EmployeeBasicDTO>(response);
            return employeeBasicDto;


        }

        public async Task<List<EmployeeBasicDTO>> GetAll()
        {
            var response = await _empBasicCosmos.GetAll();
            var employeeDto = new List<EmployeeBasicDTO>();
            foreach(var emp in response)
            {
                var empDto = _mapper.Map<EmployeeBasicDTO>(emp);

                employeeDto.Add(empDto);
            }

            return employeeDto;
        }

        public async Task<List<EmployeeBasicDTO>> GetAllEmployeeByRole(string role)
        {
            //get all the Elements
            var allStudents = await GetAll();
            //2.filerasper role
            return allStudents.FindAll(a => a.Role == role);
            //return
        }

        /*  public async Task<EmployeeFilterCriteria> GetAllEmployeeByPaginatiion(EmployeeFilterCriteria employeeFilterCreteria)
          {
              EmployeeFilterCriteria responseObject = new EmployeeFilterCriteria();
              var checkFilter = employeeFilterCreteria.Filters.Any(a => a.FieldName == "role");
              var role = "";
              if(checkFilter)
              {
                  role = employeeFilterCreteria.Filters.Find(a => a.FieldName == "role").FieldValue;
              }
              var employees = await GetAll();

              var filterRecords = employees.FindAll(a => a.Role == role);

              responseObject.TotalCount = employees.Count;
              responseObject.Page = employeeFilterCreteria.Page;
              responseObject.PageSize = employeeFilterCreteria.PageSize;

              var skip = employeeFilterCreteria.PageSize * (employeeFilterCreteria.Page - 1);
              filterRecords = filterRecords.Skip(skip).Take(employeeFilterCreteria.PageSize).ToList();

              foreach (var item in filterRecords)
              {
                  responseObject.Employees.Add(item);
              }
              return responseObject;

          }*/
        public async Task<EmployeeFilterCriteria> GetAllEmployeeByPaginatiion(EmployeeFilterCriteria employeeFilterCreteria)
        {
            EmployeeFilterCriteria responseObject = new EmployeeFilterCriteria();
            var checkFilter = employeeFilterCreteria.Filters.Any(a => a.FieldName == "status");
            var status = "";
            if (checkFilter)
            {
                status = employeeFilterCreteria.Filters.Find(a => a.FieldName == "status").FieldValue;
            }
            var employees = await GetAll();

            //    var filterRecords = employees.FindAll(a => a.Role == status);
            var filterRecords = employees.FindAll(a => a.Status == status);

            responseObject.TotalCount = employees.Count;
            responseObject.Page = employeeFilterCreteria.Page;
            responseObject.PageSize = employeeFilterCreteria.PageSize;

            var skip = employeeFilterCreteria.PageSize * (employeeFilterCreteria.Page - 1);
            filterRecords = filterRecords.Skip(skip).Take(employeeFilterCreteria.PageSize).ToList();

            foreach (var item in filterRecords)
            {
                responseObject.Employees.Add(item);
            }
            return responseObject;

        }

        public async Task<StudentModel> AddStudentByMakePostRequest(StudentModel studentModel)
        {
            //Call AddStudent Api by MakePostRequest
            var serializeObj = JsonConvert.SerializeObject(studentModel);
            var requestObj = await HttpClientHelper.MakePostRequest(Credentials.StudentUrl, Credentials.AddStudentEndPoint, serializeObj);
            var responseObj = JsonConvert.DeserializeObject<StudentModel>(requestObj);

            return responseObj;
        }

        public async Task<List<StudentModel>> GetStudentsByMakeGetRequest()
        {
            StudentModel studentDto = new StudentModel();
            var serializeObj = JsonConvert.SerializeObject(studentDto);
            var requestObj = await HttpClientHelper.MakeGetRequest(Credentials.StudentUrl,Credentials.GetStudentEndPoint);
            var responseObj = JsonConvert.DeserializeObject<List<StudentModel>>(requestObj);

            return responseObj;
        }

        public async Task<StudentFilterCriteria> GetAllStudentsByPaginatiion(StudentFilterCriteria studentFilterCreteria)
        {
            StudentFilterCriteria responseObject = new StudentFilterCriteria();

            var checkFilter = studentFilterCreteria.Filters.Any(a => a.FieldName == "status");

            var status = "";
            if (checkFilter)
            {
                status = studentFilterCreteria.Filters.Find(a => a.FieldName == "status").FieldValue;

            }
            var students = await GetStudentsByMakeGetRequest();

            var filterRecords = students.FindAll(a => a.Status == status);

            responseObject.TotalCount = students.Count;


            responseObject.Page = studentFilterCreteria.Page;
            responseObject.PageSize = studentFilterCreteria.PageSize;

            var skip = studentFilterCreteria.PageSize * (studentFilterCreteria.Page - 1);

            // students = students.Skip(skip).Take(studentFilterCreteria.PageSize).ToList();
            filterRecords = filterRecords.Skip(skip).Take(studentFilterCreteria.PageSize).ToList();
            /*  foreach (var item in students)
           //   {
                  responseObject.Students.Add(item);
            //  }
              return responseObject;*/

            foreach (var item in filterRecords)
            {
                responseObject.Students.Add(item);
            }
            return responseObject;
        }
    }
}
