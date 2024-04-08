using StaffBase.Core.Models;

namespace StaffBase.Application.Utils
{
    public interface ICsvProvider
    {
        string EmployeeToCsv(List<Employee> employeeList);
        string OrganizationToCsv(List<Organization> organizationsList);
        (List<Employee> employee, string error) CsvToEmployee(string csv);
        (List<Organization>, string error) CsvToOrganization(string csv);

    }
}