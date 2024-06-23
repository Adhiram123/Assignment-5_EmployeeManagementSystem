namespace EmployeeManagementSystem.Common
{
    public class Credentials
    {
        public static readonly string databaseName = Environment.GetEnvironmentVariable("dataBaseName");
        public static readonly string containerName = Environment.GetEnvironmentVariable("containerName");
        public static readonly string CosmosEndPoint = Environment.GetEnvironmentVariable("cosmosUrl");
        public static readonly string PrimaryKey = Environment.GetEnvironmentVariable("primaryKey");
        internal static readonly string StudentUrl = Environment.GetEnvironmentVariable("studentUrl");
        internal static string AddStudentEndPoint = "/api/Student/AddStudent";
        internal static string GetStudentEndPoint = "/api/Student/GetAllStudents";
    }
}
