using EmployeeManagementSystem.Common;
using EmployeeManagementSystem.DTO;
using EmployeeManagementSystem.Entites;
using EmployeeManagementSystem.Interface;
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


        [HttpPost]
        public async Task<IActionResult> Export()
        {
            var employees = await _employeeService.GetAll();
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
        }

        [HttpPost]
        public async Task<IActionResult> GetEmployeeByRole(string role)
        {
            var response = await _employeeService.GetAllEmployeeByRole(role);

            return Ok(response);
        }

        [HttpPost]
        public async Task<EmployeeFilterCriteria> GetAllEmployeesByPagination(EmployeeFilterCriteria employeeFilterCreteria)
        {
            var response = await _employeeService.GetAllEmployeeByPaginatiion(employeeFilterCreteria);

            return response;
        }

    }
}
