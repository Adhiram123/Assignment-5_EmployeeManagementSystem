using EmployeeManagementSystem.DTO;
using EmployeeManagementSystem.Entites;

namespace EmployeeManagementSystem.CosmosDb
{
    public interface IEmployeebasicDetailsCosmosDbService
    {
        Task<EmployeeBasicEntity> AddEmployee(EmployeeBasicEntity emp);
        Task<EmployeeBasicEntity> GetEmployeeByUId(string uid);
        Task<EmployeeBasicEntity> Update(EmployeeBasicDTO employeeBasicDto);

        Task<EmployeeBasicEntity> Creat(EmployeeBasicEntity response);
        Task<EmployeeBasicEntity> Delete(string uid);
        Task<List<EmployeeBasicEntity>> GetAll();
    }
}
