using EmployeeManagementSystem.Common;
using EmployeeManagementSystem.DTO;
using EmployeeManagementSystem.Entites;
using Microsoft.Azure.Cosmos;

namespace EmployeeManagementSystem.CosmosDb
{
    public class Additonalnfo_CosmosDb_Service :IAddiitonalInfo_CosmosDb_Service
    {
        public CosmosClient _cosmosClient;
        private readonly Container _container;
        public Additonalnfo_CosmosDb_Service()
        {
            _cosmosClient = new CosmosClient(Credentials.CosmosEndPoint, Credentials.PrimaryKey);
            _container = _cosmosClient.GetContainer(Credentials.databaseName, Credentials.containerName);
        }

        public async Task<EmployeeAdditonalInfoEntity> AddEmployee(EmployeeAdditonalInfoEntity emp)
        {
            var response = await _container.CreateItemAsync(emp);
            return emp;
        }

       public async  Task<List<EmployeeAdditonalInfoEntity>> GetAll()
        {
            var response = _container.GetItemLinqQueryable<EmployeeAdditonalInfoEntity>(true).Where(q => q.Active == true && q.Archive == false && q.Documentype == "Additional").ToList();

            return response;
        }

        public async Task<EmployeeAdditonalInfoEntity> GetEmployeeByUId(string uid)
        {
            var response = _container.GetItemLinqQueryable<EmployeeAdditonalInfoEntity>(true).Where(q => q.UId == uid && q.Active == true && q.Archive == false && q.Documentype == "Additional").FirstOrDefault();
            return response;
        }

        public async Task<EmployeeAdditonalInfoEntity> Update(AdditionalInfoDTO additionalInfoDto)
        {
            var response = _container.GetItemLinqQueryable<EmployeeAdditonalInfoEntity>(true).Where(q => q.UId == additionalInfoDto.UId && q.Active == true && q.Archive == false && q.Documentype == "Additional").FirstOrDefault();
            response.Archive = true;
            response.Active = false;
            await _container.ReplaceItemAsync(response, response.Id);

            return response;
        }

        public async Task<EmployeeAdditonalInfoEntity> Creat(EmployeeAdditonalInfoEntity response)
        {
            response = await _container.CreateItemAsync(response);
            return response;
        }

        public async Task<EmployeeAdditonalInfoEntity> Delete(string uid)
        {
            var response = _container.GetItemLinqQueryable<EmployeeAdditonalInfoEntity>(true).Where(q => q.UId == uid && q.Active == true && q.Archive == false && q.Documentype == "Additional").FirstOrDefault();
            response.Archive = true;
            response.Active = false;

            await _container.ReplaceItemAsync(response, response.Id);
            return response;
        }
    }
}
