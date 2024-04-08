using StaffBase.Core.Models;

namespace StaffBase.DataAccess.Repositories
{
    public interface IStaffBaseRepository
    {
        Task AddEmployee(Employee employee);
        Task AddEmployeeList(List<Employee> employee);
        Task AddOrganization(Organization organization);
        Task AddOrganizationList(List<Organization> organization);
        (List<Employee>?, string) GetAllEmployee();
        (List<Organization>?, string) GetAllOrganizations();
        (List<Employee>?, string) GetEmployeeByOrganizationId(Guid organizationId);
    }
}