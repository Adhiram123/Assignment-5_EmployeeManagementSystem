using EmployeeManagementSystem.DTO;

namespace EmployeeManagementSystem.Interface
{
    public interface IAdditionInfo_Serivice
    {
        Task<AdditionalInfoDTO> AddEmployeeDetails(AdditionalInfoDTO additonalInfoDto);
        Task<AdditionalInfoDTO> Delete(string uid);
        Task<AdditionalInfoDTO> GetEmployeeByUid(string uid);
        Task<AdditionalInfoDTO> Update(AdditionalInfoDTO additionalInfoDto);
    }
}
