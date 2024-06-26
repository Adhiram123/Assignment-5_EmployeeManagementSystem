using AutoMapper;
using EmployeeManagementSystem.Common;
using EmployeeManagementSystem.CosmosDb;
using EmployeeManagementSystem.DTO;
using EmployeeManagementSystem.Entites;
using EmployeeManagementSystem.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EmployeeManagementSystem.Services
{
    public class AdditionalInfo_Service :IAdditionInfo_Serivice
    {
        public readonly IAddiitonalInfo_CosmosDb_Service _additionalInfo_CosmosDb_Service;
        public readonly IMapper _mapper;
        public AdditionalInfo_Service(IAddiitonalInfo_CosmosDb_Service additionalInfo_CosmosDb_Service, IMapper mapper)
        {
            _additionalInfo_CosmosDb_Service = additionalInfo_CosmosDb_Service;
            _mapper = mapper;
        }

        public async Task<AdditionalInfoDTO> AddEmployeeDetails(AdditionalInfoDTO additonalInfoDto)
        {
            var emp = _mapper.Map<EmployeeAdditonalInfoEntity>(additonalInfoDto);

            //Assign Mandatory Values
            BaseEntity.Initializer2(true, "Additional", "adhiram", emp);

           // emp.EmployeeBasicDetailsUId ="hi";

             var reciveData = await _additionalInfo_CosmosDb_Service.GetAll();
            bool ispresent = false;
            foreach(var Uid in reciveData)
            {
                if(Uid.EmployeeBasicDetailsUId == emp.EmployeeBasicDetailsUId)
                {
                    ispresent = true;
                    break;
                }
            }
            if (ispresent == false)
            {

                var response = await _additionalInfo_CosmosDb_Service.AddEmployee(emp);

                var responseModel = _mapper.Map<AdditionalInfoDTO>(response);

                return responseModel;
            }
            else
            {
                return null;
            }
        }

        public async Task<AdditionalInfoDTO> GetEmployeeByUid(string uid)
        {
            var response = await _additionalInfo_CosmosDb_Service.GetEmployeeByUId(uid);
            if (response != null)
            {
                

                var responseModel = _mapper.Map<AdditionalInfoDTO>(response);

                return responseModel;
            }
            else
            {
                return null;
            }

        }

        public async Task<AdditionalInfoDTO> Update(AdditionalInfoDTO additionalInfoDto)
        {
            var response = await _additionalInfo_CosmosDb_Service.Update(additionalInfoDto);

            BaseEntity.Initializer2(false, "basic", "adhiram", response);

            response.UId = additionalInfoDto.UId;
            response.EmployeeBasicDetailsUId = additionalInfoDto.EmployeeBasicDetailsUId;
            response.AlternateEmail = additionalInfoDto.AlternateEmail;
            response.WorkInformation = additionalInfoDto.WorkInformation;
            response.PersonalDetails = additionalInfoDto.PersonalDetails;
            response.IdentityInformation = additionalInfoDto.IdentityInformation;

            response = await _additionalInfo_CosmosDb_Service.Creat(response);

            var response1 = _mapper.Map<AdditionalInfoDTO>(response);


            return response1;

        }
        public async Task<AdditionalInfoDTO> Delete(string uid)
        {
            var response = await _additionalInfo_CosmosDb_Service.Delete(uid);

           
            //Asign the mandatory Values
            response.Archive = true;
            response.Active = false;

            var employeeadditionalDto = _mapper.Map<AdditionalInfoDTO>(response);
            return employeeadditionalDto;
        }

        public async Task<List<AdditionalInfoDTO>> GetAll()
        {
            var response = await _additionalInfo_CosmosDb_Service.GetAll();
            var employeeDto = new List<AdditionalInfoDTO>();
            foreach (var emp in response)
            {
                var empDto = _mapper.Map<AdditionalInfoDTO>(emp);

                employeeDto.Add(empDto);
            }

            return employeeDto;
        }
        public async Task<AdditionalEmployeelDetailsFilterCriteria> GetAllAdditionalEmployeeDetailsByPagination(AdditionalEmployeelDetailsFilterCriteria employeeFilterCreteria)
        {
            AdditionalEmployeelDetailsFilterCriteria responseObject = new AdditionalEmployeelDetailsFilterCriteria();
            var checkFilter = employeeFilterCreteria.Filters.Any(a => a.FieldName == "Age");
            var role = "";
            if (checkFilter)
            {
                role = employeeFilterCreteria.Filters.Find(a => a.FieldName == "Age").FieldValue;
            }
            var employees = await GetAll();

            var filterRecords = employees.FindAll(a => a.PersonalDetails.Age == "25");

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

        public async Task<AdditionalInfoDTO> GetEmployeeAdditionalDetailsByBasicDetailUid(string basicUid)
        {
            //get all the Elements
            var allEmployee = await GetAll();
            //2.filerasper Uid
            return allEmployee.Find(a => a.EmployeeBasicDetailsUId == basicUid);
            //return
        }
    }
}
