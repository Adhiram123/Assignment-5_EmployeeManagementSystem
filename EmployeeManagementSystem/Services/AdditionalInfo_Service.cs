using AutoMapper;
using EmployeeManagementSystem.Common;
using EmployeeManagementSystem.CosmosDb;
using EmployeeManagementSystem.DTO;
using EmployeeManagementSystem.Entites;
using EmployeeManagementSystem.Interface;

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

            BaseEntity.Initializer2(true, "Additional", "adhiram", emp);
           
            var reciveData = await _additionalInfo_CosmosDb_Service.GetAll();
            bool ispresent = false;
            foreach(var Uid in reciveData)
            {
                if(Uid.UId==emp.UId)
                {
                    ispresent = true;
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

    }
}
