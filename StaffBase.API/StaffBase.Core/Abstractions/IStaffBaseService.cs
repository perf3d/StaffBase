using StaffBase.Core.Models;

namespace StaffBase.Application.Services
{
    public interface IStaffBaseService
    {
        Task AddEmployee(Employee employee);
        Task<string> AddEmployeeFromCsv(string csv);
        Task AddOrganization(Organization organization);
        Task<string> AddOrganizationsFromCsv(string csv);
        (List<Employee>? employee, string error) GetAllEmployee();
        (string csv, string error) GetAllEmployeeCsv();
        (List<Organization>? organizations, string error) GetAllOrganizations();
        (string csv, string error) GetAllOrganizationsCsv();
        (List<Employee>? employee, string error) GetEmployeeByOrganizationId(Guid organizationId);
        (string csv, string error) GetEmployeeByOrganizationIdCsv(Guid organizationId);
    }
}