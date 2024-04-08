using StaffBase.Application.Utils;
using StaffBase.Core.Models;
using StaffBase.DataAccess.Repositories;
using System.Data;
namespace StaffBase.Application.Services
{
    public class StaffBaseService : IStaffBaseService
    {
        private readonly IStaffBaseRepository _repository;
        private readonly ICsvProvider _csvProvider;
        public StaffBaseService(IStaffBaseRepository repository, ICsvProvider csvProvider)
        {
            _repository = repository;
            _csvProvider = csvProvider;
        }

        public (string csv, string error) GetAllOrganizationsCsv()
        {
            string csv = string.Empty;
            string error = string.Empty;
            List<Organization>? organizationsList = new();
            (organizationsList, error) = _repository.GetAllOrganizations();
            if (!string.IsNullOrEmpty(error))
            {
                return (csv, error);
            }
            csv = _csvProvider.OrganizationToCsv(organizationsList);
            return (csv, error);
        }

        public (string csv, string error) GetEmployeeByOrganizationIdCsv(Guid organizationId)
        {
            string csv = string.Empty;
            string error = string.Empty;
            List<Employee>? employeeList = new();
            (employeeList, error) = _repository.GetEmployeeByOrganizationId(organizationId);
            if (!string.IsNullOrEmpty(error))
            {
                return (csv, error);
            }
            csv = _csvProvider.EmployeeToCsv(employeeList);
            return (csv, error);
        }

        public (string csv, string error) GetAllEmployeeCsv()
        {
            string csv = string.Empty;
            string error = string.Empty;
            List<Employee>? employeeList = new();
            (employeeList, error) = _repository.GetAllEmployee();
            if (!string.IsNullOrEmpty(error))
            {
                return (csv, error);
            }
            csv = _csvProvider.EmployeeToCsv(employeeList);
            return (csv, error);
        }

        public (List<Organization>? organizations, string error) GetAllOrganizations()
        {
            return _repository.GetAllOrganizations();
        }

        public (List<Employee>? employee, string error) GetEmployeeByOrganizationId(Guid organizationId)
        {
            return _repository.GetEmployeeByOrganizationId(organizationId);
        }

        public (List<Employee>? employee, string error) GetAllEmployee()
        {
            return _repository.GetAllEmployee();
        }

        public async Task AddOrganization(Organization organization)
        {
            await _repository.AddOrganization(organization);
        }

        public async Task AddEmployee(Employee employee)
        {
            await _repository.AddEmployee(employee);
        }

        public async Task<string> AddOrganizationsFromCsv(string csv)
        {
            string error = string.Empty;
            (List<Organization> organizationList, error) = _csvProvider.CsvToOrganization(csv);
            if (!string.IsNullOrEmpty(error))
            {
                return error;
            }
            await _repository.AddOrganizationList(organizationList);
            return error;
        }

        public async Task<string> AddEmployeeFromCsv(string csv)
        {
            string error = string.Empty;
            (List<Employee> employeeList, error) = _csvProvider.CsvToEmployee(csv);
            if (!string.IsNullOrEmpty(error))
            {
                return error;
            }
            await _repository.AddEmployeeList(employeeList);
            return error;
        }
    }
}
