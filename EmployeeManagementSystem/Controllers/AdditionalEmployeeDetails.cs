using EmployeeManagementSystem.Common;
using EmployeeManagementSystem.DTO;
using EmployeeManagementSystem.Entites;
using EmployeeManagementSystem.Interface;
using EmployeeManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Drawing;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EmployeeManagementSystem.Controllers
{
    [Route("api/[Controller]/[action]")]
    [ApiController]
    public class AdditionalEmployeeDetails : Controller
    {
        public readonly IAdditionInfo_Serivice _additionalService;
        public  AdditionalEmployeeDetails(IAdditionInfo_Serivice additionalService)
        {

            _additionalService = additionalService;
        }

        [HttpPost]
        public async Task<IActionResult> AddAdditionalEmployeeDetails(AdditionalInfoDTO additonalInfoDto)
        {
            var response = await _additionalService.AddEmployeeDetails(additonalInfoDto);

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
        public async Task<IActionResult> GetEmployeeByUid(string uid)
        {
            var response = await _additionalService.GetEmployeeByUid(uid);

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
        public async Task<IActionResult> GetEmployeeByBasicDetailsUid(string basicUid)
        {
            var response = await _additionalService.GetAllEmployeeByBasicUid(basicUid);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDetailsOfAdditionalEmployeeDetails(AdditionalInfoDTO additionalInfoDto)
        {

            var response = await _additionalService.Update(additionalInfoDto);

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
            var response = await _additionalService.Delete(uid);

            return Ok("delete succserfully");
        }

        [HttpPost]
      
        public async Task<AdditionalEmployeelDetailsFilterCriteria> GetAllAdditionalEmployeeDetailsByPagination(AdditionalEmployeelDetailsFilterCriteria employeeFilterCreteria)
        {
            var response = await _additionalService.GetAllAdditionalEmployeeDetailsByPagination(employeeFilterCreteria);

            return response;
        }

        [HttpGet]
        public async Task<List<AdditionalInfoDTO>> GetAll()
        {

            var response = await _additionalService.GetAll();
            return response;
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

            var employees = new List<AdditionalInfoDTO>();

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
                        string dateString = GetStringFormCell(workSheet, row, 10);
                        DateTime dateOfJoining;
                        if (!DateTime.TryParse(dateString, out dateOfJoining))
                        {
                            // Handle the case where the date is not valid
                            dateOfJoining = DateTime.MinValue; 
                        }
                        string dateString1 = GetStringFormCell(workSheet, row, 11);
                        DateTime dateofBirth;

                        if (!DateTime.TryParse(dateString1, out dateofBirth))
                        {
                            // Handle the case where the date is not valid
                            dateofBirth = DateTime.MinValue; 
                        }

                        var employee = new AdditionalInfoDTO
                        {

                            EmployeeBasicDetailsUId = GetStringFormCell(workSheet, row, 1),
                            AlternateEmail = GetStringFormCell(workSheet, row, 2),
                            AlternateMobile = GetStringFormCell(workSheet, row, 3),
                            WorkInformation = new WorkInfo_
                            {
                                DesignationName = GetStringFormCell(workSheet, row, 4),
                                DepartmentName = GetStringFormCell(workSheet, row, 5),
                                LocationName = GetStringFormCell(workSheet, row, 7),

                                EmployeeStatus = GetStringFormCell(workSheet, row, 8),
                                SourceOfHire = GetStringFormCell(workSheet, row, 9),
                                DateOfJoining = dateOfJoining,
                                // DateOfJoining = GetStringFormCell(workSheet, row,10)
                            },
                            PersonalDetails = new PersonalDetails_
                            {
                               // DateOfBirth = GetStringFormCell(workSheet, row, 11),
                               DateOfBirth= dateofBirth,
                               Age =GetStringFormCell(workSheet,row,12),
                               Gender=GetStringFormCell(workSheet,row,13),
                                Religion=GetStringFormCell(workSheet,row,14),
                                Caste=GetStringFormCell(workSheet,row,15),
                                MartialStatus=GetStringFormCell(workSheet,row,16),
                                BloodGroup=GetStringFormCell(workSheet,row,17),
                                Height=GetStringFormCell(workSheet,row,14),
                                Weight=GetStringFormCell(workSheet,row,18),
                                // Set personal details properties here
                            },
                            IdentityInformation = new IdentityInfo_
                            {
                                // Set identity information properties here
                                PAN=GetStringFormCell(workSheet,row,19),
                                Aadhar=GetStringFormCell(workSheet,row,20),
                                Nationality=GetStringFormCell(workSheet,row,21),
                                PassportNumber=GetStringFormCell(workSheet,row,22),
                                PFNumber=GetStringFormCell(workSheet,row,23)

                            }

                        };

                        await AddAdditionalEmployeeDetails(employee);


                        employees.Add(employee);

                    }
                }
            }
            return Ok(employees);
        }
    }
}
