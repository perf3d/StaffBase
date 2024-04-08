using StaffInterface.Core.Models;
using StaffInterface.Infrastructure.Contracts;

namespace StaffInterface.Core.Abstractions
{
    public interface IBaseApiAccess
    {
        public (List<Organization>? organizations, string error) GetOrganizations();
        public (List<Employee> employee, string error) GetEmployee();
        public (List<Employee> employee, string error) GetEmployeeByOrganizationId(Guid id);
        public (Guid organizationId, string error) AddOrganization(Organization organization);
        public (Guid employeeId, string error) AddEmployee(Employee employee);
        public string AddOrganizationsCsv(string pathToFile);
        public string AddEmployeeCsv(string pathToFile);
        public string GetOrganizationsCsv(string pathToFile);
        public string GetEmployeeCsv(string pathToFile);
        public string GetEmployeeByOrganizationIdCsv(string pathToFile, Guid organizationId);
    }
}