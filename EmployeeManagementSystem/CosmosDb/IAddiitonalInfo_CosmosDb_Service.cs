using EmployeeManagementSystem.DTO;
using EmployeeManagementSystem.Entites;

namespace EmployeeManagementSystem.CosmosDb
{
    public interface IAddiitonalInfo_CosmosDb_Service
    {
        Task<EmployeeAdditonalInfoEntity> AddEmployee(EmployeeAdditonalInfoEntity emp);
        Task<EmployeeAdditonalInfoEntity> Creat(EmployeeAdditonalInfoEntity response);
        Task<EmployeeAdditonalInfoEntity> Delete(string uid);
        Task<List<EmployeeAdditonalInfoEntity>> GetAll();
        Task<EmployeeAdditonalInfoEntity> GetEmployeeByUId(string uid);
        Task<EmployeeAdditonalInfoEntity> Update(AdditionalInfoDTO additionalInfoDto);
    }
}
