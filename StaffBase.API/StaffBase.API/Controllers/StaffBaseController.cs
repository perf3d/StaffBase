using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StaffBase.API.Contracts;
using StaffBase.Application.Services;
using StaffBase.Core.Models;
using StaffBase.DataAccess.Repositories;
using System.Net;
using System.Text;

namespace StaffBase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffBaseController : ControllerBase
    {
        protected readonly IStaffBaseService _staffBaseService;

        public StaffBaseController(IStaffBaseService staffBaseService)
        {
            _staffBaseService = staffBaseService;
        }

        [HttpPost]
        [Route("EmployeeCsv")]
        public async Task<IActionResult> UploadEmployeeCsv(IFormFile file)
        {
            string csv = string.Empty;
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("No file uploaded.");
                }
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    csv = await reader.ReadToEndAsync();
                }
                string error = await _staffBaseService.AddEmployeeFromCsv(csv);
                if (!string.IsNullOrEmpty(error))
                {
                    return BadRequest(error);
                }
                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }

        [HttpPost]
        [Route("OrganizationsCsv")]
        public async Task<IActionResult> UploadOrganizationsCsv(IFormFile file)
        {
            string csv = string.Empty;
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("No file uploaded.");
                }
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    csv = await reader.ReadToEndAsync();
                }
                string error = await _staffBaseService.AddOrganizationsFromCsv(csv);
                if (!string.IsNullOrEmpty(error))
                {
                    return BadRequest(error);
                }
                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: " + ex.Message);
            }
        }

        [HttpGet("Organizations")]
        public ActionResult<List<OrganizationResponse>> GetOrganizations()
        {
            (List<Organization>? organizationEntities, string error) = _staffBaseService.GetAllOrganizations();
            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }
            List<OrganizationResponse> organizations = organizationEntities.Select(e => new OrganizationResponse(e.Id, e.Name, e.Inn, e.LegalAddress, e.ActualAddress)).ToList();
            return Ok(organizations);
        }

        [HttpGet("Organizations.csv")]
        public async Task<IActionResult> GetDataCsv()
        {
            (string csvFile, string error) = _staffBaseService.GetAllOrganizationsCsv();
            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }
            try
            {
                var result = new FileContentResult(Encoding.UTF8.GetBytes(csvFile), "text/csv")
                {
                    FileDownloadName = "Organizations.csv"
                };
                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("Employee.csv")]
        public async Task<IActionResult> GetEmployeeCsv()
        {
            (string csvFile, string error) = _staffBaseService.GetAllEmployeeCsv();
            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }
            try
            {
                var result = new FileContentResult(Encoding.UTF8.GetBytes(csvFile), "text/csv")
                {
                    FileDownloadName = "Employee.csv"
                };
                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("Employee.csv/{organizationId:guid}")]
        public async Task<IActionResult> GetEmployeeByOrganizationCsv(Guid organizationId)
        {
            (string csvFile, string error) = _staffBaseService.GetEmployeeByOrganizationIdCsv(organizationId);
            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }
            try
            {
                var result = new FileContentResult(Encoding.UTF8.GetBytes(csvFile), "text/csv")
                {
                    FileDownloadName = "Employee.csv"
                };
                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("Employee")]
        public ActionResult<List<Employee>> GetEmployee()
        {
            (List<Employee>? employeeEntities, string error) = _staffBaseService.GetAllEmployee();
            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }
            List<EmployeeResponse> employee = employeeEntities.Select(e =>
                    new EmployeeResponse(
                        e.Id,
                        e.Lastname,
                        e.Firstname,
                        e.Patronomic,
                        e.Birthdate,
                        e.PassportSeries,
                        e.PassportNumber,
                        e.OrganizationId)
                ).ToList();
            return Ok(employee);
        }

        [HttpGet("Employee/{organizationId:guid}")]
        public ActionResult<List<Employee>> GetEmployee(Guid organizationId)
        {
            (List<Employee>? employeeEntities, string error) = _staffBaseService.GetEmployeeByOrganizationId(organizationId);
            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }
            List<EmployeeResponse> employee = employeeEntities.Select(e =>
                    new EmployeeResponse(
                        e.Id,
                        e.Lastname,
                        e.Firstname,
                        e.Patronomic,
                        e.Birthdate,
                        e.PassportSeries,
                        e.PassportNumber,
                        e.OrganizationId)
                ).ToList();
            return Ok(employee);
        }

        [Route("Employee")]
        [HttpPost]
        public async Task<ActionResult<Guid>> AddEmployee([FromBody] EmployeeRequest request)
        {
            (Employee employee, string error) = Employee.Create(
                Guid.NewGuid(),
                request.lastname,
                request.firstname,
                request.patronomic,
                request.birthdate,
                request.passportSeries,
                request.passportNumber,
                request.organizationId
            );
            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }
            await _staffBaseService.AddEmployee(employee);
            return Ok(employee.Id);
        }

        [Route("Organizations")]
        [HttpPost]
        public async Task<ActionResult<Guid>> AddOrganization([FromBody] OrganizationRequest request)
        {
            (Organization organization, string error) = Organization.Create(
                Guid.NewGuid(),
                request.name,
                request.inn,
                request.legalAddress,
                request.actualAddress
            );
            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }
            await _staffBaseService.AddOrganization(organization);
            return Ok(organization.Id);
        }
    }
}
