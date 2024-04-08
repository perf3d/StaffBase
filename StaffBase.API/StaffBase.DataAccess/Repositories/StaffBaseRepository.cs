using StaffBase.Core.Models;
using StaffBase.DataAccess.Entities;

namespace StaffBase.DataAccess.Repositories
{
    public class StaffBaseRepository : IStaffBaseRepository
    {
        private readonly StaffBaseDbContext _context;
        public StaffBaseRepository(StaffBaseDbContext context)
        {
            _context = context;
        }

        public (List<Organization>?, string) GetAllOrganizations()
        {
            string error = string.Empty;
            List<Organization>? organizationEntities = _context.Organizations.Select(e =>
                    Organization.Create(e.Id, e.Name, e.Inn, e.LegalAddress, e.ActualAddress).organization
                ).ToList();
            if (organizationEntities.Count==0)
            {
                error = "There are no organization!";
            }
            return (organizationEntities, error);
        }

        public (List<Employee>?, string) GetEmployeeByOrganizationId(Guid organizationId)
        {
            string error = string.Empty;
            List<Employee>? employeeEntities = _context.Employee.Where(x => x.OrganizationId == organizationId).Select(e =>
                    Employee.Create(e.Id, e.Lastname, e.Firstname, e.Patronomic, e.Birthdate, e.PassportSeries, e.PassportNumber, e.OrganizationId).employee
                ).ToList();
            if (employeeEntities.Count == 0)
            {
                error = "There are no employee with this organization ID!";
            }
            return (employeeEntities, error);
        }

        public (List<Employee>?, string) GetAllEmployee()
        {
            string error = string.Empty;
            List<Employee>? employeeEntities = _context.Employee.Select(e =>
                    Employee.Create(e.Id, e.Lastname, e.Firstname, e.Patronomic, e.Birthdate, e.PassportSeries, e.PassportNumber, e.OrganizationId).employee
                ).ToList();
            if (employeeEntities.Count == 0)
            {
                error = "There are no employee!";
            }
            return (employeeEntities, error);
        }

        public async Task AddOrganization(Organization organization)
        {
            OrganizationEntity organizationEntity = new()
            {
                Id = organization.Id,
                Name = organization.Name,
                Inn = organization.Inn,
                LegalAddress = organization.LegalAddress,
                ActualAddress = organization.ActualAddress
            };
            await _context.Organizations.AddAsync(organizationEntity);
            await _context.SaveChangesAsync();
        }

        public async Task AddEmployee(Employee employee)
        {
            EmployeeEntity employeeEntity = new()
            {
                Id = employee.Id,
                Lastname = employee.Lastname,
                Firstname = employee.Firstname,
                Patronomic = employee.Patronomic,
                Birthdate = employee.Birthdate,
                PassportSeries = employee.PassportSeries,
                PassportNumber = employee.PassportNumber,
                OrganizationId = employee.OrganizationId
            };
            await _context.Employee.AddAsync(employeeEntity);
            await _context.SaveChangesAsync();
        }

        public async Task AddEmployeeList(List<Employee> employee)
        {
            List<EmployeeEntity> employeeEntities = new();
            foreach(Employee empl in employee)
            {
                employeeEntities.Add(
                    new EmployeeEntity(){
                        Id = empl.Id,
                        Lastname = empl.Lastname,
                        Firstname = empl.Firstname,
                        Patronomic = empl.Patronomic,
                        Birthdate = empl.Birthdate,
                        PassportSeries = empl.PassportSeries,
                        PassportNumber = empl.PassportNumber,
                        OrganizationId = empl.OrganizationId
                    });
            }
            List<Guid> existingIds = _context.Employee.Select(e => e.Id).ToList();
            var newEntities = employeeEntities.Where(e => !existingIds.Contains(e.Id)).ToList();
            if (newEntities.Any())
            {
                await _context.Employee.AddRangeAsync(newEntities);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddOrganizationList(List<Organization> organization)
        {
            List<OrganizationEntity> organizationEntities = new();
            foreach (Organization org in organization)
            {
                organizationEntities.Add(
                    new OrganizationEntity()
                    {
                        Id = org.Id,
                        Name = org.Name,
                        Inn = org.Inn,
                        LegalAddress = org.LegalAddress,
                        ActualAddress = org.ActualAddress
                    });
            }
            List<Guid> existingIds = _context.Organizations.Select(e => e.Id).ToList();
            var newEntities = organizationEntities.Where(e => !existingIds.Contains(e.Id)).ToList();
            if (newEntities.Any())
            {
                await _context.Organizations.AddRangeAsync(newEntities);
                await _context.SaveChangesAsync();
            }
        }
    }
}
