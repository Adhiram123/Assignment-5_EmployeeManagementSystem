using EmployeeManagementSystem.DTO;
using EmployeeManagementSystem.Entites;

namespace EmployeeManagementSystem.Interface
{
    public interface IAdditionInfo_Serivice
    {
        Task<AdditionalInfoDTO> AddEmployeeDetails(AdditionalInfoDTO additonalInfoDto);
        Task<AdditionalInfoDTO> Delete(string uid);
        Task<List<AdditionalInfoDTO>> GetAll();
        Task<AdditionalEmployeelDetailsFilterCriteria> GetAllAdditionalEmployeeDetailsByPagination(AdditionalEmployeelDetailsFilterCriteria employeeFilterCreteria);
        Task<AdditionalInfoDTO> GetEmployeeAdditionalDetailsByBasicDetailUid(string basicUid);
        Task<AdditionalInfoDTO> GetEmployeeByUid(string uid);
        Task<AdditionalInfoDTO> Update(AdditionalInfoDTO additionalInfoDto);
    }
}
