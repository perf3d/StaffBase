using StaffInterface.Core.Models;
using System.Collections.ObjectModel;

namespace StaffInterface.Core.Abstractions
{
    public interface IAppService
    {
        string UpdateEmloyeeCollection(ObservableCollection<Employee> EmployeeGridCollection, IBaseApiAccess _apiAccess, Guid? organizationId = null);
        string UpdateOrganizationsCollection(ObservableCollection<Organization> OrganizationGridCollection, IBaseApiAccess _apiAccess, ObservableCollection<string>? OrganizationBoxCollection = null);
        (Guid id, string error) AddEmployeeToBase(Employee employee, IBaseApiAccess _apiAccess);
        string AddEmployeeToBaseFromFile(IBaseApiAccess _apiAccess);
        string SaveEmployeeToFile(IBaseApiAccess _apiAccess, Guid? organizationId = null);
        (Guid id, string error) AddOrganizationToBase(Organization organization, IBaseApiAccess _apiAccess);
        string AddOrganizationToBaseFromFile(IBaseApiAccess _apiAccess);
        string SaveOrganizationsToFile(IBaseApiAccess _apiAccess);
    }
}