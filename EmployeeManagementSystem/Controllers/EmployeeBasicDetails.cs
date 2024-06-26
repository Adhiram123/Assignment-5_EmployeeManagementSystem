using EmployeeManagementSystem.Common;
using EmployeeManagementSystem.DTO;
using EmployeeManagementSystem.Entites;
using EmployeeManagementSystem.Interface;
using EmployeeManagementSystem.ServiceFilter;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace EmployeeManagementSystem.Controllers
{
    [Route("api/[Controller]/[action]")]
    [ApiController]
    public class EmployeeBasicDetails : Controller
    {
        public readonly IEmployeeService _employeeService;
       
        public EmployeeBasicDetails(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
            
    }

        [HttpPost]
        public async Task<IActionResult> AddBasicEmployeeDetails(EmployeeBasicDTO employeeBasicDTO)
        {
            var response = await _employeeService.AddBasicEmployeeDetails(employeeBasicDTO);

            if (response != null)
            {
                return Ok(response);
            }
            else
            {
                return Ok("It is Null ");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetBasicEmployeeByUid(String uid)
        {
            var response = await _employeeService.GetBasicEmployeeByUid(uid);

            if (response != null)
            {
                return Ok(response);
            }
            else
            {
                return Ok("Enterd Id is Invalid");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDetailsOfEmployee(EmployeeBasicDTO employeeBasicDto)
        {

            var response = await _employeeService.Update(employeeBasicDto);

            if (response != null)
            {
                return Ok(response);
            }
            else
            {
                return Ok("Enterd Id is Invalid");
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteEmployee(string uid)
        {
            var response = await _employeeService.Delete(uid);

            return Ok("delete succserfully");
        }

       private string GetStringFormCell(ExcelWorksheet workSheet, int row, int column)
        {
            var cellValue = workSheet.Cells[row, column].Value;
            return cellValue?.ToString()?.Trim();
        }

        [HttpPost]
        public async Task<IActionResult> ImportExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("file is empty");

            var employees = new List<EmployeeBasicDTO>();

        //    var additonal = new List<AdditionalInfoDTO>();

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                using (var pacakage = new ExcelPackage(stream))
                {
                   // AdditionalInfoDTO dto = new AdditionalInfoDTO();
                   
                    var workSheet = pacakage.Workbook.Worksheets[0];
                    var rowCount = workSheet.Dimension.Rows;
                    for (int row = 2; row <= rowCount; row++)
                    {

                        var employee = new EmployeeBasicDTO
                        {

                            FirstName = GetStringFormCell(workSheet, row, 1),
                            LastName = GetStringFormCell(workSheet, row, 2),
                            Email = GetStringFormCell(workSheet, row, 3),
                            Mobile =GetStringFormCell(workSheet,row,4),
                            ReportingMangerName = GetStringFormCell(workSheet,row,5)

                        };

                        await AddBasicEmployeeDetails(employee);
                     

                        employees.Add(employee);
                       
                    }
                }
            }
            return Ok(employees);
        }

       /* [HttpPost]
        public async Task<IActionResult> ImportBothExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("file is empty");

            var employees = new List<EmployeeBasicDTO>();
            var additionalInfos = new List<AdditionalInfoDTO>();

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                using (var package = new ExcelPackage(stream))
                {
                    var workSheet = package.Workbook.Worksheets[0];
                    var rowCount = workSheet.Dimension.Rows;
                    for (int row = 2; row <= rowCount; row++)
                    {
                        var employee = new EmployeeBasicDTO
                        {
                            FirstName = GetStringFormCell(workSheet, row, 1),
                            LastName = GetStringFormCell(workSheet, row, 2),
                            Email = GetStringFormCell(workSheet, row, 3),
                            Mobile = GetStringFormCell(workSheet, row, 4),
                            ReportingMangerName = GetStringFormCell(workSheet, row, 5)
                        };
                        WorkInfo_ t = new WorkInfo_();
                        var additionalInfo = new AdditionalInfoDTO
                        {
                            

                            EmployeeBasicDetailsUId = GetStringFormCell(workSheet, row, 6),
                            AlternateEmail = GetStringFormCell(workSheet, row, 7),
                            AlternateMobile = GetStringFormCell(workSheet,row,8),
                            DesignationName = GetStringFormCell(workSheet, row, 9)
                            // Add more fields as necessary
                        };

                        await AddBasicEmployeeDetails(employee);
                        // Assuming there's a method to add additional info
                        await AddAdditionalEmployeeByMakePostRequest(additionalInfo);

                        employees.Add(employee);
                        additionalInfos.Add(additionalInfo);
                    }
                }
            }
            return Ok(new { employees, additionalInfos });
        }*/
        /* [HttpPost]
         public async Task<IActionResult> Export()
         {
             var employees = await _employeeService.GetAll();
             //var additionalEmployeeDetails = await _employeeService.GetAdditionalEmployeeDetailsByMakeGetRequest();

             ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

             using (var package = new ExcelPackage())
             {
                 var worksheet = package.Workbook.Worksheets.Add("employees");

                 // Add header
                 worksheet.Cells[1, 1].Value = "UId";
                 worksheet.Cells[1, 2].Value = "FirstName";
                 worksheet.Cells[1, 3].Value = "LastName";
                 worksheet.Cells[1, 4].Value = "Email";
                 worksheet.Cells[1, 5].Value = "PhoneNumber";
                 worksheet.Cells[1, 6].Value = "ReportingManager";


                 // Set header style
                 using (var range = worksheet.Cells[1, 1, 1, 5])
                 {
                     range.Style.Font.Bold = true;
                     range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                     range.Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                 }

                 // Add student data
                 for (int i = 0; i < employees.Count; i++)
                 {
                     var emp = employees[i];
                     worksheet.Cells[i + 2, 1].Value = emp.UId;
                     worksheet.Cells[i + 2, 2].Value = emp.FirstName;
                     worksheet.Cells[i + 2, 3].Value = emp.LastName;
                     worksheet.Cells[i + 2, 4].Value = emp.Email;
                     worksheet.Cells[i + 2, 5].Value = emp .Mobile;
                     worksheet.Cells[i + 2, 6].Value = emp.ReportingMangerName;
                 }

                 var stream = new System.IO.MemoryStream();
                 package.SaveAs(stream);
                 stream.Position = 0;

                 var fileName = "Employees.xlsx";
                 return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml", fileName);
             }
         }*/


        [HttpPost]
        public async Task<IActionResult> Export()
        {
            var employees = await _employeeService.GetAll();
           
            var additionalEmployeeDetails = await _employeeService.GetAdditionalEmployeeDetailsByMakeGetRequest();

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("employees");
              //  var worksheet1 = package.Workbook.Worksheets.Add("additionalEmployeeDetails");

                // Add header
                worksheet.Cells[1, 1].Value = "UId";
                worksheet.Cells[1, 2].Value = "FirstName";
                worksheet.Cells[1, 3].Value = "LastName";
                worksheet.Cells[1, 4].Value = "Email";
                worksheet.Cells[1, 5].Value = "PhoneNumber";
                worksheet.Cells[1, 6].Value = "ReportingManager";
                worksheet.Cells[1, 7].Value = "EmployeeBasicDetailsUId";
                worksheet.Cells[1, 8].Value = "AlternateEmail";
                worksheet.Cells[1, 9].Value = "AlternateMobile";
                worksheet.Cells[1, 10].Value = "DesignationName";
                worksheet.Cells[1, 11].Value = "DepartmentName";
                worksheet.Cells[1, 12].Value = "LocationName";
                worksheet.Cells[1, 13].Value = "EmployeeStatus";
                worksheet.Cells[1, 14].Value = "SourceOfHire";
                worksheet.Cells[1, 15].Value = "DateOfJoining";
                worksheet.Cells[1, 16].Value = " DateOfBirth";
                worksheet.Cells[1, 17].Value = "Age";
                worksheet.Cells[1, 18].Value = "Gender";
                worksheet.Cells[1, 19].Value = "Religion";
                worksheet.Cells[1, 20].Value = "Caste";
                worksheet.Cells[1, 21].Value = "MartialStatus";
                worksheet.Cells[1, 22].Value = "BloodGroup";
                worksheet.Cells[1, 23].Value = "Height";
                worksheet.Cells[1, 24].Value = "weight";
                worksheet.Cells[1, 25].Value = "PAN";
                worksheet.Cells[1, 26].Value = "Aadhar";
                worksheet.Cells[1, 27].Value = "Nationality";
                worksheet.Cells[1, 28].Value = "PassportNumber";
                worksheet.Cells[1, 29].Value = "PFNumber";

                // Set header style
                using (var range = worksheet.Cells[1, 1, 1, 29])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                }

                // Add student data
                for (int i = 0; i < employees.Count; i++)
                {
                    var emp = employees[i];
                    worksheet.Cells[i + 2, 1].Value = emp.UId;
                    worksheet.Cells[i + 2, 2].Value = emp.FirstName;
                    worksheet.Cells[i + 2, 3].Value = emp.LastName;
                    worksheet.Cells[i + 2, 4].Value = emp.Email;
                    worksheet.Cells[i + 2, 5].Value = emp.Mobile;
                    worksheet.Cells[i + 2, 6].Value = emp.ReportingMangerName;

                    for (int i1 = 0; i1 < additionalEmployeeDetails.Count; i1++)
                    {
                        if (emp.UId.Equals(additionalEmployeeDetails[i1].EmployeeBasicDetailsUId))
                        {
                            var emp1 = additionalEmployeeDetails[i1];
                            worksheet.Cells[i+2, 7].Value = emp1.EmployeeBasicDetailsUId;
                            worksheet.Cells[i+2, 8].Value = emp1.AlternateEmail;
                            worksheet.Cells[i+2, 9].Value = emp1.AlternateMobile;
                            worksheet.Cells[i + 2, 10].Value = emp1.WorkInformation.DesignationName;
                            worksheet.Cells[i + 2, 11].Value = emp1.WorkInformation.DepartmentName;
                            worksheet.Cells[i + 2, 12].Value = emp1.WorkInformation.LocationName;
                            worksheet.Cells[i + 2, 13].Value = emp1.WorkInformation.EmployeeStatus;
                            worksheet.Cells[i + 2, 14].Value = emp1.WorkInformation.SourceOfHire;
                            worksheet.Cells[i + 2, 15].Value = emp1.WorkInformation.DateOfJoining;
                            worksheet.Cells[i + 2, 16].Value = emp1.PersonalDetails.DateOfBirth;
                            worksheet.Cells[i + 2, 17].Value = emp1.PersonalDetails.Age;
                            worksheet.Cells[i + 2, 18].Value = emp1.PersonalDetails.Gender;
                            worksheet.Cells[i + 2, 19].Value = emp1.PersonalDetails.Religion;
                            worksheet.Cells[i + 2, 20].Value = emp1.PersonalDetails.Caste;
                            worksheet.Cells[i + 2, 21].Value = emp1.PersonalDetails.MartialStatus;
                            worksheet.Cells[i + 2, 22].Value = emp1.PersonalDetails.BloodGroup;
                            worksheet.Cells[i + 2, 23].Value = emp1.PersonalDetails.Height;
                            worksheet.Cells[i + 2, 24].Value = emp1.PersonalDetails.Weight;
                            worksheet.Cells[i + 2, 25].Value = emp1.IdentityInformation.PAN;
                            worksheet.Cells[i + 2, 26].Value = emp1.IdentityInformation.Aadhar;
                            worksheet.Cells[i + 2, 27].Value = emp1.IdentityInformation.Nationality;
                            worksheet.Cells[i + 2, 28].Value = emp1.IdentityInformation.PassportNumber;
                            worksheet.Cells[i + 2, 29].Value = emp1.IdentityInformation.PFNumber;

                        }

                    }
                }

                var stream = new System.IO.MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                var fileName = "Employees.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml", fileName);
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetEmployeeByRole(string role)
        {
            var response = await _employeeService.GetAllEmployeeByRole(role);

            return Ok(response);
        }

        [HttpPost]
        //[ServiceFilter(typeof(BuildEmployeeFilter))]
        public async Task<EmployeeFilterCriteria> GetAllEmployeesByPagination(EmployeeFilterCriteria employeeFilterCreteria)
        {
            var response = await _employeeService.GetAllEmployeeByPaginatiion(employeeFilterCreteria);

            return response;
        }

        [HttpPost]
        public async Task<IActionResult> AddAdditionalEmployeeByMakePostRequest(AdditionalInfoDTO additionalInfoDTO)
        {
            var response = await _employeeService.AddAdditionalByMakePostRequest(additionalInfoDTO);

            return Ok(response);

        }


        [HttpGet]
        public async Task<List<AdditionalInfoDTO>> GetAdditionalEmployeeDetailsByMakeGetRequest()
        {
            var response = await _employeeService.GetAdditionalEmployeeDetailsByMakeGetRequest();

            return response;
        }

        [HttpPost]
        public async Task<IActionResult> AddStudentByMakePostRequest(StudentModel studentModel)
        {
            var response = await _employeeService.AddStudentByMakePostRequest(studentModel);

            return Ok(response);
        
        }

        [HttpGet]
        public async Task<List<StudentModel>> GetStudentByMakeGetRequest()
        {
            var response = await _employeeService.GetStudentsByMakeGetRequest();

            return response;
        }

        [HttpPost]
        [ServiceFilter(typeof(BuildStudentFilter))]
        public async Task<StudentFilterCriteria> GetAllStudentsByPagination(StudentFilterCriteria studentFilterCreteria)
        {
            var response = await _employeeService.GetAllStudentsByPaginatiion(studentFilterCreteria);
            return response;
        }

        [HttpPost]
        [ServiceFilter(typeof(BuildEmployeeFilter))]
        public async Task<EmployeeFilterCriteria> GetAllEmployeesByServiceFilter(EmployeeFilterCriteria employeeFilterCreteria)
        {
            var response = await _employeeService.GetAllEmployeeByServiceFilter(employeeFilterCreteria);

            return response;
        }


    }
}
