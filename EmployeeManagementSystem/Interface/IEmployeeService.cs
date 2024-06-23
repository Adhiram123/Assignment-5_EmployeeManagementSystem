using EmployeeManagementSystem.DTO;
using EmployeeManagementSystem.Entites;

namespace EmployeeManagementSystem.Interface
{
    public interface IEmployeeService
    {
        Task<EmployeeBasicDTO> AddBasicEmployeeDetails(EmployeeBasicDTO employeeBasicDTO);
        
        Task<EmployeeBasicDTO> GetBasicEmployeeByUid(string uid);
        Task<EmployeeBasicDTO> Update(EmployeeBasicDTO employeeBasicDto);

        Task<EmployeeBasicDTO> Delete(string uid);
        Task<List<EmployeeBasicDTO>> GetAll();
        Task<List<EmployeeBasicDTO>> GetAllEmployeeByRole(string role);
        Task<EmployeeFilterCriteria> GetAllEmployeeByPaginatiion(EmployeeFilterCriteria employeeFilterCreteria);
        Task<StudentModel> AddStudentByMakePostRequest(StudentModel studentModel);
        Task<List<StudentModel>> GetStudentsByMakeGetRequest();
        Task<StudentFilterCriteria> GetAllStudentsByPaginatiion(StudentFilterCriteria studentFilterCreteria);
    }
}
