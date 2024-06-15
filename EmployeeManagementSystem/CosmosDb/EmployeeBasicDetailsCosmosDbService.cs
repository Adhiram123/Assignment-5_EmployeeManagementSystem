using EmployeeManagementSystem.Common;
using EmployeeManagementSystem.DTO;
using EmployeeManagementSystem.Entites;
using Microsoft.Azure.Cosmos;

namespace EmployeeManagementSystem.CosmosDb
{
    public class EmployeeBasicDetailsCosmosDbService :IEmployeebasicDetailsCosmosDbService
    {
        public CosmosClient _cosmosClient;
        private readonly Container _container;
        public EmployeeBasicDetailsCosmosDbService()
        {
            _cosmosClient = new CosmosClient(Credentials.CosmosEndPoint, Credentials.PrimaryKey);
            _container = _cosmosClient.GetContainer(Credentials.databaseName, Credentials.containerName);
        }

        public async Task<EmployeeBasicEntity> AddEmployee(EmployeeBasicEntity emp)
        {
            var response = await _container.CreateItemAsync(emp);
            return emp;
        }

        public async Task<EmployeeBasicEntity> GetEmployeeByUId(string uid)
        {
            var response = _container.GetItemLinqQueryable<EmployeeBasicEntity>(true).Where(q => q.UId==uid && q.Active == true && q.Archive == false && q.Documentype == "basic").FirstOrDefault();
            return response;
        }

        public async Task<EmployeeBasicEntity> Update(EmployeeBasicDTO employeeBasicDto)
        {
            var response = _container.GetItemLinqQueryable<EmployeeBasicEntity>(true).Where(q => q.UId == employeeBasicDto.UId && q.Active == true && q.Archive == false && q.Documentype == "basic").FirstOrDefault();
            response.Archive = true;
            response.Active = false;
            await _container.ReplaceItemAsync(response, response.Id);

            return response;
        }

        public async Task<EmployeeBasicEntity> Creat(EmployeeBasicEntity response)
        {
            response = await _container.CreateItemAsync(response);
            return response;
        }

        public async Task<EmployeeBasicEntity> Delete(string uid)
        {
            var response = _container.GetItemLinqQueryable<EmployeeBasicEntity>(true).Where(q => q.UId == uid && q.Active == true && q.Archive == false && q.Documentype == "basic").FirstOrDefault();
            response.Archive = true;
            response.Active = false;

            await _container.ReplaceItemAsync(response, response.Id);
            return response;
        }

        public async Task<List<EmployeeBasicEntity>> GetAll()
        {
            var response = _container.GetItemLinqQueryable<EmployeeBasicEntity>(true).Where(q => q.Active == true && q.Archive == false && q.Documentype == "basic").ToList();

            return response;
        }
    }
}
